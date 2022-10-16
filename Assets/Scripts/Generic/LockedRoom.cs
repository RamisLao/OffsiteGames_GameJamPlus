using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedRoom : MonoBehaviour
{
    [Title("Room Settings")]
    [SerializeField] private List<PalmTreeGroves> _treesToClean;
    [SerializeField] private GameObject _doorToUnlock;

    [Title("Listening on")]
    [SerializeField] private VoidEventChannelSO _eventOnCombatEnded;

    private bool _locked = true;

    private void Awake()
    {
        //_eventOnCombatEnded.OnEventRaised += MaybeUnlock;
    }

    private void Update()
    {
        if (_locked)
        {
            MaybeUnlock();
        }
    }

    private void MaybeUnlock()
    {
        if (_treesToClean == null || _treesToClean.Count == 0) return;

        bool unlocked = true;
        foreach (PalmTreeGroves p in _treesToClean)
        {
            if (!p.PalmIsCleaned)
            {
                unlocked = false;
                break;
            }
        }

        if (unlocked)
        {
            _doorToUnlock.SetActive(false);
            _locked = false;
        }
    }
}
