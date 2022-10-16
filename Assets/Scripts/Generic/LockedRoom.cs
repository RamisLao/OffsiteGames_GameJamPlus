using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedRoom : MonoBehaviour
{
    [Title("Room Settings")]
    [SerializeField] private int _enemiesToDefeated;
    [SerializeField] private VariableInt _enemiesDefeated;
    [SerializeField] private GameObject _doorToUnlock;
    private bool _isUnlocked;

    private void Start()
    {
        _enemiesDefeated.Value = 0;
    }

    private void Update()
    {
        if (_isUnlocked)
            return;

        UnlockRoom();
    }

    private void UnlockRoom()
    {
        if(_enemiesDefeated.Value >= _enemiesToDefeated)
        {
            _doorToUnlock.SetActive(false);
            _isUnlocked = true;
        }
    }
}
