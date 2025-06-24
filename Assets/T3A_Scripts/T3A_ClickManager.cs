using UnityEngine;
using UnityEngine.InputSystem;

public class T3A_ClickManager : MonoBehaviour
{
    Camera m_Camera;

    private void Awake()
    {
        m_Camera = Camera.main;
    }

    private void Update()
    {
        Mouse mouse = Mouse.current;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = mouse.position.ReadValue();
            Vector2 worldPoint = m_Camera.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Clicked on: " + hit.collider.name);

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
