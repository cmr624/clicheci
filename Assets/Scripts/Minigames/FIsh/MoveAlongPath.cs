using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveAlongPath : MonoBehaviour
{

    public float timeToTravelArcInSeconds;

    public bool RandomSpeed = false;
    [HideInInspector]
    public bool Vulnerable = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        MoveOnPath();
    }

    public void MoveOnPath()
    {
        if (RandomSpeed)
        {
            
            LeanTween.move(gameObject, FishMinigameManager.Instance.JumpArc.vec3, Random.Range(1f, timeToTravelArcInSeconds));
        }
        else
        {
            LeanTween.move(gameObject, FishMinigameManager.Instance.JumpArc.vec3, timeToTravelArcInSeconds);
        }
    }

   
}
