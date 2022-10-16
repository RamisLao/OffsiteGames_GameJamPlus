using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _contentText;
    [SerializeField] private LayoutElement _layoutElement;
    [SerializeField] private int _characterWrapLimit;
    [SerializeField] private InputSystemUIInputModule _inputModule;
    private RectTransform _rectTransform;

    public void SetText(string content, string header = "")
    {
        if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
        if (string.IsNullOrEmpty(header))
        {
            _headerText.gameObject.SetActive(false);
        }
        else
        {
            _headerText.gameObject.SetActive(true);
            _headerText.text = header;
        }

        _contentText.text = content;

        int headerLength = _headerText.text.Length;
        int contentLenght = _contentText.text.Length;

        _layoutElement.enabled = (headerLength > _characterWrapLimit || contentLenght > _characterWrapLimit) ? true : false;

        Vector2 position = _inputModule.point.action.ReadValue<Vector2>();

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        _rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }
}
