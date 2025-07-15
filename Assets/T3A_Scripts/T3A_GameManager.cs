using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class T3A_GameManager : MonoBehaviour
{
    public int TotalItems = 5;
    public Button NextLevelButton;
    public TextMeshProUGUI HintBoxText;
    public GameObject DialogueBox;
    public TextMeshProUGUI DialogueBoxTitle;
    public TextMeshProUGUI DialogueBoxText;
    public T3A_CursorManager CursorManager;

    private int _itemsFound = 0;

    private void Awake()
    {
        // Hide next level button
        NextLevelButton.gameObject.SetActive(false);

        // Hide dialogue popup
        CloseDialogueBox();

        // Clear hintbox text
        UpdateHintBox("");
    }

    public void ItemFound()
    {
        _itemsFound++;

        if ( _itemsFound == TotalItems )
        {
            NextLevelButton.gameObject.SetActive(true);
        }
    }

    public void UpdateHintBox(string text)
    {
        if (text == "")
        {
            CursorManager.ChangeCursor("game");
        }

        else
        {
            CursorManager.ChangeCursor("hinthover");
        }
        
        HintBoxText.text = text;
    }

    public void ShowDialogueBox(string title, string text)
    {
        DialogueBoxTitle.text = title;
        DialogueBoxText.text = text;
        DialogueBox.gameObject.SetActive(true);
    }

    public void CloseDialogueBox()
    {
        DialogueBox.gameObject.SetActive(false);
    }

    public void WiggleObject(GameObject obj)
    {
        StartCoroutine(WiggleRoutine(obj));
    }

    IEnumerator WiggleRoutine(GameObject obj)
    {
        float duration = 1f;
        float speed = 20f;
        float angle = 15f;
        float time = 0f;

        Quaternion startRot = obj.transform.rotation;

        while (time <= duration)
        {
            float fade = 1f - (time / duration);
            float zRotation = Mathf.Sin(time * speed) * angle * fade;
            obj.transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
            time += Time.deltaTime;
            yield return null;
        }

        obj.transform.rotation = startRot;
    }
}
