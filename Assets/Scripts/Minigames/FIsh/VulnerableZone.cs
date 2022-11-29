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
    
    private void Start()
    {
        b = FishMinigameManager.Instance.Bear;
        bAnimator = b.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Doctor"))
        {
            // we are in the valid zone
            Vulnerable = true;
            Debug.Log("fish can be eaten");

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
            if (!b.IsDodging && !b.IsSwiping)
            {
                // bear gets eaten
                b.Ouchy();
                other.gameObject.SetActive(false);
            }else if (b.IsSwiping)
            {
                // swatted away
                other.gameObject.SetActive(false);
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
