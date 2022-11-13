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
    // 50% chance for coin
    // 25% chance for gnome
    // 25% chance for snake
    public float coinProbability = .6f;
    public float gnomeProbability = .7f;
    public float snakeProbability = .3f;


    private SpriteRenderer _sprite;

    public Sprite Coin;
    public Sprite Gnome;
    public Sprite Snake;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void Spawn()
    {
        float p = Random.Range(0f, 1f);
        Debug.Log(p);
        if (p < coinProbability)
        {
            // coin
            _sprite.sprite = Coin;
        }
        else if (p >= coinProbability && p < gnomeProbability)
        {
            // gnome
            _sprite.sprite = Gnome;
        }
        else
        {
            // snake
            _sprite.sprite = Snake;
        }
    }
    
    

   
}
