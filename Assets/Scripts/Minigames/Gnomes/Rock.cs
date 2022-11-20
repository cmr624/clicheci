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
        
        // add in a rock breaking animation
        if (choice == 0)
        {
            Coin.SetActive(true);
            GnomeMinigameManager.Instance.AddScore(CoinData.scoreValue);
        }
        else if (choice == 1)
        {
            Snake.SetActive(true);
            GnomeMinigameManager.Instance.AddScore(SnakeData.scoreValue);
        }
        else if (choice == 2)
        {
            Gnome.SetActive(true);
            GnomeMinigameManager.Instance.AddScore(GnomeData.scoreValue);
        }
        _sprite.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;// SetActive(false);
        
        GnomeMinigameManager.Instance.Respawn(gameObject.transform.root.gameObject);
    }

   
    // ty < 3 https://forum.unity.com/threads/random-numbers-with-a-weighted-chance.442190/
    
    // [.6, .5, .3, .2]
    // 16
    // 6 / 16
    // 5 / 16
    // 3 / 16
    // 2 / 16
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

