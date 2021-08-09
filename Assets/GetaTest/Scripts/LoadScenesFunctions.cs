using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenesFunctions : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }
    public void LoadScene(int _sceneIndex)
    {
        Time.timeScale = 1;
        ScenesController.LoadScene(_sceneIndex);
    }
}
