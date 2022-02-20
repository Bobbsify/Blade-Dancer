using System;
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
    [SerializeField]
    private float danceDuration = 1f;

    [Header("Dance Ability Area Defining")]
    [SerializeField]
    private float minSphereWidth = 1.0f;
    [SerializeField]
    private float maxSphereWidth = 10.0f;

    [Header("Particles")]

    [SerializeField]
    private ParticleSystem danceParticles;

    [SerializeField]
    [Range(0.1f,5.0f)]
    private float speedMultiplier = 1.5f;

    [SerializeField]
    private float particleDefaultSpeed;

    [SerializeField]
    [Range(4,20)]
    private float particleDefaultAmount = 4;

    [Header("Sound")]

    [SerializeField]
    private SoundPacket danceSound;

    // ----

    private GameManager gameManager;

    private PlayerController playerController;

    private UIDanceController danceUI;

    private void OnValidate()
    {
        playerController = GetComponent<PlayerController>();
        if (danceParticles == null)
        {
            danceParticles = transform.Find("DanceParticles").GetComponent<ParticleSystem>();
        }
        else
        {
            particleDefaultSpeed = danceParticles.main.startSpeed.constant;
        }
    }

    private void Start()
    {
        if(playerController == null)
        playerController = GetComponent<PlayerController>();

        danceUI = gameManager.GetUIComponent<UIDanceController>();
        if (danceUI == null)
        {
            Debug.LogError("No dance ui attached to " + this);
        }
        else 
        {
            danceUI.UpdateCharge(charge);
        }


    }

    public void Trigger()
    {
        playerController.DisableOtherAbilities<Dance>();
        gameManager.ShakeCamera();

        var main = danceParticles.main;
        main.startSpeed = particleDefaultSpeed + (charge * speedMultiplier);
        danceParticles.Emit(Mathf.FloorToInt(charge * particleDefaultAmount));
        gameManager.PlaySound(danceSound);

        playerController.Animate("dance");

        float radius = Mathf.Max(charge * maxSphereWidth / maxCharge, minSphereWidth);
        Collider[] hits =Physics.OverlapSphere(this.transform.position, radius);
        foreach (Collider hit in hits) 
        {
            if (hit.TryGetComponent(out IEnemy enemy)) 
            {
                enemy.Dance();
            }
        }
        StartCoroutine(DanceRoutine());
        this.charge = minCharge;
        danceUI.UpdateCharge(charge);
        this.enabled = false; //Deactivate after use;
    }

    public void Charge(int amount) 
    {
        this.charge = Mathf.Max(Mathf.Min(charge + amount, maxCharge),minCharge);
        danceUI.UpdateCharge(charge);
    }

    public void SendActionToGameManager()
    {
        this.gameManager.ActionEventTrigger(Actions.Dance);
    }

    private IEnumerator DanceRoutine()
    {
        gameManager.AskForTimerStop();
        yield return new WaitForSeconds(danceDuration);
        gameManager.AskForTimerStart();
        SendActionToGameManager();
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
