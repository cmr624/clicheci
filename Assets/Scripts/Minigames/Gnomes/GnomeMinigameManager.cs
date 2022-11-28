using System.Collections;
using System.Collections.Generic;
using System.Timers;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;

using MoreMountains.Tools;
public class GnomeMinigameManager : MMSingleton<GnomeMinigameManager>
{

    protected GameFlowManager _flowManagerInstance;
    public Texture2D cursorTexture;
    // all gnome minigame logic here

    

    public float roundTime = 20f;

    private Coroutine _timer;

    public GameObject RespawningRockPrefab;


    public MMF_Player RockBreakFeedback;
    public MMF_Player CoinFeedback;
    public MMF_Player GnomeFeedback;
    public MMF_Player SnakeFeedback;

    [Header("Score Tracking")]
    public Image scoreFill;
    private float _score = 50;
    public float Score => _score;


    protected void Start()
    {
       _flowManagerInstance = GameFlowManager.Instance;
       //Debug.Log(_flowManagerInstance.playerID);
       //Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

        //set player score to what we want it to start writh (half way?) 
        scoreFill.fillAmount = (_score/100);
        Debug.Log("Starting Score is " + _score + " points");


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
    
    public void AddScore(float score)
    {


        _score += score;
        Debug.Log("Current score is " + _score);

        //adjust the fill amount on the scoreFill image (amount of "points")
        AdjustScoreFill();

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

    private void AdjustScoreFill() {

    //   for choppy score animation ONLY use this:
      scoreFill.fillAmount = (_score/100);
     

    //below works, but looks the same as the above. 
    //maybe add a time.DeltaTime?

    // //score percentage
    // float scorePercent= _score/100;

    // //if the score percentage isn't equal to the fill amount do this
    // if (scorePercent != scoreFill.fillAmount) {
    //     // if score% is more than the fill amount, add .01 to fill amount until it's equal
    //     if (scorePercent > scoreFill.fillAmount) {
    //         while (scoreFill.fillAmount < scorePercent) {
    //             scoreFill.fillAmount += .01f;
    //         }
    //         //if score% is less than fill amount, minus .01 until scores are equal
    //     } else if (scorePercent < scoreFill.fillAmount) {
    //         while (scoreFill.fillAmount > scorePercent) {
    //             scoreFill.fillAmount -= .01f;
    //         }
    //     } else {
    //         return;
    //     }
    // } 
    

    }
    
}
