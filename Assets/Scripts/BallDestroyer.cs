using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyer : MonoBehaviour
{
    public float zPosition = 10f;  // The Z position to set
    public float destroyDelay = 0.5f;  // The delay before destroying the ball
    public float downwardImpulse = 5f;  // The magnitude of the downward impulse

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Change the Z position of the ball
            Vector3 position = collision.gameObject.transform.position;
            position.z = zPosition;
            collision.gameObject.transform.position = position;

            // Disable the collider on the ball
            Collider2D collider = collision.gameObject.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            // Apply a downward impulse to the ball
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(Vector2.down * downwardImpulse, ForceMode2D.Impulse);
            }

            // Destroy the ball after a delay
            Destroy(collision.gameObject, destroyDelay);
        }
    }
}
