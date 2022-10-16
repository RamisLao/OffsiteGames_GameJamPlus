using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmTreeGroves : MonoBehaviour
{
    [SerializeField] private GameObject dirtyPalm;
    [SerializeField] private GameObject cleanPalm;
    private bool _palmIsCleaned = false;
    public bool PalmIsCleaned => _palmIsCleaned;

    public void CleanPalm()
    {
        dirtyPalm.SetActive(false);
        cleanPalm.SetActive(true);
        _palmIsCleaned = true;
    }
}
