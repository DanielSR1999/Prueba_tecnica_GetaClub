using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    public Slider loadingBar;

    int sceneToLoadIndex;
    void Start()
    {
        sceneToLoadIndex = ScenesController.sceneIndex;
        StartCoroutine(_LoadScene(sceneToLoadIndex));
        
    }
    IEnumerator _LoadScene(int sceneIndex)
    {
        yield return new WaitForSeconds(0.8f);

        AsyncOperation loadingOperation= SceneManager.LoadSceneAsync(sceneIndex);

        while(!loadingOperation.isDone)
        {
            loadingBar.value = Mathf.Clamp01(loadingOperation.progress);
            yield return null;
        }
    }
    
}
