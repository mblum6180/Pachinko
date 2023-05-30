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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Toggle the visibility of InputField and Submit Button when the W key is pressed
            ToggleNameInput();

            ballSpawner.canSpawn = false;
        }
    }

    public void ToggleNameInput()
    {
        nameInputField.gameObject.SetActive(!nameInputField.gameObject.activeSelf);
        submitButton.gameObject.SetActive(!submitButton.gameObject.activeSelf);
        if (nameInputField.gameObject.activeSelf)
        {
            nameInputField.Select(); // This line gives focus to the InputField
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
            Debug.Log("Failed to add high score. Invalid name.");
        }
    }
}
