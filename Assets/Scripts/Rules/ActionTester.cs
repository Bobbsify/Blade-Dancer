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

    private void FixedUpdate()
    {
        if (Input.GetKey(submitKeycode))
        {
            manager.ActionEventTrigger(actionToSubmit);
        }
    }

    public void Init(GameManager gameManager)
    {
        this.manager = gameManager;
    }
}
