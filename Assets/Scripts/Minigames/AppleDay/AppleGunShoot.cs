using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;

public class AppleGunShoot : MonoBehaviour
{
    public GameObject AppleProjectile;
    public Animator apple_anim;

    public Transform SpawnPoint;

    public float TimeOfApple;
    public UnityEvent OnShoot;
    public Transform AppleTo;

    protected MMF_Player _player;

    private void Start()
    {
        _player = GetComponent<MMF_Player>();
    }

    // Start is called before the first frame update
    public void Shoot()
    {
        // the apple gun state is turned on
        apple_anim.SetBool("Firing", true);
        // then it's set to turn off after .5 seconds
        StartCoroutine(StopAnimation(.5f));
        
        _player.PlayFeedbacks();
        OnShoot.Invoke();
        // create hte apple from the prefab
        GameObject Apple = Instantiate(AppleProjectile, SpawnPoint.position, SpawnPoint.rotation);
        // actually shoot the apple
        //Apple.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, Speed));
        LeanTween.move(Apple, AppleTo, TimeOfApple);
        
        GameObject AppleScale = Apple.gameObject;
        // scale hte apple down over the scale of (currently) 6 seconds
        LeanTween.scale(AppleScale, Vector3.zero, TimeOfApple);
    }

    private void StopFiring()
    {
        apple_anim.SetBool("Firing", false);
    }
    
    private IEnumerator StopAnimation(float seconds)
    {
       yield return new WaitForSeconds(seconds);
       StopFiring();
    }
}
