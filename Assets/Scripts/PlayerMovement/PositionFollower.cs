using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFollower : MonoBehaviour
{

    public Transform TargetTransform;
    public Vector3 Offset;


   

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = TargetTransform.position + Offset;
    }
}
