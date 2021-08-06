using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsInfo : MonoBehaviour
{
    public Text GameWinsText;
    public Text GamesPlayedText;
    public Text recordGameText;

    private void OnEnable()
    {
        GameWinsText.text = "Partidas ganadas: "+ PlayerPrefs.GetInt(SaveData.GameWinsData, 0).ToString();
        GamesPlayedText.text = "Partidas jugadas: " + PlayerPrefs.GetInt(SaveData.GamesPlayedData, 0).ToString();
        recordGameText.text = "Mejor partida: " + PlayerPrefs.GetInt(SaveData.GameRecordData, 0).ToString();
    }
}
