using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class GnomeMinigameManager : MinigameManager
{

    public Texture2D cursorTexture;
    // all gnome minigame logic here


    public float time = 20f;
    
    protected void Start()
    {
       base.Start();
       Debug.Log(_flowManagerInstance.playerID);
       //Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

       StartCoroutine(TimerComplete());
    }

    IEnumerator TimerComplete()
    {
        yield return new WaitForSeconds(20f);
       _flowManagerInstance.MinigameComplete();
    }
    
}
