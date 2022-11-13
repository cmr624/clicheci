using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeMinigameManager : MinigameManager
{

    public Texture2D cursorTexture;
    // all gnome minigame logic here

    protected void Start()
    {
       base.Start();
       Debug.Log(_flowManagerInstance.playerID);
       StartCoroutine(Complete());
       Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    IEnumerator Complete()
    {
        yield return (new WaitForSeconds(5f));
        _flowManagerInstance.MinigameComplete();
    }

    
}
