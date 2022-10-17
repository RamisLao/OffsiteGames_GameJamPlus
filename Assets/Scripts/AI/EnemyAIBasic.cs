using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyAIBasic : EnemyAI
{
    [Space]
    [Title("Broadcasting on")]
    [SerializeField] private ApplyCardEffectEventChannelSO _eventApplyCardEffect;
    [SerializeField] private ApplyAbsorbEffectEventChannelSO _eventApplyAbsorbEffect;

    [Title("Combat")]
    [SerializeField] private RuntimeSetCardData _enemyDeck;
    [SerializeField] private VariablePlayerCombat _variablePlayer;

    [Title("References")]
    [SerializeField] private Image _protectedImage;
    [SerializeField] private Image _exposedImage;
    [SerializeField] private Image _sapplingImage;
    [SerializeField] private Image _blockImage;
    [SerializeField] private Image _damageImage;
    [SerializeField] private TMPro.TextMeshProUGUI _protectedText;
    [SerializeField] private TMPro.TextMeshProUGUI _exposedText;
    [SerializeField] private TMPro.TextMeshProUGUI _sapplingText;
    [SerializeField] private TMPro.TextMeshProUGUI _blockText;
    [SerializeField] private TMPro.TextMeshProUGUI _damageText;

    protected CardData _actionToPerform;

    public override void SelectActionsToPerform()
    {
        HideAllUI();
        _actionToPerform = _enemyDeck.GetRandomItem();
        UpdateUI();
    }

    public override void PerformActions()
    {
        if (_actionToPerform.AppliesToSelf)
            _eventApplyCardEffect.RaiseEvent(_actionToPerform, this);
        else if (_actionToPerform.AppliesAbsorb)
        {
            _eventApplyAbsorbEffect.RaiseEvent(_actionToPerform, this, _variablePlayer.Value);
            OnAttack.Invoke();
        }
        else
        {
            _eventApplyCardEffect.RaiseEvent(_actionToPerform, _variablePlayer.Value);
            OnAttack.Invoke();
        }

        HideAllUI();
    }

    private void HideAllUI()
    {
        _protectedImage.gameObject.SetActive(false);
        _exposedImage.gameObject.SetActive(false);
        _sapplingImage.gameObject.SetActive(false);
        _blockImage.gameObject.SetActive(false);
        _damageImage.gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        if (_actionToPerform.AppliesProtected)
        {
            _protectedText.text = $"{_actionToPerform.ProtectedAmount}";
            _protectedImage.gameObject.SetActive(true);
        }
        if (_actionToPerform.AppliesExposed)
        {
            _exposedText.text = $"{_actionToPerform.ExposedAmount}";
            _exposedImage.gameObject.SetActive(true);
        }
        if (_actionToPerform.AppliesSappling)
        {
            _sapplingText.text = $"{_actionToPerform.SapplingAmount}";
            _sapplingImage.gameObject.SetActive(true);
        }
        if (_actionToPerform.AppliesBlock)
        {
            _blockText.text = $"{_actionToPerform.BlockAmount}";
            _blockImage.gameObject.SetActive(true);
        }
        if (_actionToPerform.AppliesDamage)
        {
            _damageText.text = $"{_actionToPerform.DamageAmount}";
            _damageImage.gameObject.SetActive(true);
        }
    }
}
