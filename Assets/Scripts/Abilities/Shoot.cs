using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Shoot : MonoBehaviour, IAbility, IInputReceiverShoot, IGameEntity
{
    [Header("Shooting settings")]
    [SerializeField]
    private bool canShoot = true;

    [SerializeField]
    private float shootCooldown;

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private Transform objSpawnPos;

    [SerializeField]
    private SoundPacket shootingSound;

    [Header("Shoot Direction Axis")]
    [SerializeField]
    private string horizontalLeftJoyName = "Horizontal-RightStick";

    [SerializeField]
    private string verticalLeftJoyName = "Vertical-RightStick";

    private GameManager gameManager;

    private Transform projectilesRoot;

    private Animator anim;

    private void Start()
    {
        TryGetComponent(out anim);
    }
    public IEnumerator CooldownShoot()
    {
        yield return new WaitForSeconds(this.shootCooldown);
        this.canShoot = true;
    }
    private void Update()
    {
        Quaternion rotation = GetRotationToShootAt();
        objSpawnPos.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
        anim.SetFloat("rotation", rotation.eulerAngles.y / 360);
    }

    public void Trigger()
    {
        if (this.canShoot)
        {
            canShoot = false;
            //objSpawnPos.rotation = GetRotationToShootAt();
            GameObject projInstantiated = Instantiate(projectile, this.objSpawnPos); //Create projectile
            projInstantiated.GetComponent<ProjectileController>().SetTeam(Team.Player);
            projInstantiated.transform.parent = projectilesRoot;
            gameManager.PlaySound(shootingSound);
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
        if (this.enabled)
        {
            this.Trigger();
        }
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

    public void CanShoot(bool state)
    {
        this.canShoot = state;
    }

    public void SetProjectilesRoot(Transform projectilesRoot) 
    {
        this.projectilesRoot = projectilesRoot;
    }
}
