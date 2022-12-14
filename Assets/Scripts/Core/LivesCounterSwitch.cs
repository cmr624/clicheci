using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesCounterSwitch : MonoBehaviour
{
    public int NumberOfLives;
    public Image[] RedHeartArray;
    public Image[] GreyHeartArray;

    private int _currentRedHeartIndex;

    private void Start()
    {
        _currentRedHeartIndex = RedHeartArray.Length - 1;
    }

    public Image GameOverText;
    public void RemoveLife()
    {
        if (NumberOfLives > 0)
        {
            RedHeartArray[_currentRedHeartIndex].enabled = false;
            GreyHeartArray[_currentRedHeartIndex].enabled = true;
            _currentRedHeartIndex--;
            
            NumberOfLives--;           
        }
        if (NumberOfLives <= 0) 
        {
            foreach (var img in GreyHeartArray)
            {
                img.enabled = false;
            }
            GameOverText.enabled = true;
        }
    }
    
}
