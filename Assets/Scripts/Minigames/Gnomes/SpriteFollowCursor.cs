using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFollowCursor : MonoBehaviour
{

    public Transform sprite;
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        transform.position = mousePosition;
    }
}
