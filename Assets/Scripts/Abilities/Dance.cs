using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Dance : MonoBehaviour, IAbility, IInputReceiverDance
{
    private bool canDance;

    private bool CanBeStunned;

    [SerializeField]
    private float enemyCountdown;

    [SerializeField]
    private string enemy = "Enemy";

    GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemy))
        {
            CanBeStunned = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(enemy))
        {
            CanBeStunned = false;
        }
    }

    public IEnumerator CooldownEnemy(GameObject obj)
    {
        yield return new WaitForSeconds(enemyCountdown);
        obj.GetComponent<Rigidbody>().isKinematic = false;

    }

    public void Trigger(GameObject obj)
    {
        if (this.canDance)
        {
            if (this.CanBeStunned)
            {
                obj.GetComponent<Rigidbody>().isKinematic = true;
                StartCoroutine(CooldownEnemy(obj));
            }
        }
    }

    void IInputReceiverDance.ReceiveInputDance()
    {
      //TODO inserire il collider nemico 
    }

    public void SendActionToGameManager()
    {
        this.gameManager.ActionEventTrigger(Actions.Dance);
    }
}
