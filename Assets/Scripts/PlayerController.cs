using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour, IGameEntity
{
    [Header("Health")]
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private int currentHealth;

    [SerializeField]
    private float invincibilityDuration = 0.5f;

    [Header("Sound Effects")]
    [SerializeField]
    private SoundPacket playerDamage;

    [SerializeField]
    private SoundPacket playerShoot;

    [SerializeField]
    private SoundPacket playerDie;

    //---------------------------

    private Animator animPlayer;

    private IAbility[] abilities;

    private PhysicsMovement movement;

    private GameObject[] health;

    private GameManager gameManager;

    private bool canTakeDamage = true;

    private void OnValidate()
    {
        this.animPlayer = this.gameObject.GetComponentInChildren<Animator>(true);
    }

    private void Start()
    {
        currentHealth = maxHealth;
        this.abilities = GetComponentsInChildren<IAbility>(true);
        movement = GetComponent<PhysicsMovement>();
        TryGetComponent(out animPlayer);
    }

    public int GetMaxHealth() { return this.maxHealth; }
    public int GetHealth() { return this.currentHealth; }

    public void TakeDamage(int amount)
    {
        if (canTakeDamage) { 
            currentHealth = Mathf.Min(Mathf.Max(0, currentHealth - amount),maxHealth);
            canTakeDamage = false;
            if (currentHealth == 0)
            {
                gameManager.PlaySound(playerDie);
                DoDeath();
            }
            else if (amount > 0) 
            {
                gameManager.PlaySound(playerDamage);
                StartCoroutine(Invincibility());
            }
            gameManager.ActionEventTrigger(Actions.TakeDamage);
            gameManager.PlayerDamageTrigger();
        }
    }
    #region Animation

    public void Animate(string name, int amount)
    {
        animPlayer.SetInteger(name, amount);
    }
    public void Animate(string name, bool state = true)
    {
        animPlayer.SetBool(name,state);
    }
    public void Animate(string name, float amount)
    {
        animPlayer.SetFloat(name, amount);
    }
    public void Animate(string name)
    {
        animPlayer.SetTrigger(name);
    }

    public void FallAnimationCompleted()
    {
        DisableAllAbilities();
        gameManager.PlayerHasFallen();
    }

    public void PlayerLanded()
    {
        EnableAllAbilities();
        gameManager.PlayerLanded();
    }

    #endregion

    public void Reset()
    {
        gameManager.doReset();
        this.currentHealth = maxHealth;
        canTakeDamage = true;
        gameManager.PlayerDamageTrigger();
    }

    public void ToggleMovement(bool state = true) 
    {
        this.movement.enabled = state;
    }

    public void EnableAbility<T>() where T : IAbility
    {
        foreach (IAbility ability in abilities)
        {
            if (ability is T)
            {
                ability.Enable();
            }
        }
    }

    public void DisableAbility<T>() where T : IAbility
    {
        foreach (IAbility ability in abilities)
        {
            if (ability is T)
            {
                ability.Disable();
            }
        }
    }

    public void EnableOtherAbilities<T>() where T : IAbility
    {
        foreach (IAbility ability in abilities)
        {
            if (!(ability is T))
            {
                ability.Enable();
            }
        }
    }

    public void DisableOtherAbilities<T>() where T : IAbility
    {
        foreach (IAbility ability in abilities)
        {
            if (!(ability is T))
            {
                ability.Disable();
            }
        }
    }

    public void EnableAllAbilities()
    {
        foreach (IAbility ability in abilities)
        {
            ability.Enable();
        }
        movement.enabled = true;
    }

    public void DisableAllAbilities()
    {
        foreach (IAbility ability in abilities)
        {
            ability.Disable();
        }
        movement.enabled = false;
    }

    private void DoDeath()
    {
        Animate("death");
        DisableAllAbilities();
    }

    private IEnumerator Invincibility() 
    {
        yield return new WaitForSeconds(invincibilityDuration);
        canTakeDamage = true;
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
