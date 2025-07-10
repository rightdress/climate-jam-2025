using UnityEngine;
using UnityEngine.SceneManagement;

public class T3A_Controller : MonoBehaviour
{
    // Make it so that the controller gameobject (and all of its children, including the options menu) persist between scenes
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void NextScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Debug.Log("Quit button pressed.");
        Application.Quit();
    }

    public void ShowPopup(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void HidePopup(GameObject obj)
    {
        obj.SetActive(false);
    }
}
