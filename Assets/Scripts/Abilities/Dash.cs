using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Dash : MonoBehaviour, IAbility, IGameEntity, IInputReceiverDash, IInputReceiverMove
{ 
    [SerializeField]
    private float dashForce;

    [SerializeField]
    private float dashCooldown;

    private bool canDash;

    private GameManager gameManager;

    private Vector3 lastDirection;

    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        canDash = true;
    }

    public IEnumerator CooldownDash()
    {
        yield return new WaitForSeconds(this.dashCooldown);
        this.canDash = true;
    }

    public void Trigger()
    {
        if (this.canDash)
        {
            canDash = false;
            body.AddForce(lastDirection*dashForce); //Add time.deltaTime?
            SendActionToGameManager();
            StartCoroutine(CooldownDash());
        }
    }

    public void SendActionToGameManager()
    {
        this.gameManager.ActionEventTrigger(Actions.Dash);
    }
    void IInputReceiverMove.ReceiveInputMove(Vector3 direction)
    {
        this.lastDirection = direction.normalized;
    }

    void IInputReceiverDash.ReceiveInputDash()
    {
        this.Trigger();
    }
    void IAbility.Enable()
    {
        this.enabled = true;
    }

    void IAbility.Disable()
    {
        this.enabled = false;
    }

    void IGameEntity.Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
