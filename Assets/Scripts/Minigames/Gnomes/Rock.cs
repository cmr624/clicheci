using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rock : MonoBehaviour
{
    
    public float coinProbability = .6f;
    public float gnomeProbability = .1f;
    public float snakeProbability = .3f;


    public void Spawn()
    {
        float p = Random.Range(0, 1);
        if (p < coinProbability)
        {
            // coin
        }
        else if (p >= coinProbability && p < gnomeProbability)
        {
            // gnome
        }
        else
        {
            // snake
        }
    }
    
    

    // click on a stone
    // a stone can have 3 states with varied probability
    // 50% chance for coin
    // 25% chance for gnome
    // 25% chance for snake
}
