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
            canShoot = false;
            objSpawnPos.rotation = GetRotationToShootAt();
            GameObject projInstantiated = Instantiate(projectile, this.objSpawnPos); //Create projectile
            projInstantiated.GetComponent<ProjectileController>().SetTeam(Team.Player);
            projInstantiated.transform.parent = projectilesRoot;
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

    private Quaternion GetRotationToShootAt()
    {
        float horizontalInput = Input.GetAxis(horizontalLeftJoyName);
        float verticalInput = Input.GetAxis(verticalLeftJoyName);

        Vector3 worldPosition = new Vector3(horizontalInput, 0, -verticalInput); //-vertical input necessary

        if (worldPosition == Vector3.zero) //No input through the joystick axis 
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(objSpawnPos.position);
            Vector3 dir = mousePos - screenPos; //Find direction vector between the mouse and the objspawner
            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg; //Get tangent (Conver to degrees, Atan2 returns radians)
            return Quaternion.AngleAxis(angle, Vector3.up);
        }

        return Quaternion.LookRotation(worldPosition,Vector3.forward);
    }

    public void SetProjectilesRoot(Transform projectilesRoot) 
    {
        this.projectilesRoot = projectilesRoot;
    }
}
