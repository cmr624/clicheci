using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongPath : MonoBehaviour
{

    public float timeToTravelArcInSeconds;
    
    [HideInInspector]
    public bool Vulnerable = false;
    // Start is called before the first frame update
    void Start()
    {
        MoveOnPath();
    }

    public void MoveOnPath()
    {
        LeanTween.move(gameObject, FishMinigameManager.Instance.JumpArc.vec3, timeToTravelArcInSeconds);
    }

   
}
