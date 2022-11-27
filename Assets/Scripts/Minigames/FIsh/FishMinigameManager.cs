using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class FishMinigameManager :MMSingleton<FishMinigameManager>
{
    protected GameFlowManager _flowManagerInstance;

    public Bear Bear;
    protected void Start()
    {
        _flowManagerInstance = GameFlowManager.Instance;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Bear.Swipe();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            Bear.Dodge();
        }
    }
}
