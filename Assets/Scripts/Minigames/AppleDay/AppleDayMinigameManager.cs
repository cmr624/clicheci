using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class AppleDayMinigameManager : MMSingleton<AppleDayMinigameManager>
{


    public List<Transform> LeftDoors;
    public List<Transform> RightDoors;
    
    protected GameFlowManager _flowManagerInstance;

    public GameObject AppleGun;
    // Start is called before the first frame update
    void Start()
    {
        _flowManagerInstance = GameFlowManager.Instance;
    }

    void Update()
    {
        AppleShoot();
    }

    public void AppleShoot()
    {
        bool fired = false;
        if (Input.GetButtonDown("Fire1") && !fired)
        {
            AppleGun.GetComponent<AppleGunShoot>().Shoot();
            fired = true;
        }
    }

    public void CompleteGame()
    {
        _flowManagerInstance.MinigameComplete();
    }
    
    public GameObject DoctorPrefab;
    public void DoctorSpawn()
    {
        
    }

    private void DoctorMove()
    {
        
    }
}
