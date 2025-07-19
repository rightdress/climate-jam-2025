using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class T3A_HintArrow : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Transform TargetObject;

    [Header("Bounds for Returning Hint Arrow to Box")]
    [SerializeField] private Vector2 _minBounds;
    [SerializeField] private Vector2 _maxBounds;

    [Header("Default Position and Rotation")]
    [SerializeField] private Vector2 _defaultPosition;

    private Camera _camera;
    private BoxCollider2D _boxCollider;
    private Vector3 _snappingPoint = new Vector3(-4, -4, 0);
    private bool _inDefaultPosition = true;

    private void Awake()
    {
        _camera = Camera.main;
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
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
        if (_boxCollider.bounds.Contains(_snappingPoint))
        {
            transform.position = _defaultPosition;
            transform.rotation = Quaternion.identity;
            _inDefaultPosition = true;
        }
        else
        {
            _inDefaultPosition = false;
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
