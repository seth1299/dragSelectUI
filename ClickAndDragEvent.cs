/*

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[System.Serializable]
public class ClickAndDragEvent : UnityEvent<Vector2, Vector2> { }

public class ClickAndDragSelector : MonoBehaviour
{
    // Event that passes the start and end positions of the click-and-drag.
    public ClickAndDragEvent OnClickAndDrag = new ClickAndDragEvent();

    // Adjustable settings for what constitutes a "quick" click-and-drag.
    public float maxClickTime = 0.3f;      // Maximum allowed time (in seconds) for the drag.
    public float minDragDistance = 20f;    // Minimum drag distance (in pixels) to register.

    private Vector2 mouseDownPos;
    private float mouseDownTime;
    private bool isDragging;

    void Update()
    {
        // Check that a mouse is connected
        if (Mouse.current == null)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Record the time and position when the mouse is pressed.
            mouseDownTime = Time.time;
            mouseDownPos = Mouse.current.position.ReadValue();
            isDragging = true;
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && isDragging)
        {
            // When the mouse button is released, evaluate the drag.
            isDragging = false;
            Vector2 mouseUpPos = Mouse.current.position.ReadValue();
            float dragDuration = Time.time - mouseDownTime;
            float dragDistance = Vector2.Distance(mouseDownPos, mouseUpPos);

            // Check if the drag was quick enough and moved far enough to count.
            if (dragDuration <= maxClickTime && dragDistance >= minDragDistance)
            {
                // Fire the event, passing the start and end positions.
                OnClickAndDrag?.Invoke(mouseDownPos, mouseUpPos);
            }
        }
    }
}
*/