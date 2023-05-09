using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyer : MonoBehaviour
{
    public float destroyYPosition = -6f;

    void Update()
    {
        if (transform.position.y < destroyYPosition)
        {
            Destroy(gameObject);
        }
    }
}
