using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Kart",menuName ="Kart")]
public class Kart : ScriptableObject
{
    public int currentPlayerColorSelected = 0;
    public int currentFrontWheelColorSelected = 0;
    public int currentRearWheelColorSelected = 0;
    public int currentCarColorSelected = 0;

    [Header("Materials")]
    public Material playerMaterial;
    public Material wheelsFrontMaterial;
    public Material wheelsRearMaterial;
    public Material carMaterial;

    [Header("ImagesUI")]
    public Sprite playerColorImage;
    public Sprite wheelFrontImage;
    public Sprite wheelRearImage;
    public Sprite carImage;
}
