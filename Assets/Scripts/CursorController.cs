using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    // Creates and instance of the script in order to be able to access it from other scripts
    public static CursorController instance;

    public Texture2D pointer, crosshair;

    private void Awake()
    {
        instance = this;
    }

    public void SetPointer()
    {
        // Updates the cursor to a pointer and resets the hotspot of the cursor to its default position (top-left)
        Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
    }

    public void SetCrosshair()
    {
        // Updates the cursor to a crosshair and changes the hotspot of the cursor to the center of the crosshair image
        Cursor.SetCursor(crosshair, new Vector2(crosshair.width / 2, crosshair.height / 2), CursorMode.Auto);
    }
}
