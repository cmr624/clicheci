using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Lean.Touch;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;

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
    //public SpawnedObjectData CoinData;
//    float coinPoints = 20f;

    [Header("Snakes")]
    public GameObject Snake;
    //public SpawnedObjectData SnakeData;
    // float snakePoints = -20f;

    [Header("Gnomes")]
    public GameObject Gnome;
    //public SpawnedObjectData GnomeData;
    // float gnomePoints = -10f;


    public float RespawnTimer = 4f;

    private Coroutine DeathCounter;
    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _rockAnimator = GetComponent<Animator>();
        _gnomeGameManager = GetComponent<GnomeMinigameManager>();

        // set automatic respawn timer
        DeathCounter = StartCoroutine(DeathTimer(Random.Range(RespawnTimer - 1, RespawnTimer)));
        
        ChooseColorValue();
        // choose the value of the rock
        ChooseRockValue();

        //LeanTween.scale(gameObject, new Vector3(1.1f, 1.1f, 1f), 1f).setLoopPingPong(-1);
    }

    [SerializeField]
    public GuessableObjectData[] ColorValueData;

    public GuessableObjectData CurrentColor;
    private void ChooseColorValue()
    {
        float[] weights = GetWeights(ColorValueData);
        CurrentColor = ColorValueData[GetRandomWeightedIndex(weights)];
        LeanTween.color(gameObject, CurrentColor.Color, Random.Range(1f, 2.0f));
        //_sprite.color = CurrentColor.Color;
    }

    private float[] GetWeights(GuessableObjectData[] DataArray)
    {
        List<float> weights = new List<float>();
        foreach (GuessableObjectData data in DataArray)
        {
            weights.Add(data.Weight);
        }

        return weights.ToArray();
    }

    // color based state
    
    // a color contains weights
    // red = .4 gnome, .1 snake, .5 coin
    // yellow = .25 snake, .10 gnome, .65 coin
    // green = .9 coin, .01 gnome, .09 snake
    
    // however, how do we choose the red, yellow, green rates 
    // so perhaps new data becomes:
    // - color
    // - weight of color
    // - data set
    //  - the data set contains 3 weights

    
    
    public IEnumerator DeathTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        DestroyRockAndRespawn();
    }
    
    public enum RockContains
    {
       Coin,
       Snake,
       Gnome
    }

    public void ChooseRockValue()
    {
       // float[] weights = {CoinData.probability, SnakeData.probability, GnomeData.probability};

        List<float> weights = new List<float>();

        foreach (var data in CurrentColor.DataSet)
        {
            weights.Add(data.probability);
        }
       
        // TODO note we're hard coding the coin as the first thing in the dataset array, snake second, etc. this is bad news bears
        int choice = GetRandomWeightedIndex(weights.ToArray());

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
        DestroyRockAndRespawn();
    }

    private void DestroyRockAndRespawn()
    {
        _sprite.enabled = false; // SetActive(false);
        DisableClick();

        GnomeMinigameManager.Instance.Respawn(gameObject.transform.root.gameObject);
    }

    private void DisableClick()
    {
        //transform.GetComponentInParent<MMWiggle>().enabled = false;
        // set the lean touch manager to off too
        transform.GetComponentInParent<LeanSelectableByFinger>().enabled = false;
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
        
        GnomeMinigameManager.Instance.AddScore(CurrentColor.DataSet[0].scoreValue);
        GnomeMinigameManager.Instance.CoinFeedback.PlayFeedbacks();

        //Debug.Log("Coin earned " + CoinData.scoreValue + " points");
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

        //TODO some primo shit code right here
        GnomeMinigameManager.Instance.AddScore(CurrentColor.DataSet[2].scoreValue);
        //Debug.Log("Gnome earned " + GnomeData.scoreValue + " points");
        GnomeMinigameManager.Instance.GnomeFeedback.PlayFeedbacks();
    }

    private void DisplaySnake()
    {
        Snake.SetActive(true);
        Animator snakeAnimator = Snake.GetComponent<Animator>();
        snakeAnimator.Play("redSnakeAnim");
        //TODO add score back in

        GnomeMinigameManager.Instance.AddScore(CurrentColor.DataSet[1].scoreValue);
        //Debug.Log("Snake earned " + SnakeData.scoreValue + " points");

        GnomeMinigameManager.Instance.SnakeFeedback.PlayFeedbacks();
    }


    public void BreakRock()
    {
        StopCoroutine(DeathCounter);
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

