using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Rigidbody))]
public class Dash : MonoBehaviour, IAbility, IGameEntity, IInputReceiverDash, IInputReceiverMove
{
    [SerializeField]
    private bool canDash;

    [SerializeField]
    private float dashForce = 11.0f;

    [SerializeField]
    private float dashStopTime = 0.2f;

    [SerializeField]
    private float dashCooldown;

    [SerializeField]
    private SoundPacket dashSound;

    private GameManager gameManager;

    private Vector3 lastDirection;

    private Rigidbody rigidBody;

    private void Awake()
    {
        TryGetComponent(out rigidBody);
    }

    private void Start()
    {
        canDash = true;
    }

    private IEnumerator StopDash() 
    {
        yield return new WaitForSeconds(dashStopTime);
        this.rigidBody.Sleep();
    }
    private IEnumerator CooldownDash()
    {
        yield return new WaitForSeconds(this.dashCooldown);
        this.canDash = true;
    }

    public void Trigger()
    {
        if ((this.canDash && this.enabled) && lastDirection != Vector3.zero)
        {
            canDash = false;
            this.rigidBody.AddForce(lastDirection * dashForce, ForceMode.Impulse);
            StartCoroutine(StopDash());
            SendActionToGameManager();
            gameManager.PlaySound(dashSound);
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
        if (this.enabled)
        {
            this.Trigger();
        }
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
