using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Button[] buttons;
    private int selectedButtonIndex;

    void Start()
    {
        SelectButton(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SelectButton(selectedButtonIndex - 1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SelectButton(selectedButtonIndex + 1);
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            buttons[selectedButtonIndex].onClick.Invoke();
        }
    }

    void SelectButton(int index)
    {
        // Ensure the index is within the bounds of the buttons array
        index = Mathf.Clamp(index, 0, buttons.Length - 1);
        
        // Change the color of the previously selected button to normal
        buttons[selectedButtonIndex].image.color = Color.white;

        // Update the selected button index
        selectedButtonIndex = index;

        // Change the color of the newly selected button to highlight it
        buttons[selectedButtonIndex].image.color = Color.green;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowHighScores()
    {
        SceneManager.LoadScene("HighScore");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}