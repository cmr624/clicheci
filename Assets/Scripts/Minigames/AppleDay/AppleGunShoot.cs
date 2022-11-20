using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleGunShoot : MonoBehaviour
{
    public GameObject AppleProjectile;
    // Start is called before the first frame update
    public void Shoot()
    {
        GameObject Apple = Instantiate(AppleProjectile, transform.position, transform.rotation);
        Apple.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1100f));
    }
}
