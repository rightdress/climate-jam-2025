using UnityEngine;

public class T3A_HiddenObject : MonoBehaviour
{
    public T3A_GameManager GameManager;
    public GameObject UI_Object;
    public string HintBoxText;

    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        // Create a unique instance of the material, so that the greyscale shader doesn't affect all objects since they use the same material before runtime
        _spriteRenderer = UI_Object.GetComponent<SpriteRenderer>();
        _spriteRenderer.material = new Material(_spriteRenderer.material);

        // Set object to be grey
        _spriteRenderer.material.SetFloat("_TransitionAmount", 1f);
    }

    public void OnClicked()
    {
        // Tell GameManager that an object was found (currently doesn't specify WHICH object was found, but we can add that if necessary)
        GameManager.ItemFound();

        // Set object to be colourful
        _spriteRenderer.material.SetFloat("_TransitionAmount", 0f);

        // Update hintbox text
        GameManager.UpdateHintBox(HintBoxText);

        // Wiggle UI Object
        GameManager.WiggleObject(UI_Object);

        // Disable gameObject with collider component, which will prevent the collider from being clicked on again and
        // trigger the FMOD event as long as FMOD studio event emitter component is set up properly:
        // Event Play Trigger should be set to "Object Disable", with the correct event placed in the Event field
        gameObject.SetActive(false);
    }
}
