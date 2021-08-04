using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public int secondsRemaining;
    public int initialTime = 300;

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
    }
    void AddTime(int extraSeconds)
    {
        secondsRemaining += extraSeconds;
        timerText.text = secondsRemaining.ToString();
    }
}
