using UnityEngine;

public class T3A_CursorManager : MonoBehaviour
{
    public Texture2D DefaultCursor;
    public Texture2D DefaultClickedCursor;
    public Texture2D GameCursor;
    public Texture2D GameClickedCursor;
    public Texture2D DragCursor;
    public Texture2D HintHoverCursor;
    public Texture2D ZoomCursor;

    public bool isGameScene;

    void Start()
    {
        if (isGameScene)
        {
            Cursor.SetCursor(GameCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(DefaultCursor, Vector2.zero, CursorMode.Auto);
        }
    }

    private void SetCursor(Texture2D cursorTexture)
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void ChangeCursor(string state)
    {
        switch (state.ToLower()) {
            case "default":
                SetCursor(DefaultCursor);
                break;
            case "defaultclicked":
                SetCursor(DefaultClickedCursor);
                break;
            case "game":
                SetCursor(GameCursor);
                break;
            case "gameclicked":
                SetCursor(GameClickedCursor);
                break;
            case "drag":
                SetCursor(DragCursor);
                break;
            case "hinthover":
                SetCursor(HintHoverCursor);
                break;
            case "zoom":
                SetCursor(ZoomCursor);
                break;
        }
    }
}
