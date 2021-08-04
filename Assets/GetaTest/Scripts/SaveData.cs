using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public void SetGameWinsData(string DataID,int value)
    {
        PlayerPrefs.SetInt(DataID, value);
    }
    public void SetGamesPlayedData(string DataID, int value)
    {
        PlayerPrefs.SetInt(DataID, value);
    }
}
