using UnityEngine;

public class T3A_HiddenObject : MonoBehaviour
{
    public T3A_GameManager GameManager;
    public GameObject UI_Object;

    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        // Create a unique instance of the material, so that the greyscale shader doesn't affect all objects since they use the same material before runtime
        _spriteRenderer = UI_Object.GetComponent<SpriteRenderer>();
        _spriteRenderer.material = new Material(_spriteRenderer.material);

        // Set object to be grey
        _spriteRenderer.material.SetFloat("_Greyscale", 1f);
    }

    public void OnClicked()
    {
        // Tell GameManager that an object was found (currently doesn't specify WHICH object was found, but we can add that if necessary)
        GameManager.ItemFound();

        // Set object to be colourful
        _spriteRenderer.material.SetFloat("_Greyscale", 0f);
    }
}
