using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour, IAbility
{
    private bool canShooting;

    [SerializeField]
    private float shootCountdown;

    public IEnumerator CooldownShooting()
    {
        yield return new WaitForSeconds(this.shootCountdown);
        this.canShooting = true;

    }

    public void Trigger(GameObject obj)
    {
        if (this.canShooting)
        {
            //TODO spara il
            this.canShooting = false;
            StartCoroutine(CooldownShooting());
        }
    }

    public void SendActionToGameManager()
    {

    } 
}
