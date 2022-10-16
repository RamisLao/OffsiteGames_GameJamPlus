using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLineRenderer : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    [Title("Listening on")]
    [SerializeField] private ListVector3EventChannelSO _eventUpdateRendererPositions;
    [SerializeField] private VoidEventChannelSO _eventActivateLineRenderer;
    [SerializeField] private VoidEventChannelSO _eventDeactivateLineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
        _eventActivateLineRenderer.OnEventRaised += ActivateLineRenderer;
        _eventDeactivateLineRenderer.OnEventRaised += DeactivateLineRenderer;
        _eventUpdateRendererPositions.OnEventRaised += UpdateLineRenderer;
    }

    private void ActivateLineRenderer()
    {
        _lineRenderer.enabled = true;
    }

    private void DeactivateLineRenderer()
    {
        _lineRenderer.enabled = false;
    }

    private void UpdateLineRenderer(List<Vector3> positions)
    {
        _lineRenderer.SetPositions(positions.ToArray());
    }
}
