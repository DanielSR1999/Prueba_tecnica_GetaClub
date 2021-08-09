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

    [MenuItem("GetaTest/Reload Kar Configuration")]
    static void DeleteConfig()
    {
        GameObject karSelection = GameObject.Find("CustomizationController");
        if(karSelection!=null)
        {
            karSelection.GetComponent<KarSelection>().ReloadKarCustom();
        }
    }
}
