using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
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
            obj.transform.position = obj.transform.position * speed * Time.deltaTime;
            this.canDash = false;
            StartCoroutine(CooldownDash());
        }
    }

    public void SendActionToGameManager()
    {

    }
}
