using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class T3A_HintArrow : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Transform TargetObject;

    [Header("Default Position and Rotation")]
    [SerializeField] private Vector3 _defaultPosition;

    private Camera _camera;
    private BoxCollider2D _boxCollider;
    private bool _inDefaultPosition = true;

    private Dictionary<float, float> _orthographicSizeToArrowPosition = new Dictionary<float, float>();
    private RectTransform _rectTransform;

    private void Awake()
    {
        _camera = Camera.main;
        _boxCollider = GetComponent<BoxCollider2D>();
        _rectTransform = GetComponent<RectTransform>();

        _orthographicSizeToArrowPosition.Add(10, -8.285f);
        _orthographicSizeToArrowPosition.Add(9, -7.46f);
        _orthographicSizeToArrowPosition.Add(8, -6.64f);
        _orthographicSizeToArrowPosition.Add(7, -5.81f);
        _orthographicSizeToArrowPosition.Add(6, -4.98f);
        _orthographicSizeToArrowPosition.Add(5, -4.155f);
        _orthographicSizeToArrowPosition.Add(4, -3.325f);
        _orthographicSizeToArrowPosition.Add(3, -2.5f);
        _orthographicSizeToArrowPosition.Add(2, -1.665f);

        _defaultPosition = GetDefaultPosition();
    }

    public Vector3 GetDefaultPosition()
    {
        float xPos = _orthographicSizeToArrowPosition[_camera.orthographicSize] + _camera.transform.position.x;
        float yPos = _orthographicSizeToArrowPosition[_camera.orthographicSize] + _camera.transform.position.y;
        return new Vector3(xPos, yPos, 0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _defaultPosition = GetDefaultPosition();
    }

    // Update object's position and rotation if it's being dragged
    public void OnDrag(PointerEventData eventData)
    {
        // Get mouse
        Mouse mouse = Mouse.current;

        // Find mouse position
        Vector2 mousePosition = mouse.position.ReadValue();
        Vector2 worldPoint = _camera.ScreenToWorldPoint(mousePosition);

        // Move arrow with mouse
        transform.position = worldPoint;

        // Rotate arrow to look at target
        // Based off of this code: https://discussions.unity.com/t/make-sprite-look-at-vector2-in-unity-2d/97929/2
        Vector3 direction = TargetObject.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Check if arrow's collision box contains the "snapping point" above, and if so, return arrow to default position and rotation
        // Docs: https://docs.unity3d.com/6000.1/Documentation/ScriptReference/Bounds.Contains.html
        if (_boxCollider.bounds.Contains(_defaultPosition))
        {
            transform.position = _defaultPosition;
            transform.rotation = Quaternion.identity;
            _inDefaultPosition = true;

            // Reset object's Z position to 0 so it doesn't disappear when we zoom out
            Vector3 currentPosition = _rectTransform.anchoredPosition3D;
            currentPosition.z = 0;
            _rectTransform.anchoredPosition3D = currentPosition;
        }
        else
        {
            _inDefaultPosition = false;

            // Reset object's Z position to 0 so it doesn't disappear when we zoom out
            Vector3 currentPosition = _rectTransform.anchoredPosition3D;
            currentPosition.z = 0;
            _rectTransform.anchoredPosition3D = currentPosition;
        }
    }

    public void SwitchTargetObject(Transform newTarget)
    {
        TargetObject = newTarget;

        if (!_inDefaultPosition)
        {
            // Change direction of arrow if it's out of its default position
            Vector3 direction = TargetObject.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
    }

    
}
