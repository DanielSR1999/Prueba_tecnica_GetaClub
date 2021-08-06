using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    Text timerText;
    int secondsRemaining;
    [SerializeField]
    int initialTime = 300;

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
    public void AddTime(int extraSeconds)
    {
        secondsRemaining += extraSeconds;
        timerText.text = secondsRemaining.ToString();
    }
}
