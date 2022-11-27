using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableZone : MonoBehaviour
{
    public bool Vulnerable;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Doctor"))
        {
            // we are in the valid zone
            Vulnerable = true;
            Debug.Log("fish can be eaten");
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
