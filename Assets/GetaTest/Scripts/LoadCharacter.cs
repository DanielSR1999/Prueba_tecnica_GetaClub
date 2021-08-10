using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class LoadCharacter : MonoBehaviour
{
    [SerializeField] Kart[] kartsCustom;
    [Header("MeshRenderers")]
    public SkinnedMeshRenderer characterRenderer;
    public MeshRenderer[] frontWheelsRenderer;
    public MeshRenderer[] rearWheelsRenderer;
    public SkinnedMeshRenderer carRenderer;
    private void Start()
    {
        int playerMaterialSelection = PlayerPrefs.GetInt(KarSelection.playerSelectionID);
        int carMaterialSelection = PlayerPrefs.GetInt(KarSelection.CarSelectionID);
        int frontWheelSelection = PlayerPrefs.GetInt(KarSelection.FrontWheelSelectionID);
        int rearWheelSelection = PlayerPrefs.GetInt(KarSelection.RearWHeelSelectionID);

        characterRenderer.material = kartsCustom[playerMaterialSelection].playerMaterial;
        carRenderer.material = kartsCustom[carMaterialSelection].carMaterial;

        foreach(MeshRenderer frontWheels in frontWheelsRenderer)
        {
            frontWheels.material = kartsCustom[frontWheelSelection].wheelsFrontMaterial;
        }
        foreach (MeshRenderer rearWheels in rearWheelsRenderer)
        {
            rearWheels.material = kartsCustom[rearWheelSelection].wheelsRearMaterial;
        }

    }
}
