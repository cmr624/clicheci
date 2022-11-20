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

    public GameObject RespawningRockPrefab;
    protected void Start()
    {
       _flowManagerInstance = GameFlowManager.Instance;
       //Debug.Log(_flowManagerInstance.playerID);
       //Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

       _timer = StartCoroutine(TimerComplete());
    }

    public float RespawnInSecondsTimer = 4f;

    public void Respawn(GameObject go)
    {
       StartCoroutine(RespawnTimer(RespawnInSecondsTimer, go)); 
    }

    private IEnumerator RespawnTimer(float timeInSeconds, GameObject go)
    {
       yield return new WaitForSeconds(timeInSeconds);
       Instantiate(RespawningRockPrefab, go.transform.position, go.transform.rotation);
       Destroy(go);
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
        
        // ending sequence
        
        // trigger an animation. in 5 seconds, go back home
        
        
        yield return new WaitForSeconds(5f);
       _flowManagerInstance.MinigameComplete();
    }
    
}
