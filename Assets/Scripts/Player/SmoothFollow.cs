using System;
using UnityEngine;

[ExecuteInEditMode]
public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    public float depth;
    public float angle;
    
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        if (Application.isEditor && !Application.isPlaying && target != null)
        {
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.right);
            Vector3 offset = rotation * (Vector3.up * depth);
            transform.position = target.position + offset;
            transform.LookAt(target.position);
        }
    }

    private void FixedUpdate() 
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.right);
        Vector3 targetPosition = target.position + rotation * (Vector3.up * depth);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
