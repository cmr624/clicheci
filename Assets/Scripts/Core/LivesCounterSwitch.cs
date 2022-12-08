using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesCounterSwitch : MonoBehaviour
{
    public int NumberOfLives;
    public GameObject[] ImagesArray;

    private List<Image> _imagesArr = new List<Image>();
    private int _currentIndex;

    private void Start()
    {
        _currentIndex = ImagesArray.Length - 1;
        foreach (var go in ImagesArray)
        {
           _imagesArr.Add(go.GetComponent<Image>()); 
        }
    }

    public void RemoveLife()
    {
        if (NumberOfLives > 0)
        {
            _imagesArr[_currentIndex].enabled = false;
           _currentIndex--;
           NumberOfLives--;           
        }
    }
    
    public void ResetImages()
    {
        foreach (var img in _imagesArr)
        {
           img.enabled = (true); 
        }
    }
}
