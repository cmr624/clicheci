using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class FishMinigameManager :MMSingleton<FishMinigameManager>
{
    protected GameFlowManager _flowManagerInstance;

    public Bear Bear;

    public VulnerableZone VulnerableZone;

    public LeanTweenPath JumpArc;

    protected MMMultipleObjectPooler _pooler;

    public int Lives;

    public float timerInSeconds;
    protected void Start()
    {
        _flowManagerInstance = GameFlowManager.Instance;
        _pooler = gameObject.GetComponent<MMMultipleObjectPooler>();
        Spawner();
        StartCoroutine(StartTimer());
    }

    protected IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timerInSeconds);
        GameOver = true;
        GameOverSequence();
    }

    public void GameOverSequence()
    {
        if (Lives > 0) {
             _flowManagerInstance.WonLastGame = true;
        }  else {
            _flowManagerInstance.WonLastGame = false;
        }
       _flowManagerInstance.MinigameComplete(); 
    }


    public float timeBetweenSpawns =0.5f;
    protected void Spawner()
    {
        StartCoroutine(Spawning());
    }

    [HideInInspector]
    public bool GameOver = false;
    
    private IEnumerator Spawning()
    {
        while (!GameOver)
        {
           // TODO randomize this!!!
           yield return new WaitForSeconds(timeBetweenSpawns);
           GameObject nextSpawnedGameObject = _pooler.GetPooledGameObject();
           nextSpawnedGameObject.gameObject.SetActive(true);
           nextSpawnedGameObject.gameObject.GetComponent<MMPoolableObject>().TriggerOnSpawnComplete();     
        }
    }
    // create a spawner
    // have 3 pools of objects
    
    // trash - 10% chance, slow
    // yummy fish - 70% chance, pretty fast
    // big scary fish - 20 % chance, normal speed
    
    // rate of spawning items
    
    // pick one at random based on probability

    // each object needs its own speed
   
    // ugh ugh guhghhgh

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Bear.Swipe();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Bear.Dodge();
        }

        
    }
}
