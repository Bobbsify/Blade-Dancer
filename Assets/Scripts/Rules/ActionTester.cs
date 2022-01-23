using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTester : MonoBehaviour, IGameEntity
{
    [SerializeField]
    private Actions actionToSubmit;

    [Header("Press To Submit")]
    [SerializeField]
    private KeyCode submitKeycode = KeyCode.Break;

    private GameManager manager;

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
