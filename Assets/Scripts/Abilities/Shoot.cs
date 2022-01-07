using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Shoot : MonoBehaviour, IAbility, IInputReceiverShoot, IGameEntity
{
    private bool canShoot;

    [Header("Shooting settings")]
    [SerializeField]
    private float shootCooldown;

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private Transform objSpawnPos;

    private Transform projectilesRoot;

    [Header("Shoot Direction Axis")]
    [SerializeField]
    private string horizontalLeftJoyName = "Horizontal-RightStick";

    [SerializeField]
    private string verticalLeftJoyName = "Vertical-RightStick";

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
            objSpawnPos.rotation = GetRotationToShootAt();
            GameObject projInstantiated = Instantiate(projectile, this.objSpawnPos); //Create projectile
            projInstantiated.transform.parent = projectilesRoot;
            SendActionToGameManager();  //Tell the Game Manager that a projectile has been shot
            //StartCoroutine(CooldownShoot());    //Start Projectile cooldown
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

    private Quaternion GetRotationToShootAt()
    {
        float horizontalInput = Input.GetAxis(horizontalLeftJoyName);
        float verticalInput = Input.GetAxis(verticalLeftJoyName);

        Vector3 worldPosition = new Vector3(horizontalInput, 0, -verticalInput); //-vertical input necessary

        if (worldPosition == Vector3.zero) //No input through the joystick axis 
        {
            worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Get mouse world position
            worldPosition.y = 0;
        }

        return Quaternion.LookRotation(worldPosition);
    }

    public void SetProjectilesRoot(Transform projectilesRoot) 
    {
        this.projectilesRoot = projectilesRoot;
    }
}
