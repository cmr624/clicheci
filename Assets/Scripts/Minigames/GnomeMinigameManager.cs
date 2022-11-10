using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeMinigameManager : MinigameManager
{
    
    // all gnome minigame logic here

    protected void Start()
    {
       base.Start();
       Debug.Log(_flowManagerInstance.playerID);
       StartCoroutine(Complete());
    }

    IEnumerator Complete()
    {
        yield return (new WaitForSeconds(5f));
        _flowManagerInstance.MinigameComplete();
    }

    
}
