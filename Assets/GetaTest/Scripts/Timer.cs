using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    SoundsController soundsController;
    [SerializeField]
    Text timerText;
    public int secondsRemaining;
    [SerializeField]
    int initialTime = 300;
    [SerializeField]
    Canvas gameCanvas;
    [SerializeField]
    Canvas resultUI;
    public Text resultText;
    [SerializeField]
    string winText;
    [SerializeField]
    string lossText;
    private void Start()
    {
        secondsRemaining = initialTime;
        StartCoroutine(_Timer());
    }

    IEnumerator _Timer()
    {
        while(secondsRemaining>0)
        {
            yield return new WaitForSeconds(1f);
            secondsRemaining--;
            timerText.text = secondsRemaining.ToString();
        }
        gameCanvas.enabled = false;
        resultUI.enabled = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<KartController>().DisableMovement();
        player.GetComponent<KartController>().SetGameData(false, 0);
        resultText.text = lossText;
        soundsController.PlayLoss();
    }

    public void Win()
    {
        gameCanvas.enabled = false;
        resultUI.enabled = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<KartController>().DisableMovement();
        player.GetComponent<KartController>().SetGameData(true, secondsRemaining);
        resultText.text = winText;
        soundsController.PlayWin();
    }
    public void AddTime(int extraSeconds)
    {
        secondsRemaining += extraSeconds;
        timerText.text = secondsRemaining.ToString();
    }
}
