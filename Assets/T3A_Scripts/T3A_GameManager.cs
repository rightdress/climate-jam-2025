using UnityEngine;
using UnityEngine.UI;

public class T3A_GameManager : MonoBehaviour
{
    public int TotalItems = 5;
    public Button NextLevelButton;

    private int _itemsFound = 0;

    private void Awake()
    {
        // Hide next level button
        NextLevelButton.gameObject.SetActive(false);
    }

    public void ItemFound()
    {
        _itemsFound++;

        if ( _itemsFound == TotalItems )
        {
            NextLevelButton.gameObject.SetActive(true);
        }
    }
}
