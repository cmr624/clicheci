using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class AppleDayMinigameManager : MMSingleton<AppleDayMinigameManager>
{


    protected GameFlowManager _flowManagerInstance;
    
    public GameObject Doctor;

    public GameObject AppleGun;
    public int MaxAmmo = 1;

    public DoctorSequenceData SequenceData;

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

        StartCoroutine(IterateSequence());
    }

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

    public void AppleShoot()
    {
        if (Input.GetButtonDown("Jump") && !OutOfAmmo)
        {
            AppleGun.GetComponent<AppleGunShoot>().Shoot();
            MaxAmmo--;
            if (MaxAmmo <= 0)
            {
                OutOfAmmo = true;
            }
        }
    }

    
    
    public void CompleteGame()
    {
        StartCoroutine(EndGame(3f));
    }

    private IEnumerator EndGame(float seconds)
    {
       yield return new WaitForSeconds(seconds); 
        _flowManagerInstance.MinigameComplete();
    }

    public void StartSequence()
    {
 
    }

    private IEnumerator IterateSequence()
    {
        yield return new WaitForSeconds(.5f);
        
        foreach (var STEP in SequenceData.Sequence)
        {
            yield return new WaitForSeconds(STEP.TimerToSpawn);
            Doctor.transform.position = FindDoor(STEP.Location).position;
            Doctor.GetComponent<Rigidbody2D>().velocity = new Vector2(STEP.Speed, 0f);
            yield return new WaitForSeconds(3f);
        }
        CompleteGame();
    }
}
