using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Vector3 offset = new Vector3 (0, 0, 0);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, Mathf.Infinity, Time.smoothDeltaTime);
    }
}
