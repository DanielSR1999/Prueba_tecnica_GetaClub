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
        KarSelection karSelection = GameObject.Find("Customization").GetComponent<KarSelection>();
        if(karSelection!=null)
        {
            karSelection.ReloadKarCustom();
        }
    }
}
