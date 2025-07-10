using UnityEngine;
using UnityEngine.UI;

public class T3A_HiddenObject : MonoBehaviour
{
    public T3A_GameManager GameManager;
    public GameObject UI_Object;
    public GameObject UI_Object_Background;
    public string Dialogue_Title;
    public string Dialogue_Text;

    Image _image;

    private void Awake()
    {
        // Grab image of UI background so we can change its color once an object has been found
        _image = UI_Object_Background.GetComponent<Image>();
    }

    public void OnClicked()
    {
        // Tell GameManager that an object was found
        GameManager.ItemFound();

        // Make background of UI object light green
        _image.color = new Color32(167, 233, 118, 255);

        // Wiggle UI Object
        GameManager.WiggleObject(UI_Object);

        // Make Dialogue Box appear
        GameManager.ShowDialogueBox(Dialogue_Title, Dialogue_Text);

        // Disable gameObject with collider component, which will prevent the collider from being clicked on again and
        // trigger the FMOD event as long as FMOD studio event emitter component is set up properly:
        // Event Play Trigger should be set to "Object Disable", with the correct event placed in the Event field
        gameObject.SetActive(false);
    }
}
