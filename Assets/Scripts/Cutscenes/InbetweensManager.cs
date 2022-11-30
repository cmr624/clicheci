using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    [Header("TV Heads")]
    [SerializeField] int TvHeadAnimNum= 0;
    [SerializeField] Animator TVHeadAnimator;

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


// void Start()
//     {
    

//     //    NextAnimation();
//     //    Button btn =  nextButton.GetComponent<Button>();
//     //    btn.onClick.AddListener(NextAnimation);

//     }

    // void NextAnimation() {
            //i don't think "null" will work for this bool
    //     if (WonLastGame != null) {
    //         CEOAnimation();
    //     }
    //     TVHeadAnimation();
    // }

    public void CEOAnimation() {
        
        if (GameFlowManager.Instance.WonLastGame) {
            //play Won animation
            Win_GO.SetActive(true);
            win_text_GO.SetActive(true);
            CEOWinAnimator.Play("CEO_Thumbs_Anim");
            
            //needs coroutine so it doesn't play right away
            // GreenExclamation_GO.SetActive(true);

        } else {
            //play Lose Animation
            Lose_GO.SetActive(true);
            lose_text_GO.SetActive(true);
            CEOLoseAnimator.Play("CEO_Angry_Lose");
            AngryLinesAnimator.Play("angryLinesAnim");
            
            //needs coroutine so it doesn't play right away
            //RedExclamation_GO.SetActive(true);
        }

    }

    public void TVHeadAnimation() {
        Win_GO.SetActive(false);
            win_text_GO.SetActive(false);
            Lose_GO.SetActive(false);
            lose_text_GO.SetActive(false);
        TvHeadAnimNum = Random.Range(1, 5);

         TVHeadAnimator.SetInteger("tvheadnum", TvHeadAnimNum);

    }
}
