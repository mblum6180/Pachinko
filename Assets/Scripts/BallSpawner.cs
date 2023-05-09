using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public float spawnYPosition = 4f;
    public float minXPosition = -4f;
    public float maxXPosition = 4f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBall();
        }
    }

    void SpawnBall()
    {
        float randomX = Random.Range(minXPosition, maxXPosition);
        Vector2 spawnPosition = new Vector2(randomX, spawnYPosition);
        Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
    }
}