using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG;

public class Rock : MonoBehaviour
{
     // click on a stone
    // a stone can have 3 states with varied probability
    // 60% chance for coin
    // 10% chance for gnome
    // 20% chance for snake
    private SpriteRenderer _sprite;

    public GameObject Coin;
    public SpawnedObjectData CoinData;
    public GameObject Snake;
    public SpawnedObjectData SnakeData;
    public GameObject Gnome;
    public SpawnedObjectData GnomeData;


    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void Spawn()
    {
        
        float[] weights = {CoinData.probability, SnakeData.probability, GnomeData.probability};
        int choice = GetRandomWeightedIndex(weights);
        if (choice == 0)
        {
            Coin.SetActive(true);
            GnomeMinigameManager.Instance.AddScore(CoinData.scoreValue);
            _sprite.transform.gameObject.SetActive(false);
        }
        else if (choice == 1)
        {
            Snake.SetActive(true);
            GnomeMinigameManager.Instance.AddScore(SnakeData.scoreValue);
            _sprite.transform.gameObject.SetActive(false);
        }
        else if (choice == 2)
        {
            Gnome.SetActive(true);
            GnomeMinigameManager.Instance.AddScore(GnomeData.scoreValue);
            _sprite.transform.gameObject.SetActive(false);
        }

        Respawn();
    }

    private void Respawn()
    {
        Debug.Log("");
    }
    
    // ty https://forum.unity.com/threads/random-numbers-with-a-weighted-chance.442190/
    public int GetRandomWeightedIndex(float[] weights)
    {
        if (weights == null || weights.Length == 0) return -1;
 
        float w;
        float t = 0;
        int i;
        for (i = 0; i < weights.Length; i++)
        {
            w = weights[i];
 
            if (float.IsPositiveInfinity(w))
            {
                return i;
            }
            else if (w >= 0f && !float.IsNaN(w))
            {
                t += weights[i];
            }
        }
 
        float r = Random.value;
        float s = 0f;
 
        for (i = 0; i < weights.Length; i++)
        {
            w = weights[i];
            if (float.IsNaN(w) || w <= 0f) continue;
 
            s += w / t;
            if (s >= r) return i;
        }
 
        return -1;
    } 
   
}


/*
 *  float p = Random.Range(0f, 1f);
           if (p < coinProbability)
           {
               // coin
               // TODO change all these to prefabs
           }
           else if (p >= coinProbability && p < gnomeProbability)
           {
               // gnome
           }
           else
           {
               // snake
           }
 *
 * 
 */