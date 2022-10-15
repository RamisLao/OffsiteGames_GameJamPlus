using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReleaseLimits : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransformLimits;

    public bool IsMouseInsideLimits()
    {
        Vector2 localMousePosition = _rectTransformLimits.InverseTransformPoint(Input.mousePosition);
        if (_rectTransformLimits.rect.Contains(localMousePosition)) return true;

        return false;
    }
}
