using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongPath : MonoBehaviour
{

    public LeanTweenPath path;
    // Start is called before the first frame update
    void Start()
    {
        LTDescr tween = LeanTween.move(gameObject, path.vec3, 1f)
            .setDelay(1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
