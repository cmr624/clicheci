using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VulnerableZone : MonoBehaviour
{

    protected Bear b;
    protected Animator bAnimator;
    public bool Vulnerable;

    public UnityEvent OnFishEaten;
    public UnityEvent OnBearHurt;
    private void Start()
    {
        b = FishMinigameManager.Instance.Bear;
        bAnimator = b.GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Doctor"))
        {
            // we are in the valid zone
            Vulnerable = true;
            //Debug.Log("fish can be eaten");

            if (b.IsSwiping)
            {
                //Destroy(other.gameObject);
                OnFishEaten.Invoke();
                other.gameObject.SetActive(false);
                // TODO set some particle effect here
                //bAnimator.SetBool("isSwiping", true);
            }
        }
        if (other.CompareTag("BigFishy"))
        {
            // eat the bear
            if (!b.IsDodging || b.IsSwiping)
            {
                if (b.IsSwiping)
                {
                    bAnimator.SetBool("isSwiping", false);
                }
                OnBearHurt.Invoke();
                // bear gets eaten
                b.Ouchy();
                other.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Doctor"))
        {
            // we are in the valid zone
            Vulnerable = true;
            //Debug.Log("fish can be eaten");

            if (b.IsSwiping)
            {
                //Destroy(other.gameObject);
                OnFishEaten.Invoke();
                other.gameObject.SetActive(false);
                // TODO set some particle effect here
                //bAnimator.SetBool("isSwiping", true);
            }
        }

        
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Doctor"))
        {
            // we are no longer in the valid zone
            Vulnerable = false;
            Debug.Log("fish cannot be eaten");
        } 
    }
}
