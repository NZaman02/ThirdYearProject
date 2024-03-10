using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{

    public Texture2D cursorTexture;
    public Vector2 hotspot = Vector2.zero;

    private void Start()
    {
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }

    private void Update()
    {
        // Follow mouse position with the reticle
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z; 
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnDestroy()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}

