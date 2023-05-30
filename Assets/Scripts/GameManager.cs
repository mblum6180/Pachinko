using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Animator lightFlash; // Animator for the flashing light effect

    public int ballCount = 250; // Starting ball count
    public TextMeshProUGUI ballCountText;  // Reference to your TextMeshPro text field

    // Points per ball
    [SerializeField]
    public int pointsToAdd = 7;

    void Awake()
    {
        // Set up the singleton
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            UpdateBallCountText();  // Initialize the text when the game starts
        }
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            // If you are in the editor, this will stop the play mode
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            // If you are in a build of your game, this will quit the application
            Application.Quit();
            #endif
        }
    }

    public void DecreaseBallCount()
    {
        ballCount--;
        UpdateBallCountText();  // Update the text whenever the ball count changes
    }

    private void UpdateBallCountText()
    {
        ballCountText.text = "Ball Count: " + ballCount;  // Set the text to display the current ball count
    }

     public void AddBallCount(int ballsToAdd)
    {
        // Increase the ball count and update the UI
        ballCount += ballsToAdd;
    }

    public void FlashEffect()
{
    if (lightFlash)
    {
        // Play the "Light" animation from the beginning
        lightFlash.Play("Light", 0, 0f);
    }
    else
    {
        Debug.LogWarning("No flashAnimator assigned in GameManager!");
    }
}

}