using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Dance : MonoBehaviour, IAbility, IInputReceiverDance
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
        float radius = (maxCharge / maxSphereWidth) * (charge / minSphereWidth);
        RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, radius, Vector3.zero);
        foreach (RaycastHit hit in hits) 
        {
            if (hit.collider.TryGetComponent(out IEnemy enemy)) 
            {
                enemy.Dance();
            }
        }
        this.charge = minCharge;
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
      //TODO inserire il collider nemico 
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
