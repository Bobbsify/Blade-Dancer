using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Shoot : MonoBehaviour, IAbility, IInputReceiverShoot
{
    private bool canShooting;

    [SerializeField]
    private float shootCountdown;

    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform objSpawnPos;

    GameManager gameManager;

    private void Start()
    {
        this.canShooting = true;
    }
    public IEnumerator CooldownShooting()
    {
        yield return new WaitForSeconds(this.shootCountdown);
        this.canShooting = true;
    }

    public void Trigger(GameObject obj)
    {
        if (this.canShooting)
        {
            //TODO mettere la variabile canShooting false 
            Instantiate(obj, this.objSpawnPos);
            StartCoroutine(CooldownShooting());
        }
    }

    public void SendActionToGameManager()
    {
        this.gameManager.ActionEventTrigger(Actions.Shoot);
    }

    void IInputReceiverShoot.ReceiveInputShoot()
    {
        this.Trigger(projectile);
    }
}
