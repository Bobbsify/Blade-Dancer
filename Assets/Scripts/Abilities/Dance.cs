using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance : MonoBehaviour, IAbility, IInputReceiverDance
{
    private bool canDance;

    [SerializeField]
    private float enemyCountdown;

    public IEnumerator CooldownEnemy(GameObject obj)
    {
        yield return new WaitForSeconds(this.enemyCountdown);
        obj.GetComponent<Rigidbody>().isKinematic = false;
        obj.GetComponent<Animator>().enabled = true;
    }

    public void Trigger(GameObject obj)
    {
        if (this.canDance)
        {
            obj.GetComponent<Rigidbody>().isKinematic = true;
            obj.GetComponent<Animator>().enabled = false;
            this.canDance = false;
            StartCoroutine(CooldownEnemy(obj));
        }
    }

    public void ReceiveInputDance()
    {
       
    }

    public void SendActionToGameManager()
    {

    }
}
