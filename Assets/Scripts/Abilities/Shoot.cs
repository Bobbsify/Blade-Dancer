using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Shoot : MonoBehaviour, IAbility
{
    private bool canShooting;

    [SerializeField]
    private float shootCountdown;

    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform projectileSpawnPosition;

    public IEnumerator CooldownShooting()
    {
        yield return new WaitForSeconds(this.shootCountdown);
        this.canShooting = true;
    }

    public void Trigger(GameObject obj)
    {
        if (this.canShooting)
        {
            Instantiate(this.projectile, this.projectileSpawnPosition);
            this.canShooting = false;
            StartCoroutine(CooldownShooting());
        }
    }

    public void SendActionToGameManager()
    {

    } 
}
