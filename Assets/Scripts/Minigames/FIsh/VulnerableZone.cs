using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableZone : MonoBehaviour
{

    protected Bear b;
    protected Animator bAnimator;
    public bool Vulnerable;

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
                other.gameObject.SetActive(false);
                // TODO set some particle effect here
                //bAnimator.SetBool("isSwiping", true);
            }
        }

        if (other.CompareTag("BigFishy"))
        {
            // eat the bear
            bAnimator.SetBool("isHurt", true);
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
