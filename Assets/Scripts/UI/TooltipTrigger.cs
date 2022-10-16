using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] private bool _useDefaultContent = false;
    [ShowIf("@this._useDefaultContent == true && this._useText == true")]
    [SerializeField] private string _id = "";
    [ShowIf("@this._useDefaultContent == true && this._useText == true")]
    [SerializeField] private string _header = "";
    public string Header
    {
        set { _header = value; }
    }
    [ShowIf("@this._useDefaultContent == true && this._useText == true")] [Multiline(5)]
    [SerializeField] private string _content = "";
    public string Content
    {
        set { _content = value; }
    }

    [ShowIf("@this._useDefaultContent == false")]
    [SerializeField] private RuntimeSetTooltipLinkData _tooltipLinkData;
    [SerializeField] private float _delay = 1f;
    [SerializeField] private float _lifeTime = 3;
    private Coroutine _showCoroutine;
    private Coroutine _hideCoroutine;

    public void Show(string id, string content, string header = "")
    {
        _showCoroutine = StartCoroutine(ShowCoroutine(id, content, header));
    }

    private IEnumerator ShowCoroutine(string id, string content, string header = "")
    {
        yield return new WaitForSeconds(_delay);
        TooltipSystem.Show(id, content, header);

        _hideCoroutine = StartCoroutine(HideCoroutine());
    }

    private IEnumerator HideCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        Hide(_id);
    }

    public void Hide(string id)
    {
        TooltipSystem.Hide(id);
    }

    public void Hide()
    {
        if (_showCoroutine != null)
        {
            StopCoroutine(_showCoroutine);
            _showCoroutine = null;
        }
        if (_hideCoroutine != null)
        {
            StopCoroutine(_hideCoroutine);
            _hideCoroutine = null;
        }

        TooltipSystem.Hide();
    }

    // This function receives a Link ID as a 
    public void Show(string id, string b, int c)
    {
        _id = id;
        TooltipLinkData data = _tooltipLinkData.GetDataByID(id);
        if (data != null)
            Show(id, data.Description, data.Header);
    }

    public void Show()
    {
        Show(_id, _content, _header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hide();
    }
}
