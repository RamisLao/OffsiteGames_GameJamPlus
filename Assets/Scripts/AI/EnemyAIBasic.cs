using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAIBasic : EnemyAI
{
    [Space]
    [Title("Broadcasting on")]
    [SerializeField] private ApplyCardEffectEventChannelSO _eventApplyCardEffect;
    [SerializeField] private ApplyAbsorbEffectEventChannelSO _eventApplyAbsorbEffect;

    [Title("Combat")]
    [SerializeField] private RuntimeSetCardData _enemyDeck;
    [SerializeField] private VariablePlayerCombat _variablePlayer;

    protected CardData _actionToPerform;
    public UnityEvent OnAttack;

    public override void SelectActionsToPerform()
    {
        _actionToPerform = _enemyDeck.GetRandomItem();
        Debug.Log(_actionToPerform);
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
    }
}
