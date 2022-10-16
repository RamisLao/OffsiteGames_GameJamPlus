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

    private void Awake()
    {
        _eventOnCombatEnded.OnEventRaised += MaybeUnlock;
    }

    private void MaybeUnlock()
    {
        bool unlocked = true;
        foreach (PalmTreeGroves p in _treesToClean)
        {
            if (!p.PalmIsCleaned)
            {
                unlocked = false;
                break;
            }
        }

        if (unlocked) _doorToUnlock.SetActive(false);
    }
}
