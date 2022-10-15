using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmTreeGroves : MonoBehaviour
{
    [SerializeField] private GameObject dirtyPalm;
    [SerializeField] private GameObject cleanPalm;

    public void CleanPalm()
    {
        dirtyPalm.SetActive(false);
        cleanPalm.SetActive(true);
    }
}
