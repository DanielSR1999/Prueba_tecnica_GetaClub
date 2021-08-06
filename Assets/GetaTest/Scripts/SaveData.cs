using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static string GameWinsData = "gameWins";
    public static string GamesPlayedData= "gamesPlayed";
    public static string GameRecordData = "gameRecord";

    public void SetGameWinsData(int value)
    {
        PlayerPrefs.SetInt(GameWinsData, value);
    }
    public void SetGamesPlayedData(int value)
    {
        PlayerPrefs.SetInt(GamesPlayedData, value);
    }
    public void SetGameRecorddData(int value)
    {
        PlayerPrefs.SetInt(GameRecordData, value);
    }
}
