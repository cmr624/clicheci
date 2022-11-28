using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCollidesWith : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Doctor"))
        {

            GameObject dr = other.gameObject;
            dr.GetComponent<Animator>().SetTrigger("Dead");
            dr.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
            AppleDayMinigameManager.Instance.CompleteGame();
            
            // play particle effect
            Destroy(gameObject);
        } 
    }
}
