using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class AppleDayMinigameManager : MMSingleton<AppleDayMinigameManager>
{

    
    protected GameFlowManager _flowManagerInstance;
    // Start is called before the first frame update
    void Start()
    {
        _flowManagerInstance = GameFlowManager.Instance;
    }



    public GameObject DoctorPrefab;
    public void DoctorSpawn()
    {
        
        
        
    }

    private void DoctorMove()
    {
        
    }
}
