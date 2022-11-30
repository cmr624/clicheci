using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class CutsceneManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cutsceneText;
    [SerializeField] Animator cutsceneAnimator;
    [SerializeField] int animInt = 0;
    // [SerializeField] AudioSource audioSource;
    // [SerializeField] AudioClip thisAudioClip;


    //YES, I KNOW THERE ARE PROBABLY BETTER WAYS TO DO THIS - Jesse

    [Header("First Shot")]
    [SerializeField] GameObject Shot1;
    [SerializeField] Animator shot1_CEO_Anim;
    [SerializeField] GameObject shot1_graph_Anim;
    [SerializeField] GameObject shot1_text_GO;

    [Header("Second Shot")]
    [SerializeField] GameObject Shot2;
    [SerializeField] Animator shot2_CEO_Anim;
    [SerializeField] AnimationClip thinking_idea_clip;
    [SerializeField] GameObject shot2_bulb_GO;
    [SerializeField] GameObject shot2_text_GO;

    [Header("Third Shot")]
    [SerializeField] GameObject Shot3;
    [SerializeField] Animator shot3_CEO_Pacman_Anim;
    [SerializeField] GameObject shot3_CEO_GO;
    [SerializeField] GameObject shot3_logo1_GO;
    [SerializeField] GameObject shot3_logo2_GO;
    [SerializeField] GameObject shot3_text_GO;

    [Header("Fourth Shot")]
    [SerializeField] GameObject Shot4;
    [SerializeField] Animator shot4_CEO_Anim;
    [SerializeField] Animator shot4_Money_Anim;
    [SerializeField] GameObject shot4_text_GO;

    [Header("Fifth Shot")]
    [SerializeField] GameObject Shot5;
    //[SerializeField] GameObject shot5_pitchRoomDoor;
    [SerializeField] GameObject shot5_text_GO;

   public Button nextButton;

    void Start()
    {
       NextAnimation();
       Button btn =  nextButton.GetComponent<Button>();
       btn.onClick.AddListener(NextAnimation);

    }

    void NextAnimation() {
         //on click, trigger next animation
        //cutsceneAnimator.SetInteger("AnimNum", animInt++);
        animInt++;
        AnimationEffects(animInt);
    
    }

    void AnimationEffects(int animationParameter) {
 
        //play sfx or begin anything else w
        switch (animationParameter)
        {
        case 1:
            //first sound
            Shot1.SetActive(true);
            shot1_text_GO.SetActive(true);
            //HARD CODED:
            StartCoroutine(AnimationTimer_GO(2f, shot1_graph_Anim, true));

            Debug.Log("first sound");

            break;
        case 2:
            Shot1.SetActive(false);
            shot1_text_GO.SetActive(false);
            Shot2.SetActive(true);
            shot2_text_GO.SetActive(true);

            shot2_CEO_Anim.Play("Thinking_Idea");
            StartCoroutine(AnimationTimer_GO(thinking_idea_clip.length, shot2_bulb_GO, true));

            //second sound
            Debug.Log("second sound");
            break;
        case 3:
            Shot2.SetActive(false);
            shot2_text_GO.SetActive(false);
            Shot3.SetActive(true);
            shot3_text_GO.SetActive(true);
            LeanTween.moveX(shot3_CEO_GO, 14f, 4f).setDelay(1f);
            StartCoroutine(AnimationTimer_GO(2f, shot3_logo1_GO, false));
            StartCoroutine(AnimationTimer_GO(3f, shot3_logo2_GO, false));

            //third sound
            Debug.Log("third sound");
            break;
        case 4:
            Shot3.SetActive(false);
            shot3_text_GO.SetActive(false);
            Shot4.SetActive(true);
            shot4_text_GO.SetActive(true);

            //4th sound
            Debug.Log("fourth sound");
            break;
        case 5:
            Shot4.SetActive(false);
            shot4_text_GO.SetActive(false);
            Shot5.SetActive(true);
            shot5_text_GO.SetActive(true);

            //final sound
            Debug.Log("fifth sound");
            break;

        case 6:
            //load TV Head 
           // InbetweensManager.TVHeadAnimation();

            //load next scene
            SceneManager.LoadScene("GnomeMinigameScene");
            break;

        default:
    
            break;
        }

    }

    IEnumerator AnimationTimer_GO(float seconds, GameObject gameObject, bool myBool){
        
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(myBool);

    }
}
