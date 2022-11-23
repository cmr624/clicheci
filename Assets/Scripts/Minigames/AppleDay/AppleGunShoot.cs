using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleGunShoot : MonoBehaviour
{
    public GameObject AppleProjectile;
    public Animator apple_anim;
    // Start is called before the first frame update
    public void Shoot()
    {
        apple_anim.SetBool("Firing", true);
        GameObject Apple = Instantiate(AppleProjectile, transform.position, transform.rotation);
        Apple.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1100f));
      
    }
}
