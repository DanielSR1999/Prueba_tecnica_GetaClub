using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ShowMechanimObjects : MonoBehaviour
{
    [MenuItem("GetaTest/Show Instantiate PowerUp 1")]
    static void ShowRandomInstantiatePowerUp01()
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        GameObject _object = GameObject.Find("PowerUpInstantiate1");       
        sceneView.AlignViewToObject(_object.transform);
        sceneView.Repaint();
        Selection.SetActiveObjectWithContext(_object, null);
    }

    [MenuItem("GetaTest/Show Instantiate PowerUp 2")]
    static void ShowRandomInstantiatePowerUp02()
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        GameObject _object = GameObject.Find("PowerUpInstantiate2");
        sceneView.AlignViewToObject(_object.transform);
        sceneView.Repaint();
        Selection.SetActiveObjectWithContext(_object, null);
    }

    [MenuItem("GetaTest/Show Instantiate PowerUp 3")]
    static void ShowRandomInstantiatePowerUp03()
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        GameObject _object = GameObject.Find("PowerUpInstantiate3");
        sceneView.AlignViewToObject(_object.transform);
        sceneView.Repaint();
        Selection.SetActiveObjectWithContext(_object, null);
    }
    [MenuItem("GetaTest/Show Instantiate PowerUp 4")]
    static void ShowRandomInstantiatePowerUp04()
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        GameObject _object = GameObject.Find("PowerUpInstantiate4");
        sceneView.AlignViewToObject(_object.transform);
        sceneView.Repaint();
        Selection.SetActiveObjectWithContext(_object, null);
    }

    [MenuItem("GetaTest/Show Oil Obstacle 1")]
    static void ShowOilObstacle01()
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        GameObject _object = GameObject.Find("OilObstacle1");
        sceneView.AlignViewToObject(_object.transform);
        sceneView.Repaint();
        Selection.SetActiveObjectWithContext(_object, null);
    }
    [MenuItem("GetaTest/Show Oil Obstacle 2")]
    static void ShowOilObstacle02()
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        GameObject _object = GameObject.Find("OilObstacle2");
        sceneView.AlignViewToObject(_object.transform);
        sceneView.Repaint();
        Selection.SetActiveObjectWithContext(_object, null);
    }
    [MenuItem("GetaTest/Show Jump PowerUp 1")]
    static void ShowJumpPowerUp01()
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        GameObject _object = GameObject.Find("JumpPowerUp1");
        sceneView.AlignViewToObject(_object.transform);
        sceneView.Repaint();
        Selection.SetActiveObjectWithContext(_object, null);
    }
    [MenuItem("GetaTest/Show Jump PowerUp 2")]
    static void ShowJumpPowerUp02()
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        GameObject _object = GameObject.Find("JumpPowerUp2");
        sceneView.AlignViewToObject(_object.transform);
        sceneView.Repaint();
        Selection.SetActiveObjectWithContext(_object, null);
    }
    [MenuItem("GetaTest/Show Jump PowerUp 3")]
    static void ShowJumpPowerUp03()
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        GameObject _object = GameObject.Find("JumpPowerUp3");
        sceneView.AlignViewToObject(_object.transform);
        sceneView.Repaint();
        Selection.SetActiveObjectWithContext(_object, null);
    }
}
