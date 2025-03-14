using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Animator lightFlash;
    public int ballCount = 250;
    public TextMeshProUGUI ballCountText;
    [SerializeField] public int pointsToAdd = 7;
    public GameObject tooltip;
    private float inactiveTime = 4f;
    private bool hasPlayerShot = false;
    public List<GameObject> ballInstances = new List<GameObject>();
    public GameObject gameover;
    public bool IsGameOver = false;

    // Camera Adjustment Variables
    private int previousWidth, previousHeight;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            UpdateBallCountText();
        }
        gameover.SetActive(false);
        AdjustCameraSize(); // Adjust camera on start
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }

        if (!hasPlayerShot)
        {
            inactiveTime -= Time.deltaTime;
            if (inactiveTime <= 0f)
            {
                tooltip.SetActive(true);
            }
        }

        // Detect resolution changes and adjust the camera
        if (Screen.width != previousWidth || Screen.height != previousHeight)
        {
            AdjustCameraSize();
            previousWidth = Screen.width;
            previousHeight = Screen.height;
        }
    }

void AdjustCameraSize()
{
    Camera cam = Camera.main;
    if (cam == null) return;

    GameObject board = GameObject.Find("Board");
    GameObject boardLower = GameObject.Find("BoardLower"); // Second object for height

    if (board == null || boardLower == null) return;

    SpriteRenderer boardRenderer = board.GetComponent<SpriteRenderer>();
    SpriteRenderer boardLowerRenderer = boardLower.GetComponent<SpriteRenderer>();

    if (boardRenderer == null || boardLowerRenderer == null) return;

    float boardWidth = boardRenderer.bounds.size.x;
    float totalBoardHeight = boardRenderer.bounds.size.y + boardLowerRenderer.bounds.size.y; // Combined height
    float aspectRatio = (float)Screen.width / Screen.height;

    if (aspectRatio < 1f) // Portrait mode
    {
        cam.orthographicSize = (boardWidth / 2) / aspectRatio;
    }
    else // Landscape mode
    {
        cam.orthographicSize = totalBoardHeight / 2;
    }
}



    public void DecreaseBallCount()
    {
        ballCount--;
        UpdateBallCountText();
    }

    private void UpdateBallCountText()
    {
        if (ballCountText != null)
        {
            ballCountText.text = "Balls: " + ballCount;
        }
    }

    public void AddBallCount(int ballsToAdd)
    {
        ballCount += ballsToAdd;
        UpdateBallCountText();
    }

    public void FlashEffect()
    {
        if (lightFlash)
        {
            lightFlash.Play("Light", 0, 0f);
        }
        else
        {
            Debug.LogWarning("No flashAnimator assigned in GameManager!");
        }
    }

    public void RemoveBallInstance(GameObject ball)
    {
        if (ballInstances.Contains(ball))
        {
            ballInstances.Remove(ball);
        }
    }

    public void PlayerActivity()
    {
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
