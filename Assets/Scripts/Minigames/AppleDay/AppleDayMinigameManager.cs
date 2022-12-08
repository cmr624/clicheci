using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using Random = UnityEngine.Random;

public class AppleDayMinigameManager : MMSingleton<AppleDayMinigameManager>
{

    protected GameFlowManager _flowManagerInstance;
    
    public GameObject Doctor;
    public MMF_Player DoctorSFX;
    public MMF_Player DoctorSFXDies;

    public GameObject AppleGun;
    public int MaxAmmo = 1;

    public DoctorSequenceData[] SequenceDatas;
    private DoctorSequenceData SequenceData;

    // left doors
    private Transform _4l;
    private Transform _3l;
    private Transform _2l;
    private Transform _1l;
    
    // right doors
    private Transform _4r;
    private Transform _3r;
    private Transform _2r;
    private Transform _1r;
    
    
    [HideInInspector]
    public bool OutOfAmmo = false;
    // Start is called before the first frame update
    void Start()
    {
        _flowManagerInstance = GameFlowManager.Instance;
        
        // decided to hard code this 
        // find left doors
        _4l = GameObject.Find("4l").transform;
        _3l = GameObject.Find("3l").transform;
        _2l = GameObject.Find("2l").transform;
        _1l = GameObject.Find("1l").transform;
        
        // find right doors
        _4r = GameObject.Find("4r").transform;
        _3r = GameObject.Find("3r").transform;
        _2r = GameObject.Find("2r").transform;
        _1r = GameObject.Find("1r").transform;


        float f = Random.Range(0f, 1f);
        if (f < .3f)
        {
            SequenceData = SequenceDatas[0];
        }
        else if (f < .6f && f >= .3f)
        {
            SequenceData = SequenceDatas[1];
        }
        else
        {
            SequenceData = SequenceDatas[2];
        }
        StartCoroutine(IterateSequence());

        GameOver = false;
    }

    
    // a helper function to translate the string of the door to the gameobject. it's not great
    private Transform FindDoor(string str)
    {
        if (str == ("4l"))
        {
            return _4l;
        }
        if (str == "3l")
        {
            return _3l;
        }
        if (str == "2l")
        {
            return _2l;
        }
        if (str == "1l")
        {
            return _1l;
        }

        if (str == "4r")
        {
            return _4r;
        }
        if (str == "3r")
        {
            return _3r;
        }
        if (str == "2r")
        {
            return _2r;
        }

        if (str == "1r")
        {
            return _1r;
        }

        return _1r;
    }

    void Update()
    {
        AppleShoot();
    }

    // the manager function that shoots the apple, it's acting as the gun here.
    public void AppleShoot()
    {
        // 
        if (Input.GetButtonDown("Jump") && !OutOfAmmo)
        {
            // we have to call the actual shoot function on the component itself. This is the prefab.
            AppleGun.GetComponent<AppleGunShoot>().Shoot();
            
            // do ammo logic
            MaxAmmo--;
            if (MaxAmmo <= 0)
            {
                OutOfAmmo = true;
            }
        }
    }

    
    
    public void CompleteGame()
    {
        // Debug.Log("GAME COMPLETE")
        GameOver = true;
        StartCoroutine(EndGame(5f));
    }

    private IEnumerator EndGame(float seconds)
    {
       yield return new WaitForSeconds(seconds);
       _flowManagerInstance.GameOver = true;
       _flowManagerInstance.score++;
       _flowManagerInstance.MinigameComplete();
    }

    public void StartSequence()
    {
 
    }

    public float GetDistanceBetweenTwoDoors(string door1, string door2)
    {
        return Vector3.Distance(FindDoor(door1).position, FindDoor(door2).position);
    }

    public bool GameOver = false;
    
    // this goes through our sequence data class and for loops through each step
    private IEnumerator IterateSequence()
    {
        // for every step in the current sequence
        foreach (var STEP in SequenceData.Sequence)
        {
            // wait to spawn the alotted time
            yield return new WaitForSeconds(STEP.TimerToSpawn);
            // cache the sprite renderer for hte doctor since we need to set it up;
            SpriteRenderer DrSr = Doctor.GetComponent<SpriteRenderer>();
            
            // make sure it's enabled (it gets disabled after it reaches a destination door)
            DrSr.enabled = true;
            LeanTween.alpha(Doctor, 1f, .1f);
            // set the spawn position, from a string in the StepData called LocationFrom
            Doctor.transform.position = FindDoor(STEP.LocationFrom).position;
            
            // determine how much time to take from A to B
            float d = GetDistanceBetweenTwoDoors(STEP.LocationFrom, STEP.LocationTo);
            // distance = rate * time
            // rate = distance / time;

            float r = d / STEP.WalkingTime;
            
            // if the location we're starting from is on the LEFT side, we need to go RIGHT.
            if (STEP.LocationFrom.Contains("l"))
            {
                // we're going right, so we need to be flipped
                DrSr.flipX = true;
            }
            else
            {
                r *= -1;
                // we're going left, so we need to not be flipped
                DrSr.flipX = false;
            }
            // set the velocity of the doctor
            Doctor.GetComponent<Rigidbody2D>().velocity = new Vector2(r, 0f);
            // scale up the doctor if needed
            Doctor.transform.localScale = (Doctor.transform.localScale * STEP.ScaleChange);
            
            // wait until the alotted walking time
            yield return new WaitForSeconds(STEP.WalkingTime);
            // then disable the doctor to mimic him going behind the door
            if (_instance.GameOver)
            {
                yield break;
            }
            
            LeanTween.alpha(Doctor, 0f, .05f).setOnComplete(() =>
            {
                DrSr.enabled = false;
            });
        }
        // you lose! because you didnt get the guy in time
        // play da game over screen
        GameOverAnimation();
        CompleteGame();
    }

    public GameObject GameOverDoctor;
    public void GameOverAnimation()
    {
        if (!_instance.GameOver)
        {
           GameOverDoctor.gameObject.SetActive(true); 
        }
    }
}
