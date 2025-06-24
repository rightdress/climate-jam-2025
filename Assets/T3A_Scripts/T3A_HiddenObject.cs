using UnityEngine;

public class T3A_HiddenObject : MonoBehaviour
{
    public GameObject UI_Object;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = UI_Object.GetComponent<SpriteRenderer>();
        spriteRenderer.material = new Material(spriteRenderer.material); // Create a unique instance of the material, so that the greyscale shader doesn't affect all objects

        // Set object to be grey
        spriteRenderer.material.SetFloat("_Greyscale", 1f);
    }

    public void OnClicked()
    {
        // TODO: tell gamemanager that object was found

        // Set object to be colourful
        spriteRenderer.material.SetFloat("_Greyscale", 0f);
    }
}
