using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bear : MonoBehaviour
{
    public float SwipeTimeInSeconds;
    public float DodgeTimeInSeconds;

    [HideInInspector]
    public bool IsSwiping = false;
    [HideInInspector]
    public bool IsDodging = false;

    public UnityEvent OnSwipe;
    public UnityEvent OnDodge;

    public UnityEvent OnHurt;

    protected Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _originalLoc = transform.position;
    }

    public void SwipeAnimation()
    {
        //LeanTween.scaleY(gameObject, 2f, 2f);
        _animator.SetBool("isSwiping", true);
    }

   
    
    public void Swipe()
    {
        if (IsDodging || IsSwiping)
        {
            return;
        }
        OnSwipe.Invoke();
        SwipeAnimation();
        StartCoroutine(SetSwipeBack(SwipeTimeInSeconds));
    }

    public void Ouchy()
    {
        
       _animator.SetBool("isHurt", true);
       OnHurt.Invoke();
       StartCoroutine(SetHurtBack());
    }

    public AnimationClip HurtAnimationClip;
    private IEnumerator SetHurtBack()
    {
        yield return new WaitForSeconds(HurtAnimationClip.length);
        _animator.SetBool("isHurt", false);
        FishMinigameManager.Instance.Lives--;
        if (FishMinigameManager.Instance.Lives == 0)
        {
           // game over buddy
           FishMinigameManager.Instance.GameOverSequence();
        }
    }
     
    private IEnumerator SetSwipeBack(float seconds)
    {
       IsSwiping = true;
       yield return new WaitForSeconds(seconds);
       
       _animator.SetBool("isSwiping", false);
       IsSwiping = false;
    }
    
    public void Dodge()
    { 
        if (IsDodging || IsSwiping)
        {
            return;
        }
        OnDodge.Invoke();
        Move();
        StartCoroutine(SetDodgeBack(DodgeTimeInSeconds));
    }

    protected Vector3 _originalLoc;
    
    // the dodge animation currently
    //move
    public void Move()
    {
        LeanTween.moveX(gameObject, 5f, .1f);
    }
   // and move back (tween2) 
    private void MoveBack()
    {
        LeanTween.moveX(gameObject, _originalLoc.x, .1f)
            .setOnComplete((() => { IsDodging = false; }));
    }
    private IEnumerator SetDodgeBack(float seconds)
    {
       IsDodging = true;
       yield return new WaitForSeconds(seconds);
       MoveBack();
    }

   
}
