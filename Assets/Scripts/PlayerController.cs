using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour, IGameEntity
{
    [Header("Health")]
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private int currentHealth;

    private Animator animPlayer;

    private IAbility[] abilities;

    private PhysicsMovement movement;

    private GameObject[] health;

    private GameManager gameManager;

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
        if (currentHealth > 0) { 
            currentHealth = Mathf.Min(Mathf.Max(0, currentHealth - amount),maxHealth);
            if (currentHealth == 0) 
            {
                DoDeath();
            }
            gameManager.ActionEventTrigger(Actions.TakeDamage);
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

    #endregion

    public void Reset()
    {
        gameManager.doReset();
        this.currentHealth = maxHealth;
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

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
