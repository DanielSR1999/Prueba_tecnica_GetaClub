using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsInfo : MonoBehaviour
{
    [Header("RacingStats")]
    public Text GameWinsText;
    public Text GamesPlayedText;
    public Text recordGameText;
    public Text GamesLosses;

    [Header("RailsStats")]
    public Text RailGamesPlayedText;
    public Text RailrecordGameText;
    private void OnEnable()
    {
        GameWinsText.text = "Partidas ganadas: " + PlayerPrefs.GetInt(SaveData.GameWinsDataID, 0).ToString();
        GamesPlayedText.text = "Partidas jugadas: " + PlayerPrefs.GetInt(SaveData.GamesPlayedDataID, 0).ToString();
        recordGameText.text = "Mejor vuelta: " + PlayerPrefs.GetInt(SaveData.GameRecordDataID, 0).ToString() +" segundos restantes";

        int GameLosses = PlayerPrefs.GetInt(SaveData.GamesPlayedDataID, 0) - PlayerPrefs.GetInt(SaveData.GameWinsDataID, 0);
        GamesLosses.text = "Partidas perdidas: " + GameLosses.ToString();

        RailGamesPlayedText.text = "Partidas jugadas: " + PlayerPrefs.GetInt(SaveData.railsGamesPlayedID);
        RailrecordGameText.text = "Record: " + PlayerPrefs.GetInt(SaveData.recordRailsModeID, 0);
    }
}
