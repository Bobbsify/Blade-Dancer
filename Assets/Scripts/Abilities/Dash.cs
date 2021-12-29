using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Dash : MonoBehaviour, IAbility, IInputReceiverDash
{ 
    [SerializeField]
    private float speed;

    [SerializeField]
    private float DashCountdown;

    private bool canDash;

    GameManager gameManager;

    private void Start()
    {
        canDash = true;
    }

    public IEnumerator CooldownDash()
    {
        yield return new WaitForSeconds(this.DashCountdown);
        this.canDash = true;
    }

    public void Trigger(GameObject obj)
    {
        if (this.canDash)
        {
            // TODO correggere
            Vector3 direction = obj.transform.localPosition * speed * Time.deltaTime;
            obj.transform.localPosition += direction;
            StartCoroutine(CooldownDash());
        }
    }

    public void SendActionToGameManager()
    {
        this.gameManager.ActionEventTrigger(Actions.Dash);
    }

    void IInputReceiverDash.ReceiveInputDash()
    {
        this.Trigger(this.gameObject);
    }
}
