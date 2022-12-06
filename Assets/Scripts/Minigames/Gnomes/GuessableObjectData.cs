using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "/Assets/GuessableObjectData")]
public class GuessableObjectData : ScriptableObject
{
    public string StateName;
    public Color Color;
    public float Weight;
    public SpawnedObjectData[] DataSet;
}
