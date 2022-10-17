using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool _onPause = false;
    public GameObject _pausePanel;
    public GameObject _losePanel;
    public GameObject _victoryPanel;
    public AgentEventChannelSO _eventGameOver;
    public VoidEventChannelSO _eventGameWin;

    private void Start()
    {
        _eventGameWin.OnEventRaised += VictoryPanel;
        _eventGameOver.OnEventRaised += LosePanel;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _onPause = !_onPause;
        }

        if (_onPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        _pausePanel.SetActive(_onPause);
    }

    public void ResumeGame()
    {
        _onPause = false;
        Time.timeScale = 1f;
        _pausePanel.SetActive(_onPause);
    }

    public void LosePanel(Agent agent)
    {
        _losePanel.SetActive(true);
    }

    public void VictoryPanel()
    {
        _victoryPanel.SetActive(true);
    }
}
