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

    public GameObject Coin;
    public SpawnedObjectData CoinData;
    public GameObject Snake;
    public SpawnedObjectData SnakeData;
    public GameObject Gnome;
    public SpawnedObjectData GnomeData;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _rockAnimator = GetComponent<Animator>();
    }

    IEnumerator SelectSpawnedItem(float numberOfSeconds){
        
        
        yield return new WaitForSeconds(numberOfSeconds);
        float[] weights = {CoinData.probability, SnakeData.probability, GnomeData.probability};
        int choice = GetRandomWeightedIndex(weights);
        if (choice == 0)
        {
            Coin.SetActive(true);
            Animator coinAnimator = Coin.GetComponent<Animator>();
            coinAnimator.Play("coinSpin");
            GnomeMinigameManager.Instance.AddScore(CoinData.scoreValue);
            GnomeMinigameManager.Instance.CoinFeedback.PlayFeedbacks();
        }
        else if (choice == 1)
        {
            Snake.SetActive(true);
            Animator snakeAnimator = Snake.GetComponent<Animator>();
            snakeAnimator.Play("redSnakeAnim");
            GnomeMinigameManager.Instance.AddScore(SnakeData.scoreValue);
            GnomeMinigameManager.Instance.SnakeFeedback.PlayFeedbacks();
        }
        else if (choice == 2)
        {
            Gnome.SetActive(true);
            Animator gnomeAnimator = Gnome.GetComponent<Animator>();
            gnomeAnimator.Play("gnomeMoon");
            GnomeMinigameManager.Instance.AddScore(GnomeData.scoreValue);
            GnomeMinigameManager.Instance.GnomeFeedback.PlayFeedbacks();
        }
        _sprite.enabled = false;// SetActive(false);
        // set the lean touch manager to off too
        transform.GetComponentInParent<LeanSelectableByFinger>().enabled = false;

        GnomeMinigameManager.Instance.Respawn(gameObject.transform.root.gameObject);
    }


    public void Spawn()
    {
       _rockAnimator.SetTrigger("clicked");
       //RockBreakFeedback.PlayFeedbacks();
        StartCoroutine(SelectSpawnedItem(.8f));
       // try that? 
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

