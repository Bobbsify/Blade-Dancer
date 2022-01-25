using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Dance : MonoBehaviour, IAbility, IInputReceiverDance, IGameEntity
{
    [SerializeField]
    private int charge = 1;
    [SerializeField]
    private int minCharge = 1;
    [SerializeField]
    private int maxCharge = 5;

    [Header("Dance Ability Area Defining")]
    [SerializeField]
    private float minSphereWidth = 1.0f;
    [SerializeField]
    private float maxSphereWidth = 10.0f;

    private GameManager gameManager;

    private PlayerController playerController;

    private void OnValidate()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Awake()
    {
        if(playerController == null)
        playerController = GetComponent<PlayerController>();
    }

    public void Trigger()
    {
        playerController.DisableOtherAbilities<Dance>();
        playerController.Animate("dance");
        float radius = charge * maxSphereWidth / maxCharge;
        Collider[] hits =Physics.OverlapSphere(this.transform.position, radius);
        foreach (Collider hit in hits) 
        {
            if (hit.TryGetComponent(out IEnemy enemy)) 
            {
                enemy.Dance();
            }
        }
        SendActionToGameManager();
        this.charge = minCharge;
        this.enabled = false; //Deactivate after use;
    }

    public void Charge(int amount) 
    {
        this.charge += Mathf.Max(Mathf.Min(charge + amount, maxCharge),minCharge);
    }

    public void SendActionToGameManager()
    {
        this.gameManager.ActionEventTrigger(Actions.Dance);
    }

    void IInputReceiverDance.ReceiveInputDance()
    {
        if (this.enabled)
        {
            this.Trigger();
        }
    }
    void IAbility.Enable()
    {
        this.enabled = true;
    }

    void IAbility.Disable()
    {
        this.enabled = false;
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void OnDrawGizmosSelected()
    {
        Color GizmosColor = Color.green;
        GizmosColor.a = 0.2f;
        Gizmos.color = GizmosColor;
        float radius = charge * maxSphereWidth / maxCharge;
        Gizmos.DrawSphere(this.transform.position, radius);
    }
}
