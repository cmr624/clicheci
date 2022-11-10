using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;

public class GameFlowManager : MMPersistentSingleton<GameFlowManager>
{

    public string playerID = "playerID";
    public int score = 0;
    public string defaultScene;
    
    
    public string[] minigames;
    public int currentMinigameIndexOrdered = 0;
    private void Start()
    {
        Debug.Log(Instance.score);
    }

    public void LoadNextScene()
    {
        // remove if statement laterr
        if (currentMinigameIndexOrdered < 1)
        {
            MMSceneLoadingManager.LoadScene(minigames[currentMinigameIndexOrdered], "LoadingScreen");
            currentMinigameIndexOrdered++;
        }
    }
    
    // on complete, load back the default scene.
    public void MinigameComplete()
    {
        Instance.score+=1;
        MMSceneLoadingManager.LoadScene(defaultScene, "LoadingScreen");
    }
}
