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
    

    [Header("Title Screen")]
    public GameObject titleScreenGO;
    public Image titleScreenIMG;

    [Header("Score Tracking")]
    public Image scoreFill;
    public float startingScore;
    private float _score;
    public float Score => _score;
    public GameObject loseFill;
    public float highScore = 100;


    protected void Start()
    {
       
       
       _flowManagerInstance = GameFlowManager.Instance;
       //Debug.Log(_flowManagerInstance.playerID);
       //Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

       

        //set player score to what we want it to start writh (half way?) 
        _score = startingScore;
        scoreFill.fillAmount = (_score/highScore);
        Debug.Log("Starting Score is " + _score + " points");

        //display title screen first.
        StartCoroutine(DisplayTitleScreen());
       
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
            GameOver();
        }
    }

    private IEnumerator Lose() {
        loseFill.SetActive(true);
        StopCoroutine(_timer);
        yield return new WaitForSeconds(2f);
        _flowManagerInstance.MinigameComplete();
    }

    void GameOver() {
        StartCoroutine(Lose());
    }


    IEnumerator TimerComplete()
    {
        yield return new WaitForSeconds(roundTime);
        // ending sequence
       GameOver();
    }

    private void AdjustScoreFill() {

    //   for choppy score animation ONLY use this:
      scoreFill.fillAmount = (_score/highScore);
     

    // below works, but looks the same as the above. 
    // maybe add a time.DeltaTime?

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
    
    private IEnumerator DisplayTitleScreen() {

        // REFACTOR THIS
        //

        titleScreenGO.SetActive(true);
        //StartCoroutine(FadeImage(true));
        yield return new WaitForSeconds(3f);

        LeanTween.alpha(titleScreenIMG.rectTransform, 0f, 1.5f)
            .setOnComplete(() =>
            {
                titleScreenGO.SetActive(false);
                _timer = StartCoroutine(TimerComplete());
                MusicFeedback.PlayFeedbacks();
            }); 
    
    
    }

    public MMF_Player MusicFeedback;
    //don't put a coroutine in a coroutine, you dumb idiot, freaking... sheesh!!!
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                titleScreenIMG.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                titleScreenIMG.color = new Color(1, 1, 1, i);
                yield return null;
            }
            
        }
    }
}
