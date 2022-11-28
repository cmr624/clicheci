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
    protected void Start()
    {
        _flowManagerInstance = GameFlowManager.Instance;
        _pooler = GetComponent<MMMultipleObjectPooler>();
    }


    public float timeBetweenSpawns;
    protected void Spawner()
    {
        StartCoroutine(Spawning());
    }

    public bool GameOver = false;
    
    private IEnumerator Spawning()
    {
       yield return new WaitForSeconds(timeBetweenSpawns);
       _pooler.GetPooledGameObject()
           .SetActive(true);
       yield return new WaitForSeconds(timeBetweenSpawns);
       _pooler.GetPooledGameObject().SetActive(true);

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
