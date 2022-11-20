using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "/Assets/DoctorSequenceData")]
public class DoctorSequenceData : ScriptableObject
{
    [SerializeField]
    int locations;
    [SerializeField]
    List<float> TimersPerSequence;

}
