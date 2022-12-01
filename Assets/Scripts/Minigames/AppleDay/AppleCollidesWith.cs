using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AppleCollidesWith : MonoBehaviour
{
    public UnityEvent OnExplode;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Doctor"))
        {
            GameObject dr = other.gameObject;
            dr.GetComponent<Animator>().SetTrigger("Dead");
            
            GameObject AppleTo = GameObject.Find("AppleTo");
            LeanTween.move(dr, AppleTo.transform.position, 1.5f).setEaseOutCubic();
           
            dr.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //put 
            OnExplode.Invoke();
            AppleDayMinigameManager.Instance.CompleteGame();
            
            // play particle effect
            Destroy(gameObject);
        } 
    }
}
