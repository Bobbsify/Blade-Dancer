using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private bool canDash;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float DashCountdown;

    public IEnumerator CountdownDash()
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
            StartCoroutine(CountdownDash());
        }
    }

    public void SendActionToGameManager()
    {

    }
}
