using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public BallSpawner ballSpawner;
    public HighScoreManager highScoreManager;
    public TMP_InputField nameInputField;
    public Button submitButton;
    public Toggle spawnToggle;  // Add this, reference your UI Toggle

    private void Awake()
    {
        // Hide the InputField and Submit Button initially
        nameInputField.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
    }

    private void Start()
    {
        // Add an event listener for when the button is clicked
        submitButton.onClick.AddListener(EnterHighScore);

        // Add an event listener for when the enter key is hit after typing in the input field
        nameInputField.onEndEdit.AddListener(delegate { EnterHighScore(); });

        // Set character limit for the input field
        nameInputField.characterLimit = 16;

        // Initialize the Toggle based on the BallSpawner state
        spawnToggle.isOn = ballSpawner.isSpawning;

        // Add a listener to call your BallSpawner function when the toggle is changed
        spawnToggle.onValueChanged.AddListener(ballSpawner.ToggleSpawning);
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.W) && !ballSpawner.canSpawn)
        // {
        //     // Toggle the visibility of InputField and Submit Button when the W key is pressed
        //     ToggleNameInput();
        // }
    }

    public void ToggleNameInput()
    {
        if (ballSpawner.canSpawn)
        {
            nameInputField.gameObject.SetActive(true);
            submitButton.gameObject.SetActive(true);
            if (nameInputField.gameObject.activeSelf)
            {
                nameInputField.Select(); // This line gives focus to the InputField
            }

            ballSpawner.canSpawn = false; // Disable the BallSpawner when entering high score mode
        }
    }

    public void EnterHighScore()
    {
        string playerName = nameInputField.text;
        int playerScore = GameManager.instance.ballCount /* the player's score */;

        if (highScoreManager.AddHighScore(playerName, playerScore))
        {
            // Hide the InputField and Submit Button after successfully adding the high score
            nameInputField.gameObject.SetActive(false);
            submitButton.gameObject.SetActive(false);

            // After successfully adding the high score, switch to the HighScore scene
            SceneManager.LoadScene("HighScore");
        }
        else
        {
            SceneManager.LoadScene("Menu");
            Debug.Log("Failed to add high score. Invalid name.");
        }
    }
}
