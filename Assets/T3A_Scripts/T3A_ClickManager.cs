using UnityEngine;
using UnityEngine.InputSystem;

public class T3A_ClickManager : MonoBehaviour
{
    public Vector2 minBounds;
    public Vector2 maxBounds;

    Camera _camera;
    private Vector2 _dragOrigin;
    private bool _isDragging = false;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Mouse mouse = Mouse.current;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = mouse.position.ReadValue();
            Vector2 worldPoint = _camera.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                // Check if the clicked object is a hidden object
                T3A_HiddenObject hidden_object = hit.collider.GetComponent<T3A_HiddenObject>();
                if (hidden_object != null)
                {
                    hidden_object.OnClicked();
                }
            }
        }

        if (mouse.middleButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = mouse.position.ReadValue();
            _dragOrigin = _camera.ScreenToWorldPoint(mousePosition);
            _isDragging = true;
        }

        if (mouse.middleButton.isPressed && _isDragging)
        {
            // Get mouse position
            Vector2 mousePosition = mouse.position.ReadValue();
            Vector2 curMousePosition = _camera.ScreenToWorldPoint(mousePosition);
            
            // Find difference between old and new mouse position
            Vector2 difference = _dragOrigin - curMousePosition;
            Vector3 newPosition = _camera.transform.position + new Vector3(difference.x, difference.y, 0);

            // Clamp camera within defined bounds
            newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);
            _camera.transform.position = newPosition;

            // Reset mouse position
            _dragOrigin = _camera.ScreenToWorldPoint(mouse.position.ReadValue());
        }

        if (mouse.middleButton.wasReleasedThisFrame)
        {
            _isDragging = false;
        }
    }
}
