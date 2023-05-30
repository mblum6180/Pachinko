using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Show the InputField and Submit Button when the W key is pressed
            nameInputField.gameObject.SetActive(true);
            submitButton.gameObject.SetActive(true);
        }
    }

    public void EnterHighScore()
    {
        string playerName = nameInputField.text;
        int playerScore = GameManager.instance.ballCount/* the player's score */;

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
