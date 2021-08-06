using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesController
{
    public static int sceneIndex;
    
    public static void LoadScene(int _sceneIndex)
    {
        sceneIndex = _sceneIndex;
        SceneManager.LoadScene(1); //cargamos la escena loading antes de cargar la siguiente escena
    }
   
}
