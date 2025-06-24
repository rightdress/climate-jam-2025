using UnityEngine;
using UnityEngine.InputSystem;

public class T3A_ClickManager : MonoBehaviour
{
    Camera _camera;

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

                    // Disable polygon collider so object can't be clicked again
                    hit.collider.enabled = false;
                }
            }
        }
    }
}
