using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game related
/// Changes player cursor
/// </summary>
public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D tex;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if(tex) Cursor.SetCursor(tex, new Vector2(tex.width / 2, tex.height / 2), CursorMode.Auto);  
    }
}
