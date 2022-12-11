using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class MicrowaveMinigameManager : MMSingleton<MicrowaveMinigameManager>
{
    protected GameFlowManager _flowManagerInstance;

    protected void Start()
    {
        _flowManagerInstance = GameFlowManager.Instance;
    }

    public void MicrowaveGameOver()
    {
        _flowManagerInstance.MinigameComplete();
    }
}
