using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem _current;
    [SerializeField] private Tooltip _textTooltip;
    private static string _currentID = "";

    private void Awake()
    {
        _current = this;
    }

    public static void Show(string id, string content, string header = "")
    {
        _currentID = id;
        _current._textTooltip.SetText(content, header);
        _current._textTooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        _current._textTooltip.gameObject.SetActive(false);
    }

    public static void Hide(string id)
    {
        if (_currentID == id) _current._textTooltip.gameObject.SetActive(false);
    }
}
