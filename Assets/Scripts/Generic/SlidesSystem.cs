using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Sirenix.OdinInspector;

public class SlidesSystem : MonoBehaviour
{
    [Title("Slides Settings")]
    [SerializeField] public List<GameObject> _slidesList;
    [SerializeField] private TextMeshProUGUI _slidesCount;
    private int actualSlide = 0;
    [SerializeField] private string nextScene;

    private void Start()
    {
        _slidesCount.text = $"1/{_slidesList.Count}";
    }

    public void NextSlide()
    {
        _slidesList[actualSlide].SetActive(false);
        if (actualSlide < _slidesList.Count-1)
        {
            actualSlide++;
            _slidesList[actualSlide].SetActive(true);
            _slidesCount.text = $"{actualSlide + 1}/{_slidesList.Count}";
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
