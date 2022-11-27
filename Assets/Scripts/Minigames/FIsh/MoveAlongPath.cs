using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongPath : MonoBehaviour
{

    public LeanTweenPath path;

    [HideInInspector]
    public bool Vulnerable = false;
    // Start is called before the first frame update
    void Start()
    {
        LTDescr tween = LeanTween.move(gameObject, path.vec3, 3f)
            .setDelay(.1f);
    }

   
}
