using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 5f;

    public float jumpForce = 5f;
    public float jumpCooldown = 1f;
    private float jumpTimer = 0f;
    private bool canJump = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Freeze rotation along all axes
            rb.freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime);

        

        jumpTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }

        if (jumpTimer >= jumpCooldown)
        {
            canJump = true;
        }
    }
    void Jump()
    {
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            canJump = false;
            jumpTimer = 0f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            Transform objectTransform = transform;
            Vector3 newPosition = new Vector3(-18f, 6f, 7f);
            objectTransform.position = newPosition;
        }
    }
}
