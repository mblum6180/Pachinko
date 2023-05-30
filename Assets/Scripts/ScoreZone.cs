using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    public static int pointsToAdd = 7;
    private AudioSource audioSource;


    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            // Destroy the ball
            Destroy(other.gameObject);

            // Start the Score coroutine
            StartCoroutine(Score());
        }
    }

    IEnumerator Score()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        // Play the scoring sound
        audioSource.Play();

        // Add points to the ball count
        GameManager.instance.AddBallCount(GameManager.instance.pointsToAdd);
    }
}