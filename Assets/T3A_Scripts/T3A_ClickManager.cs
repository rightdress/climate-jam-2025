using UnityEngine;
using UnityEngine.InputSystem;

public class T3A_ClickManager : MonoBehaviour
{
    [Header("Camera zoom limits")]
    public float MinZoom;
    public float MaxZoom;

    [Header("Camera bounds")]
    [SerializeField] private Vector2 _minBounds;
    [SerializeField] private Vector2 _maxBounds;

    public T3A_CursorManager CursorManager;

    Camera _camera;
    private Vector2 _dragOrigin;
    private bool _isDragging = false;

    private void Awake()
    {
        _camera = Camera.main;
        CalculateCameraBounds();
    }

    private void Update()
    {
        Mouse mouse = Mouse.current;

        // Handle clicks (looking for hidden objects)
        if (mouse.leftButton.wasPressedThisFrame)
        {
            // Change cursor
            CursorManager.ChangeCursor("gameclicked");

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

        if (mouse.leftButton.wasReleasedThisFrame)
        {
            // Change cursor
            CursorManager.ChangeCursor("game");
        }

        // Handle zoom (changing scene size)
        float scroll = mouse.scroll.ReadValue().y;

        if (Mathf.Abs(scroll) > 0.01f)
        {
            // Change size of camera
            _camera.orthographicSize -= scroll; //TODO: setting to reverse direction of mouse scroll?
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, MinZoom, MaxZoom);

            // Set new camera bounds according to orthographic size
            CalculateCameraBounds();

            // Clamp camera between new camera bounds
            Vector3 clampedPosition = _camera.transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, _minBounds.x, _maxBounds.x);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, _minBounds.y, _maxBounds.y);
            _camera.transform.position = clampedPosition;
        }

        // Handle drag (moving around scene)
        if (mouse.middleButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = mouse.position.ReadValue();
            _dragOrigin = _camera.ScreenToWorldPoint(mousePosition);
            _isDragging = true;

            // Change cursor
            CursorManager.ChangeCursor("drag");
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
            newPosition.x = Mathf.Clamp(newPosition.x, _minBounds.x, _maxBounds.x);
            newPosition.y = Mathf.Clamp(newPosition.y, _minBounds.y, _maxBounds.y);

            // Set camera position
            _camera.transform.position = newPosition;

            // Reset mouse position
            _dragOrigin = _camera.ScreenToWorldPoint(mouse.position.ReadValue());
        }

        if (mouse.middleButton.wasReleasedThisFrame)
        {
            _isDragging = false;

            // Change cursor
            CursorManager.ChangeCursor("game");
        }
    }

    private void CalculateCameraBounds()
    {
        float orthographicSize = _camera.orthographicSize;
        float aspectRatio = _camera.aspect;
        _minBounds = new Vector2((-MaxZoom + orthographicSize) * aspectRatio, -MaxZoom + orthographicSize);
        _maxBounds = new Vector2((MaxZoom - orthographicSize) * aspectRatio, MaxZoom - orthographicSize);
    }
}
