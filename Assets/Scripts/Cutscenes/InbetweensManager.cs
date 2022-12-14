using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;


public class InbetweensManager : MonoBehaviour
{
  
//list of tvheads
//list of boss inbetweens

//Logos need to appear on the TV head after they turn

//prior minigame needs to say whether they won/lost
//if won, show positive CEO interaction
//if lost show negative CEO interaction

//create order of game variants and based on that, determine which logos go in the tvhead

    [Header("Music")]
    public UnityEvent InbetweenEvent;
    public UnityEvent OutroEvent;

    [Header("TV Heads")]
    [SerializeField] int TvHeadAnimNum= 0;
    [SerializeField] Animator TVHeadAnimator;
    [SerializeField] GameObject TVHeadScene_GO;
    [SerializeField] GameObject TVHeads_GO;
    [SerializeField] float tvHeadMovement;
    [SerializeField] float tvHeadMoveTime;

    [Header("TV Head Logos")]
    [SerializeField] List<GameObject> Logos;
    

    [Header("CEO Win")]
    [SerializeField] GameObject Win_GO;
    [SerializeField] Animator CEOWinAnimator;
    [SerializeField] GameObject GreenExclamation_GO;
    [SerializeField] GameObject win_text_GO;



    [Header("CEO Lose")]
    [SerializeField] GameObject Lose_GO;
    [SerializeField] Animator CEOLoseAnimator;
    [SerializeField] Animator AngryLinesAnimator;
    [SerializeField] GameObject RedExclamation_GO;
    [SerializeField] GameObject lose_text_GO;

    [Header("Outro Anim")]
    [SerializeField] GameObject Outro_GO;
    [SerializeField] GameObject Winner_Text_GO;
    [SerializeField] GameObject Loser_Text_GO;
    [SerializeField] GameObject PlayAgain_BTN;
    [SerializeField] bool gameIsOver;

    private void Start()
    {
        Button btn =  PlayAgain_BTN.GetComponent<Button>();
        btn.onClick.AddListener(PlayAgain);
        if (GameFlowManager.Instance.GameOver)
        {
            gameIsOver = true;
        }
        
        NextAnimation();
    }
    void NextAnimation()
    {
        if (GameFlowManager.Instance.FirstInBetween)
        {
            GameFlowManager.Instance.FirstInBetween = false;
            InbetweenEvent.Invoke();
            TVHeadAnimation();
        }
        else if (gameIsOver){
            
            OutroAnimation();
        } 
        else
        {
            InbetweenEvent.Invoke();
            CEOAnimation();
        }
    }

    public void CEOAnimation() {
        
        if (GameFlowManager.Instance.WonLastGame) {
            //play Won animation
            Win_GO.SetActive(true);
            win_text_GO.SetActive(true);
            CEOWinAnimator.Play("CEO_Thumbs_Anim");
            
            StartCoroutine(AnimationTimer_GO(2f, GreenExclamation_GO, true));
        } else {
            //play Lose Animation
            Lose_GO.SetActive(true);
            lose_text_GO.SetActive(true);
            CEOLoseAnimator.Play("CEO_Angry_Lose");
            AngryLinesAnimator.Play("angryLinesAnim");
            
            //needs coroutine so it doesn't play right away
            //RedExclamation_GO.SetActive(true);
        }
         //StartCoroutine(LoadNext(1f));
         StartCoroutine(PlayTVHeads(3f));

    }

    private IEnumerator PlayTVHeads(float delay)
    {
        yield return new WaitForSeconds(delay);
        TVHeadAnimation();
    }
    
    public void TVHeadAnimation() {
        Win_GO.SetActive(false);
            win_text_GO.SetActive(false);
            Lose_GO.SetActive(false);
            lose_text_GO.SetActive(false);
        TVHeadScene_GO.SetActive(true);    
        TvHeadAnimNum = Random.Range(1, 5);    
        TVHeadAnimator.SetInteger("tvheadNum", TvHeadAnimNum);
        LeanTween.moveX(TVHeads_GO, tvHeadMovement, tvHeadMoveTime);
        
        //these are random now, but we could have the number correlate to the minigame that played before?
        GameObject logoAnim = Logos[GameFlowManager.Instance.CurrentMinigameIndexOrdered + 1];

        StartCoroutine(AnimationTimer_LogoGO(3.5f, logoAnim, true));

        //TVHeadAnimator.GetCurrentAnimatorClipInfo().GetValue(0).
    }

    void PlayAgain()
    {
        GameFlowManager.Instance.PlayAgain();
    }

    public void OutroAnimation() {
        OutroEvent.Invoke();
        Outro_GO.SetActive(true);

        StartCoroutine(AnimationTimer_GO(3f, PlayAgain_BTN,true));

//HERE!!!        //needs to reflect whether the player won at least 2 of 3 games.
        if (true) {
            Winner_Text_GO.SetActive(true);
        } else {
            Loser_Text_GO.SetActive(true);
        }


    }

    public Transform LogoTransform;

    public Camera camera;
    private void ZoomOnLogo()
    {
        Vector3 targetPosition = new Vector3(-1.62f, 3.03f, -10f);
        LeanTween.move(camera.gameObject,targetPosition, 2.8f);
        LeanTween.value(camera.gameObject, camera.orthographicSize, 0.5f, 2.8f)
            .setOnUpdate((flt) =>   {
                camera.orthographicSize = flt;
            });
        //LeanTween.clerp(camera.k)
    }
    
    IEnumerator AnimationTimer_LogoGO(float seconds, GameObject gameObject, bool myBool){
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(myBool);
        ZoomOnLogo();
        StartCoroutine(LoadNext(3f));
    }


    IEnumerator AnimationTimer_GO(float seconds, GameObject gameObject, bool myBool){
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(myBool);
        StartCoroutine(LoadNext(3f));
    }

    private IEnumerator LoadNext(float t)
    {
        yield return new WaitForSeconds(t);
        GameFlowManager.Instance.LoadNextMinigame();
    }
}
