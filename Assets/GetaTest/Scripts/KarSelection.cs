using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using UnityEditor;
using UnityEngine.SceneManagement;

public class KarSelection : MonoBehaviour
{
    public List<Sprite> playerSelector;
    public List<Sprite> wheelFrontSelector;
    public List<Sprite> wheelRearSelector;
    public List<Sprite> carSelector;

    public List<Material> playerMaterial;
    public List<Material> wheelsFrontMaterial;
    public List<Material> wheelsRearMaterial;
    public List<Material> carMaterial;

    public Image playerAvatarImage;
    public Image wheelsFrontImage;
    public Image wheelsRearImage;
    public Image carImage;

    int currentPlayerSelection = 0;
    int currentFrontWheelSelection = 0;
    int currentRearWheelSelection = 0;
    int currentCarColorSelection = 0;

    [Header("PlayerPrefsIDs")]
    public static string playerSelectionID = "playerColor";
    public static string FrontWheelSelectionID = "frontWheelColor";
    public static string RearWHeelSelectionID = "rearWheelColor";
    public static string CarSelectionID = "carColor";

    [Space(5)] [SerializeField] Kart[] kars;
    public int indexScene;
    Scene myScene;
    private void Start()
    {
        myScene = SceneManager.GetActiveScene();

        int playerSelection = PlayerPrefs.GetInt(playerSelectionID, 0);
        int FrontWheelSelection = PlayerPrefs.GetInt(FrontWheelSelectionID, 0);
        int RearWHeelSelection = PlayerPrefs.GetInt(RearWHeelSelectionID, 0);
        int CarSelection = PlayerPrefs.GetInt(CarSelectionID, 0);

        currentPlayerSelection = playerSelection;
        currentFrontWheelSelection = FrontWheelSelection;
        currentRearWheelSelection = RearWHeelSelection;
        currentCarColorSelection = CarSelection;

        LoadInfo(playerSelection, FrontWheelSelection, RearWHeelSelection, CarSelection);
    }
    
    void LoadInfo(int player,int FrontWheel,int RearWheel, int car)
    {
        if (myScene.name == ("MainMenu"))
        {
            playerAvatarImage.sprite = kars[player].playerColorImage;
            wheelsFrontImage.sprite = kars[FrontWheel].wheelFrontImage;
            wheelsRearImage.sprite = kars[RearWheel].wheelRearImage;
            carImage.sprite = kars[car].carImage;
        }

    }
    void SetPlayerColor()
    {
        playerAvatarImage.sprite = playerSelector[currentPlayerSelection];
        PlayerPrefs.SetInt(playerSelectionID, currentPlayerSelection);
    }
    void SetFrontWheel()
    {
        wheelsFrontImage.sprite = wheelFrontSelector[currentFrontWheelSelection];
        PlayerPrefs.SetInt(FrontWheelSelectionID, currentFrontWheelSelection);
    }
    void SetRearWheel()
    {
        wheelsRearImage.sprite = wheelRearSelector[currentRearWheelSelection];
        PlayerPrefs.SetInt(RearWHeelSelectionID, currentRearWheelSelection);
    }
    void SetCarColor()
    {
        carImage.sprite = carSelector[currentCarColorSelection];
        PlayerPrefs.SetInt(CarSelectionID, currentCarColorSelection);
    }
    public void SelectNextPlayerColor()
    {
        if(currentPlayerSelection<playerSelector.Count-1)
        {
            currentPlayerSelection++;
            SetPlayerColor();
        }
        else
        {
            currentPlayerSelection = 0;
            SetPlayerColor();
        }
    }
    public void SelectLastPlayerColor()
    {
        if (currentPlayerSelection > 0)
        {
            currentPlayerSelection--;
            SetPlayerColor();
        }
        else
        {
            currentPlayerSelection = playerSelector.Count - 1;
            SetPlayerColor();
        }
    }
    public void SelectNextFrontWheelColor()
    {
        if (currentFrontWheelSelection < wheelFrontSelector.Count - 1)
        {
            currentFrontWheelSelection++;
            SetFrontWheel();
        }
        else
        {
            currentFrontWheelSelection = 0;
            SetFrontWheel();
        }
    }
    public void SelectLastFrontWheelColor()
    {
        if (currentFrontWheelSelection > 0)
        {
            currentFrontWheelSelection--;
            SetFrontWheel();
        }
        else
        {
            currentFrontWheelSelection = wheelFrontSelector.Count - 1;
            SetFrontWheel();
        }
    }
    public void SelectNextRearWheelColor()
    {
        if (currentRearWheelSelection < wheelRearSelector.Count - 1)
        {
            currentRearWheelSelection++;
            SetRearWheel();
        }
        else
        {
            currentRearWheelSelection = 0;
            SetRearWheel();
        }
    }
    public void SelectLastRearWheelColor()
    {
        if (currentRearWheelSelection > 0)
        {
            currentRearWheelSelection--;
            SetRearWheel();
        }
        else
        {
            currentRearWheelSelection = wheelRearSelector.Count - 1;
            SetRearWheel();
        }
    }
    public void SelectNextCarColor()
    {
        if (currentCarColorSelection < carSelector.Count - 1)
        {
            currentCarColorSelection++;
            SetCarColor();
        }
        else
        {
            currentCarColorSelection = 0;
            SetCarColor();
        }
    }
    public void SelectLastCarColor()
    {
        if (currentCarColorSelection > 0)
        {
            currentCarColorSelection--;
            SetCarColor();
        }
        else
        {
            currentCarColorSelection = carSelector.Count - 1;
            SetCarColor();
        }
    }
}
