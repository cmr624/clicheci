using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;

public class GameFlowManager : MMPersistentSingleton<GameFlowManager>
{

    public string playerID = "the other cm";
    public int score = 0;
    public string defaultScene = "Inbetween Cutscenes";
    
    
    public string[] minigames;
    private int currentMinigameIndexOrdered = -1;

    //win/lose
    //needs to be null, but can't be??
    public bool WonLastGame;



    /**
     * CURRENT FLOW
     *
     * 1. main menu
     * 2. play button selected, loading screen, intro cutscene
     * 3. intro cutscene ends. pitch begins, load in betweens
     * 4. play in between, play associated minigame
     * 5. back to in between, play associated reaction.
     */

    private void Start()
    {
        Debug.Log(Instance.score);
    }


    public string IntroCutscene = "IntroCutscene";
    public void PlayButtonPressed()
    {
        MMSceneLoadingManager.LoadScene("IntroCutscene", LoadingSceneName);
    }

    public void LoadScene(string name)
    {
        MMSceneLoadingManager.LoadScene(name, LoadingSceneName);
    }
    
    public void LoadNextMinigame()
    {
        currentMinigameIndexOrdered++;
        MMSceneLoadingManager.LoadScene(minigames[currentMinigameIndexOrdered], LoadingSceneName);
        // remove if statement laterr
        /*
        if (currentMinigameIndexOrdered < 1)
        {
            MMSceneLoadingManager.LoadScene(minigames[currentMinigameIndexOrdered], "LoadingScreen");
            currentMinigameIndexOrdered++;
        }*/
    }

    public string LoadingSceneName = "LoadingScreen";
    public string InBetweensSceneName = "Inbetween Cutscenes";

    public bool FirstInBetween = true;
    public void LoadInBetween()
    {
        if (FirstInBetween)
        {
            FirstInBetween = false;
        }
        MMSceneLoadingManager.LoadScene(InBetweensSceneName, LoadingSceneName); 
    }
    
    // on complete, load back the default scene.
    public void MinigameComplete()
    {
        WonLastGame = true;
        Instance.score+=1;
        MMSoundManager.Instance.StopTrack(MMSoundManager.MMSoundManagerTracks.Music);
        MMSceneLoadingManager.LoadScene(defaultScene, LoadingSceneName);
    }
}
