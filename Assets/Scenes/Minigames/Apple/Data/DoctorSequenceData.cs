using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "/Assets/DoctorSequenceData")]
public class DoctorSequenceData : ScriptableObject
{
    [SerializeField] public List<Step> Sequence;
}

[Serializable]
public class Step
{
    [SerializeField]
    public string LocationFrom;
    [SerializeField] public string LocationTo;
    [SerializeField]
    public float TimerToSpawn;
    [SerializeField]
    public float ScaleChange;
    [SerializeField]
    public float WalkingTime;
}
