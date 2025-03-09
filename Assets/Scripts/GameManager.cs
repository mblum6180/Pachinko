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

    public GameObject tooltip;  // Tooltip object
    private float inactiveTime = 4f;  // How long the player has been inactive before displaying the tooltip; 
    private bool hasPlayerShot = false;  // Tracks if the player is currently active

    // Add a new list to keep track of all Ball instances
    public List<GameObject> ballInstances = new List<GameObject>();

    public GameObject gameover;
    public bool IsGameOver = false;


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
        gameover.SetActive(false);  // Hide the Game Over text when the game starts
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

        // If the player is inactive
        if (!hasPlayerShot)
        {
            // Decrease the inactive time
            inactiveTime -= Time.deltaTime;

            //   If the inactive time is less than or equal to 0,
            if (inactiveTime <= 0f)
            {
                // Show the tooltip
                tooltip.SetActive(true);
            }
        }
    }

    public void DecreaseBallCount()
    {
        ballCount--;
        UpdateBallCountText();  // Update the text whenever the ball count changes
    }

    private void UpdateBallCountText()
    {
        if(ballCountText != null)  // Make sure the text field is not null and the ball count is greater than or equal to 0
            {
                ballCountText.text = "Balls: " + ballCount;  // Set the text to display the current ball count
            }
    }

     public void AddBallCount(int ballsToAdd)
    {
        // Increase the ball count and update the UI
        ballCount += ballsToAdd;
        UpdateBallCountText();  // Update the text whenever the ball count changes
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

    public void RemoveBallInstance(GameObject ball)
    {
        if(ballInstances.Contains(ball))
        {
            ballInstances.Remove(ball);
        }
    }

    public void PlayerActivity()
    {
        // The player has interacted with the game, so set them as active, reset the inactive time, and hide the tooltip
        hasPlayerShot = true;
        inactiveTime = 0;
        tooltip.SetActive(false);
    }

    public void GameOver()
    {
        if (ballCount <= 0 && ballInstances.Count == 0)
        {
            gameover.SetActive(true);
            IsGameOver = true;
        }
    }
}