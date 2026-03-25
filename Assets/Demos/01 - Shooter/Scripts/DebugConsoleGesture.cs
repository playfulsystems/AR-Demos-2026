using System;
using UnityEngine;
using UnityEngine.InputSystem;
using IngameDebugConsole;

public class DebugConsoleGesture : MonoBehaviour
{
    [SerializeField] private float swipeThreshold = 100f;

    private Vector2 startPos;
    private Vector2 lastPos;
    private bool tracking;

    private void Start()
    {
        DebugLogManager.Instance.PopupEnabled = false;
    }

    private void Update()
    {
        var touchscreen = Touchscreen.current;
        if (touchscreen == null) return;

        bool twoFingers = touchscreen.touches[0].isInProgress && 
                          touchscreen.touches[1].isInProgress;

        if (twoFingers)
        {
            if (!tracking)
            {
                startPos = touchscreen.touches[0].position.ReadValue();
                tracking = true;
            }
            // Cache position each frame while fingers are down
            lastPos = touchscreen.touches[0].position.ReadValue();
        }
        else if (tracking)
        {
            tracking = false;
            float delta = lastPos.y - startPos.y;

            if (delta < -swipeThreshold)
                DebugLogManager.Instance.ShowLogWindow();
            else if (delta > swipeThreshold)
                DebugLogManager.Instance.HideLogWindow();
        }
    }
}