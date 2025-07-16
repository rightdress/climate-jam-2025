using UnityEngine;

public class T3A_CursorManager : MonoBehaviour
{
    public Texture2D DefaultCursor;
    public Texture2D DefaultClickedCursor;
    public Texture2D ButtonHoverCursor;
    public Texture2D ButtonClickedCursor;
    public Texture2D GameCursor;
    public Texture2D GameClickedCursor;
    public Texture2D DragCursor;
    public Texture2D HintHoverCursor;
    public Texture2D ZoomCursor;

    public bool isGameScene;

    private string _style;

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

    public string GetCursorStyle()
    {
        return _style;
    }

    private void SetCursor(Texture2D cursorTexture)
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void ChangeCursor(string style)
    {
        switch (style.ToLower()) {
            case "default":
                SetCursor(DefaultCursor);
                break;
            case "defaultclicked":
                SetCursor(DefaultClickedCursor);
                break;
            case "buttonhover":
                SetCursor(ButtonHoverCursor);
                break;
            case "buttonclicked":
                SetCursor(ButtonClickedCursor);
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
