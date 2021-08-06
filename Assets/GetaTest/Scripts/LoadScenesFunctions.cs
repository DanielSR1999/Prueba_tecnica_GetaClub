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
        ScenesController.LoadScene(_sceneIndex);
    }
}
