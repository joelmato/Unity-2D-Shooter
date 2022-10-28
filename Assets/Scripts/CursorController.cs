using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController instance;

    public Texture2D pointer, crosshair;

    private void Awake()
    {
        instance = this;
    }

    public void SetPointer()
    {
        Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
    }

    public void SetCrosshair()
    {
        Cursor.SetCursor(crosshair, new Vector2(crosshair.width / 2, crosshair.height / 2), CursorMode.Auto);
    }
}
