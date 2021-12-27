using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTester : MonoBehaviour, IGameEntity
{
    [SerializeField]
    Actions actionToSubmit;

    [Header("Press To Submit")]
    [SerializeField]
    KeyCode submitKeycode = KeyCode.Break;

    GameManager manager;

    private void Update()
    {
        if (Input.GetKeyUp(submitKeycode))
        {
            manager.ActionEventTrigger(actionToSubmit);
        }
    }

    public void Init(GameManager gameManager)
    {
        this.manager = gameManager;
    }
}
