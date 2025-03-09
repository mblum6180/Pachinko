using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public FlipperController leftFlipperController;
    public FlipperController rightFlipperController;
    private bool isUp = false; // Track the current state of the flipper

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (isUp)
            {
                leftFlipperController.FlipDown();
                rightFlipperController.FlipDown();
            }
            else
            {
                leftFlipperController.FlipUp();
                rightFlipperController.FlipUp();
            }
            // Flip the state
            isUp = !isUp;
        }
    }
}