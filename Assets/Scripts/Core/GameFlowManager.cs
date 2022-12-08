using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;

public class GameFlowManager : MMPersistentSingleton<GameFlowManager>
{

    public string defaultScene = "Inbetween Cutscenes";
    
    
    public string[] minigames;
    private int currentMinigameIndexOrdered = -1;

    public int CurrentMinigameIndexOrdered
    {
        get => currentMinigameIndexOrdered;
        
    }


    /**
     * CHAOS MODE
     * minigames in any order
     * minigame data (variants)
     * overall score counter
     * 
     */

    private bool _inChaosMode;

    public string ChaosModeIntroSceneName;
    public string ChaosModeInBetweenSceneName;
    
    public float score = 0f;
    public string playerID = "the other cm";

    public void StartChaosMode()
    {
        score = 0f;
        currentMinigameIndexOrdered = -1;
        defaultScene = ChaosModeIntroSceneName;
        _inChaosMode = true;
        LoadScene(ChaosModeIntroSceneName);
    }
    // 
    
    
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


    public void PlayAgain()
    {
        _instance.GameOver = false;
        _instance.currentMinigameIndexOrdered = -1;
        _instance.FirstInBetween = true;
        MMSceneLoadingManager.LoadScene("MainMenu", LoadingSceneName);
    }

    public string IntroCutscene = "IntroCutscene";
    public void PlayButtonPressed()
    {
        MMSceneLoadingManager.LoadScene(IntroCutscene, LoadingSceneName);
    }

    public void LoadScene(string name)
    {
        MMSceneLoadingManager.LoadScene(name, LoadingSceneName);
    }

    [HideInInspector] public bool GameOver = false;
    public void LoadNextMinigame()
    {
        currentMinigameIndexOrdered++;
        if (currentMinigameIndexOrdered > minigames.Length - 1 && _inChaosMode)
        {
            currentMinigameIndexOrdered = 0;
        }
        MMSceneLoadingManager.LoadScene(minigames[currentMinigameIndexOrdered]);
    }

    public string LoadingSceneName = "LoadingScreen";
    public string InBetweensSceneName = "Inbetween Cutscenes";

    public bool FirstInBetween = true;
    public void LoadInBetween()
    {
        
        MMSceneLoadingManager.LoadScene(InBetweensSceneName, LoadingSceneName); 
    }
    
    // on complete, load back the default scene.
    public void MinigameComplete()
    {
        
        Instance.score+=1;
        MMSoundManager.Instance.StopAllSounds();
        MMSoundManager.Instance.StopTrack(MMSoundManager.MMSoundManagerTracks.Music);
        MMSceneLoadingManager.LoadScene(defaultScene, LoadingSceneName);
    }
}
