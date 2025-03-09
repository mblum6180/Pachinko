using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PowerLauncher : MonoBehaviour, IPointerDownHandler
{
    public Slider powerSlider;
    public BallSpawner ballSpawner;

    private float pressValue = -1;
    private bool pressing = false;

    private void Awake()
    {
        if (powerSlider == null)
        {
            powerSlider = GetComponent<Slider>();
        }

        if (ballSpawner == null)
        {
            ballSpawner = FindFirstObjectByType<BallSpawner>();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && pressing)
        {
            powerSlider.value += Time.deltaTime * 3;
        }

        if (pressing)
        {
            // Compare slider's current value with pressValue
            if (Mathf.Abs(powerSlider.value - pressValue) <= 0.1f)
            {
                ballSpawner.SpawnBall(powerSlider.value);
                powerSlider.value = 0f;
                pressing = false;
            }
        }

        // Check for mouse button release
    if (Input.GetMouseButtonUp(0) && pressing)
    {
        // Spawn and launch ball with the accumulated power on mouse release
        ballSpawner.SpawnBall(powerSlider.value);
        powerSlider.value = 0f;
        pressing = false;
    }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransform rectTransform = powerSlider.GetComponent<RectTransform>();
        
        // Convert the mouse position to local position
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            // Normalize the local position to get a value between 0 and 1
            pressValue = Mathf.InverseLerp(rectTransform.rect.xMin, rectTransform.rect.xMax, localPoint.x);
            pressing = true;
            //Debug.Log(pressValue);
        }
    }
}
