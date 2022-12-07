using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBezierNodeMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveLocalX(gameObject, transform.position.x-1f, 2f)
            .setLoopPingPong()
            .setEaseInCubic();
    }
}
