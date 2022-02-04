using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSetter : MonoBehaviour
{
    [SerializeField]
    private List<Texture2D> cursorSprites = new List<Texture2D>();
    
    [SerializeField]
    [Tooltip("Corrisponde alla tipologia di cursor specificato nella texture")]
    private List<CursorType> cursorSpritesType = new List<CursorType>();

    [SerializeField]
    private CursorType defaultCursorType = CursorType.Menu;

    private Dictionary<CursorType, Texture2D> cursors = new Dictionary<CursorType, Texture2D>();

    private void Awake()
    {
        if (cursorSpritesType.Count > cursorSprites.Count) 
        {
            Debug.LogError("Not all cursor types specify a certain cursor!");
        }
        for (int i = 0; i < cursorSprites.Count; i++) 
        {
            cursors.Add(cursorSpritesType[i], cursorSprites[i]);
        }
    }

    private void Start()
    {
        SetCursor(defaultCursorType);
    }

    public void SetCursor(CursorType cursorType)
    {
        Cursor.SetCursor(cursors[cursorType], Vector3.zero, CursorMode.Auto);
    }
}

public enum CursorType 
{ 
    Menu,
    Game
}
