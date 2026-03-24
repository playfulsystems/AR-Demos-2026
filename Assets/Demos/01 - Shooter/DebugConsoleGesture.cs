using UnityEngine;
using UnityEngine.InputSystem;
using IngameDebugConsole;

public class DebugConsoleGesture : MonoBehaviour
{
    [SerializeField] private float swipeThreshold = 100f;

    private Vector2 _startPos;
    private bool _tracking;

    private void Update()
    {
        var touches = Touchscreen.current;
        if (touches == null) return;

        bool twoFingers = touches.touches[0].isInProgress && touches.touches[1].isInProgress;

        if (twoFingers && !_tracking)
        {
            _startPos = touches.touches[0].position.ReadValue();
            _tracking = true;
        }
        else if (!twoFingers && _tracking)
        {
            _tracking = false;
            float delta = touches.touches[0].position.ReadValue().y - _startPos.y;

            if (delta < -swipeThreshold)
                DebugLogManager.Instance.ShowLogWindow();
            else if (delta > swipeThreshold)
                DebugLogManager.Instance.HideLogWindow();
        }
    }
}