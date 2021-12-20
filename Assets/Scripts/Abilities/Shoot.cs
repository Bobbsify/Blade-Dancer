using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour, IAbility
{
    private bool canShooting;

    [SerializeField]
    private float shootCountdown;

    public IEnumerator CountdownShooting()
    {
        yield return new WaitForSeconds(this.shootCountdown);
        this.canShooting = true;

    }

    public void Trigger(GameObject obj)
    {
        if (this.canShooting)
        {
            //TODO spara il proiettile
            this.canShooting = false;
            StartCoroutine(CountdownShooting());
        }
    }

    public void SendActionToGameManager()
    {

    } 
}
