using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class T3A_GameManager : MonoBehaviour
{
    public int TotalItems = 5;
    public Button NextLevelButton;
    public TextMeshProUGUI HintBoxText;

    private int _itemsFound = 0;

    private void Awake()
    {
        // Hide next level button
        NextLevelButton.gameObject.SetActive(false);

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
        HintBoxText.text = text;
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
