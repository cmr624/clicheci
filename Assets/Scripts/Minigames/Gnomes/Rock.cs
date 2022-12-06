using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Lean.Touch;
using MoreMountains.Feedbacks;

public class Rock : MonoBehaviour
{
     // click on a stone
    // a stone can have 3 states with varied probability
    // 60% chance for coin
    // 10% chance for gnome
    // 20% chance for snake
    private SpriteRenderer _sprite;
    private Animator _rockAnimator;

    private GnomeMinigameManager _gnomeGameManager;

    // [Header("SCORING")]
    // public float addToScore;

    [Header("Coins")]
    public GameObject Coin;
    public SpawnedObjectData CoinData;
//    float coinPoints = 20f;

    [Header("Snakes")]
    public GameObject Snake;
    public SpawnedObjectData SnakeData;
    // float snakePoints = -20f;

    [Header("Gnomes")]
    public GameObject Gnome;
    public SpawnedObjectData GnomeData;
    // float gnomePoints = -10f;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _rockAnimator = GetComponent<Animator>();
        _gnomeGameManager = GetComponent<GnomeMinigameManager>();

        ChooseRockValue();
    }


    
    
    public enum RockContains
    {
       Coin,
       Snake,
       Gnome
    }

    public void ChooseRockValue()
    {
        float[] weights = {CoinData.probability, SnakeData.probability, GnomeData.probability};
        int choice = GetRandomWeightedIndex(weights);

        if (choice == 0)
        {
            currentRockVal = RockContains.Coin;
        }
        else if (choice == 1)
        {
            currentRockVal = RockContains.Snake;
        }
        else if (choice == 2)
        {
            currentRockVal = RockContains.Gnome;
        }
    }

    public RockContains currentRockVal; 
    
    // when it's clicked, display the correct value and change the score
    IEnumerator SelectSpawnedItem(float numberOfSeconds){
        yield return new WaitForSeconds(numberOfSeconds);
        ChooseDisplay();
        
        // disable the sprite
        _sprite.enabled = false;// SetActive(false);
        // set the lean touch manager to off too
        transform.GetComponentInParent<LeanSelectableByFinger>().enabled = false;

        GnomeMinigameManager.Instance.Respawn(gameObject.transform.root.gameObject);
    }

    private void ChooseDisplay()
    {
        if (currentRockVal == RockContains.Coin)
        {
            // coin has been randomly selected
            currentRockVal = RockContains.Coin;
            DisplayCoin();
        }
        else if (currentRockVal == RockContains.Snake)
        {
            // snake has been randomly selected
            DisplaySnake();
        }
        else if (currentRockVal == RockContains.Gnome)
        {
            // gnome has been randomly selected
            DisplayGnome();
        }
    }

    private void DisplayCoin()
    {
        Coin.SetActive(true);
        Animator coinAnimator = Coin.GetComponent<Animator>();
        coinAnimator.Play("coinSpin");
        GnomeMinigameManager.Instance.AddScore(CoinData.scoreValue);
        GnomeMinigameManager.Instance.CoinFeedback.PlayFeedbacks();

        Debug.Log("Coin earned " + CoinData.scoreValue + " points");
    }
    private void DisplayGnome()
    {
        Gnome.SetActive(true);
        Animator gnomeAnimator = Gnome.GetComponent<Animator>();

        float f = Random.Range(0f, 4f);

        if (f < 1f)
        {
            gnomeAnimator.SetTrigger("Scary");
        }
        else if (f >= 1f && f < 2f)
        {
            gnomeAnimator.SetTrigger("Moon");
        }
        else if (f >= 2f && f < 3f)
        {
            gnomeAnimator.SetTrigger("Drunk");
        }
        else
        {
            gnomeAnimator.SetTrigger("Happy");
        }

        GnomeMinigameManager.Instance.AddScore(GnomeData.scoreValue);
        //Debug.Log("Gnome earned " + GnomeData.scoreValue + " points");
        GnomeMinigameManager.Instance.GnomeFeedback.PlayFeedbacks();
    }

    private void DisplaySnake()
    {
        Snake.SetActive(true);
        Animator snakeAnimator = Snake.GetComponent<Animator>();
        snakeAnimator.Play("redSnakeAnim");
        GnomeMinigameManager.Instance.AddScore(SnakeData.scoreValue);
        //Debug.Log("Snake earned " + SnakeData.scoreValue + " points");

        GnomeMinigameManager.Instance.SnakeFeedback.PlayFeedbacks();
    }


    public void BreakRock()
    {
       _rockAnimator.SetTrigger("clicked");
       GnomeMinigameManager.Instance.RockBreakFeedback.PlayFeedbacks();
       StartCoroutine(SelectSpawnedItem(.8f));
       // try that? 
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
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

