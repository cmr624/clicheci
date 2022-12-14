using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveAlongPath : MonoBehaviour
{

    public float timeToTravelArcInSeconds;

    public bool RandomSpeed = false;
    public float RandomRange = .1f;
    [HideInInspector]
    public bool Vulnerable = false;

    private LeanTweenPath _arc;
    // Start is called before the first frame update
    void OnEnable()
    {
        _arc = FishMinigameManager.Instance.GetRandomArc();
        MoveOnPath();
    }

    public void MoveOnPath()
    {
        if (RandomSpeed)
        {
            
            LeanTween.move(gameObject, _arc.vec3, Random.Range(timeToTravelArcInSeconds - RandomRange, timeToTravelArcInSeconds + RandomRange));
        }
        else
        {
            LeanTween.move(gameObject, _arc.vec3, timeToTravelArcInSeconds);
        }
    }

   
}
