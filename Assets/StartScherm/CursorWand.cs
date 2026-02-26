using UnityEngine;

public class CursorWand : MonoBehaviour
{
    public Texture2D wandCursor;
    public Vector2 hotspot = new Vector2(4, 4); // tip of the wand
    public CursorMode mode = CursorMode.Auto;

    void Start()
    {
        Cursor.SetCursor(wandCursor, hotspot, mode);
    }
}
