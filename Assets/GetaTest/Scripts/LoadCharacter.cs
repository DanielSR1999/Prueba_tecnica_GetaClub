using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class LoadCharacter : MonoBehaviour
{
    Kart kartCustom;
    [Header("MeshRenderers")]
    public SkinnedMeshRenderer characterRenderer;
    public MeshRenderer[] frontWheelsRenderer;
    public MeshRenderer[] rearWheelsRenderer;
    public SkinnedMeshRenderer carRenderer;
    private void Start()
    {
        kartCustom = Resources.Load<Kart>("Kart");
        characterRenderer.material = kartCustom.playerMaterial;
        carRenderer.material = kartCustom.carMaterial;

        foreach(MeshRenderer frontWheels in frontWheelsRenderer)
        {
            frontWheels.material = kartCustom.wheelsFrontMaterial;
        }
        foreach (MeshRenderer rearWheels in rearWheelsRenderer)
        {
            rearWheels.material = kartCustom.wheelsRearMaterial;
        }

    }
}
