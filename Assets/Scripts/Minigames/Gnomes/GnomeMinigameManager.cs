using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

using MoreMountains.Tools;
public class GnomeMinigameManager : MMSingleton<GnomeMinigameManager>
{

    protected GameFlowManager _flowManagerInstance;
    public Texture2D cursorTexture;
    // all gnome minigame logic here

    private int _score = 0;
    public int Score => _score;

    public float roundTime = 20f;

    private Coroutine _timer;
    protected void Start()
    {
       _flowManagerInstance = GameFlowManager.Instance;
       Debug.Log(_flowManagerInstance.playerID);
       //Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

       _timer = StartCoroutine(TimerComplete());
    }

    public void AddScore(int score)
    {
        _score += score;
        if (_score < 0)
        {
            StopCoroutine(_timer);
            _flowManagerInstance.MinigameComplete();
        }
    }

    IEnumerator TimerComplete()
    {
        yield return new WaitForSeconds(roundTime);
       _flowManagerInstance.MinigameComplete();
    }
    
}
