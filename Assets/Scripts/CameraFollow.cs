using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetOffset;
    [SerializeField] private float camMove = 4;

    void LateUpdate()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        //normal movement > transform.position = target.position + targetOffset;
        //
        //movement with easing
        transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, camMove * Time.deltaTime);
    }
}
