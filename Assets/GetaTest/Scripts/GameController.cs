using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text winnerText;
    public string winnerMessage;
    public void Win()
    {
        winnerText.text = winnerMessage;
    }
}
