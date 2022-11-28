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
    [SerializeField] int animInt;
    // [SerializeField] AudioSource audioSource;
    // [SerializeField] AudioClip thisAudioClip;

   public Button nextButton;

    void Start()
    {
       Button btn =  nextButton.GetComponent<Button>();
       btn.onClick.AddListener(NextAnimation);

    }

    // void Update()
    // {
        
    // }

    void NextAnimation() {
         //on click, trigger next animation
        cutsceneAnimator.SetInteger("AnimNum", animInt++);
    
        AnimationEffects(animInt);
    
    }

    void AnimationEffects(int animationParameter) {
 
        //play sfx or begin anything else w
        switch (animationParameter)
        {
        case 1:
            //first sound
            Debug.Log("first sound");
            break;
        case 2:
            //second sound
            Debug.Log("second sound");
            break;
        case 3:
            //third sound
            Debug.Log("third sound");
            break;
        case 4:
            //4th sound
            Debug.Log("fourth sound");
            break;
        case 5:
            //final sound
            Debug.Log("fifth sound");
            break;

        case 6:
            //load new scene
            SceneManager.LoadScene("GnomeMinigameScene");
            break;

        default:
    
            break;
        }

    }
}
