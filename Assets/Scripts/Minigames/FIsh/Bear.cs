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

    public void Hide()
    {
        LeanTween.scaleY(gameObject, 2f, 2f);
    }

    public void Move()
    {
        LeanTween.moveX(gameObject, 20f, 2f);
    }
    
    public void Swipe()
    {
        if (IsDodging || IsSwiping)
        {
            return;
        }
        OnSwipe.Invoke();
        StartCoroutine(SetSwipeBack(SwipeTimeInSeconds));
    }
    
    public void Dodge()
    { 
        if (IsDodging || IsSwiping)
        {
            return;
        }
        OnDodge.Invoke();
        StartCoroutine(SetDodgeBack(DodgeTimeInSeconds));
    }
   
    private IEnumerator SetSwipeBack(float seconds)
    {
       IsSwiping = true;
       yield return new WaitForSeconds(seconds);
       IsSwiping = false;
    }
    
    private IEnumerator SetDodgeBack(float seconds)
    {
       IsDodging = true;
       yield return new WaitForSeconds(seconds);
       IsDodging = false;
    }
    
}
