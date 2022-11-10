using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class MinigameManager : MMSingleton<MinigameManager>
{

    // gets the overall flow manager
    protected GameFlowManager _flowManagerInstance;

    protected void Start()
    {
        _flowManagerInstance = GameFlowManager.Instance;
    }

}
