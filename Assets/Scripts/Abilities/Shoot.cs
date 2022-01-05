using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Shoot : MonoBehaviour, IAbility, IInputReceiverShoot, IGameEntity
{
    private bool canShoot;

    [SerializeField]
    private float shootCooldown;

    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform objSpawnPos;

    private GameManager gameManager;

    private void Start()
    {
        this.canShoot = true;
    }
    public IEnumerator CooldownShoot()
    {
        yield return new WaitForSeconds(this.shootCooldown);
        this.canShoot = true;
    }

    public void Trigger()
    {
        if (this.canShoot)
        {
            canShoot = false;
            Instantiate(projectile, this.objSpawnPos); //Create projectile
            SendActionToGameManager();  //Tell the Game Manager that a projectile has been shot
            StartCoroutine(CooldownShoot());    //Start Projectile cooldown
        }
    }

    public void SendActionToGameManager()
    {
        this.gameManager.ActionEventTrigger(Actions.Shoot);
    }

    void IInputReceiverShoot.ReceiveInputShoot()
    {
        this.Trigger();
    }

    void IGameEntity.Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    void IAbility.Enable()
    {
        this.enabled = true;
    }

    void IAbility.Disable()
    {
        this.enabled = false;
    }
}
