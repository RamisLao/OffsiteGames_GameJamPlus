using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
    public void LoadSceneAdditively(string sceneName)
    {
        Debug.Log($"Loading Scene Additively: {sceneName}");
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log($"Loading Scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneWithDelay(string sceneName)
    {
        StartCoroutine(LoadSceneWithDelayCoroutine(sceneName));
    }

    private IEnumerator LoadSceneWithDelayCoroutine(string sceneName)
    {
        yield return new WaitForSeconds(2);
        Debug.Log($"Loading Scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    public void UnloadScene(string sceneName)
    {
        Debug.Log($"Unloading Scene: {sceneName}");
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}