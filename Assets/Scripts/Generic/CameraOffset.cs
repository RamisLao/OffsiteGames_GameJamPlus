using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

public class CameraOffset : MonoBehaviour
{
    [Title("Offset Settings")]
    [SerializeField] private CinemachineCameraOffset _cameraOffset;
    public VariableVector2 _directionToMove;
    [SerializeField] private float _scaleModifier = 4;
    [SerializeField] private float _speed = 4;

    private void Update()
    {
        if(_directionToMove.Value.magnitude > 0)
        {
            _cameraOffset.m_Offset = new Vector2(Mathf.Lerp(_cameraOffset.m_Offset.x,_directionToMove.Value.x * _scaleModifier,Time.deltaTime*4f),
                Mathf.Lerp(_cameraOffset.m_Offset.y, _directionToMove.Value.y * _scaleModifier, Time.deltaTime * _speed));
        }
        else
        {
            _cameraOffset.m_Offset = new Vector2(Mathf.Lerp(_cameraOffset.m_Offset.x, 0f, Time.deltaTime * _speed),
                Mathf.Lerp(_cameraOffset.m_Offset.y, 0f, Time.deltaTime * _speed));
        }
    }
}
