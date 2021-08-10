using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeleteSaveData : MonoBehaviour
{
    [MenuItem("GetaTest/Erase stats")]
    static void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }
}
