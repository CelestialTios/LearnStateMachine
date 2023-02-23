using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public float smoothingTime = 0.3f;
    
    private Rigidbody rb;
    private Vector3 smoothVelocity = Vector3.zero;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 desiredVelocity = Vector3.Normalize(new Vector3(h, 0f, v)) * speed;
        rb.velocity = Vector3.SmoothDamp(rb.velocity, desiredVelocity, ref smoothVelocity, smoothingTime);
    }
}
