using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour, IAbility
{
    private bool canDash;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float DashCountdown;

    public IEnumerator CooldownDash()
    {
        yield return new WaitForSeconds(this.DashCountdown);
        this.canDash = true;

    }

    public void Trigger(GameObject obj)
    {
        if (this.canDash)
        {
            // TODO dash character
            this.canDash = false;
            StartCoroutine(CooldownDash());
        }
    }

    public void SendActionToGameManager()
    {

    }
}
