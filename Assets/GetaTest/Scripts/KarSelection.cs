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

    Kart kar;
    public int indexScene;
    Scene myScene;
    private void Start()
    {
        
        kar = Resources.Load<Kart>("Kart");
        if(kar==null)
        {
            kar = AssetDatabase.LoadAssetAtPath<Kart>("Assets/Resources/Kart.asset");
        }
        myScene = SceneManager.GetActiveScene();
        LoadInfo();
    }
    
    public void ReloadKarCustom()
    {
        kar = Resources.Load<Kart>("Kart");
        if (kar == null)
        {
            kar = AssetDatabase.LoadAssetAtPath<Kart>("Assets/Resources/Kart.asset");
        }
        kar.currentPlayerColorSelected = 0;
        kar.currentFrontWheelColorSelected = 0;
        kar.currentRearWheelColorSelected = 0;
        kar.currentCarColorSelected = 0;

        kar.playerMaterial = playerMaterial[0];
        kar.wheelsFrontMaterial = wheelsFrontMaterial[0];
        kar.wheelsRearMaterial = wheelsRearMaterial[0];
        kar.carMaterial = carMaterial[0];

        kar.playerColorImage = playerSelector[0];
        kar.wheelFrontImage = wheelFrontSelector[0];
        kar.wheelRearImage = wheelRearSelector[0];
        kar.carImage = carSelector[0];
    }

    
    void LoadInfo()
    {
        currentPlayerSelection = kar.currentPlayerColorSelected;
        currentFrontWheelSelection = kar.currentFrontWheelColorSelected;
        currentRearWheelSelection = kar.currentRearWheelColorSelected;
        currentCarColorSelection = kar.currentCarColorSelected;

        if(myScene.name==("MainMenu"))
        {
            playerAvatarImage.sprite = kar.playerColorImage;
            wheelsFrontImage.sprite = kar.wheelFrontImage;
            wheelsRearImage.sprite = kar.wheelRearImage;
            carImage.sprite = kar.carImage;
        }
        
    }
    void SetPlayerColor()
    {
        playerAvatarImage.sprite = playerSelector[currentPlayerSelection];
        kar.playerColorImage = playerSelector[currentPlayerSelection];
        kar.playerMaterial = playerMaterial[currentPlayerSelection];
    }
    void SetFrontWheel()
    {
        wheelsFrontImage.sprite = wheelFrontSelector[currentFrontWheelSelection];
        kar.wheelFrontImage = wheelFrontSelector[currentFrontWheelSelection];
        kar.wheelsFrontMaterial = wheelsFrontMaterial[currentFrontWheelSelection];
    }
    void SetRearWheel()
    {
        wheelsRearImage.sprite = wheelRearSelector[currentRearWheelSelection];
        kar.wheelRearImage = wheelRearSelector[currentRearWheelSelection];
        kar.wheelsRearMaterial = wheelsRearMaterial[currentRearWheelSelection];
    }
    void SetCarColor()
    {
        carImage.sprite = carSelector[currentCarColorSelection];
        kar.carImage = carSelector[currentCarColorSelection];
        kar.carMaterial = carMaterial[currentCarColorSelection];
    }
    public void SelectNextPlayerColor()
    {
        if(currentPlayerSelection<playerSelector.Count-1)
        {
            currentPlayerSelection++;
            kar.currentPlayerColorSelected = currentPlayerSelection;
            SetPlayerColor();
        }
        else
        {
            currentPlayerSelection = 0;
            kar.currentPlayerColorSelected = currentPlayerSelection;
            SetPlayerColor();
        }
    }
    public void SelectLastPlayerColor()
    {
        if (currentPlayerSelection > 0)
        {
            currentPlayerSelection--;
            kar.currentPlayerColorSelected = currentPlayerSelection;
            SetPlayerColor();
        }
        else
        {
            currentPlayerSelection = playerSelector.Count - 1;
            kar.currentPlayerColorSelected = currentPlayerSelection;
            SetPlayerColor();
        }
    }
    public void SelectNextFrontWheelColor()
    {
        if (currentFrontWheelSelection < wheelFrontSelector.Count - 1)
        {
            currentFrontWheelSelection++;
            kar.currentFrontWheelColorSelected = currentFrontWheelSelection;
            SetFrontWheel();
        }
        else
        {
            currentFrontWheelSelection = 0;
            kar.currentFrontWheelColorSelected = currentFrontWheelSelection;
            SetFrontWheel();
        }
    }
    public void SelectLastFrontWheelColor()
    {
        if (currentFrontWheelSelection > 0)
        {
            currentFrontWheelSelection--;
            kar.currentFrontWheelColorSelected = currentFrontWheelSelection;
            SetFrontWheel();
        }
        else
        {
            currentFrontWheelSelection = wheelFrontSelector.Count - 1;
            kar.currentFrontWheelColorSelected = currentFrontWheelSelection;
            SetFrontWheel();
        }
    }
    public void SelectNextRearWheelColor()
    {
        if (currentRearWheelSelection < wheelRearSelector.Count - 1)
        {
            currentRearWheelSelection++;
            kar.currentRearWheelColorSelected = currentRearWheelSelection;
            SetRearWheel();
        }
        else
        {
            currentRearWheelSelection = 0;
            kar.currentRearWheelColorSelected = currentRearWheelSelection;
            SetRearWheel();
        }
    }
    public void SelectLastRearWheelColor()
    {
        if (currentRearWheelSelection > 0)
        {
            currentRearWheelSelection--;
            kar.currentRearWheelColorSelected = currentRearWheelSelection;
            SetRearWheel();
        }
        else
        {
            currentRearWheelSelection = wheelRearSelector.Count - 1;
            kar.currentRearWheelColorSelected = currentRearWheelSelection;
            SetRearWheel();
        }
    }
    public void SelectNextCarColor()
    {
        if (currentCarColorSelection < carSelector.Count - 1)
        {
            currentCarColorSelection++;
            kar.currentCarColorSelected = currentCarColorSelection;
            SetCarColor();
        }
        else
        {
            currentCarColorSelection = 0;
            kar.currentCarColorSelected = currentCarColorSelection;
            SetCarColor();
        }
    }
    public void SelectLastCarColor()
    {
        if (currentCarColorSelection > 0)
        {
            currentCarColorSelection--;
            kar.currentCarColorSelected = currentCarColorSelection;
            SetCarColor();
        }
        else
        {
            currentCarColorSelection = carSelector.Count - 1;
            kar.currentCarColorSelected = currentCarColorSelection;
            SetCarColor();
        }
    }
}
