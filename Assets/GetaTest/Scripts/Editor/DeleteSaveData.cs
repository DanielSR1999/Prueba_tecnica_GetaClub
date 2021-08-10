using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeleteSaveData : MonoBehaviour
{
    [MenuItem("GetaTest/Erase PlayerPrefs")]
    static void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }
}
