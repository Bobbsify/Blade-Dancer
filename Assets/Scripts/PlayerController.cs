using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    private int currentHealth;

    private Animator animPlayer;

    IAbility[] abilities;

    PhysicsMovement movement;

    private void OnValidate()
    {
        movement = GetComponent<PhysicsMovement>();
        this.animPlayer = this.gameObject.GetComponentInChildren<Animator>(true);
    }

    private void Start()
    {
        currentHealth = maxHealth;
        this.abilities = GetComponentsInChildren<IAbility>(true);
    }

    public void TakeDamage(int amount) 
    {
        currentHealth = Mathf.Max(0, currentHealth - amount);
        if (currentHealth == 0) 
        {
            DoDeath();
        }
    }

    private void DoDeath()
    {
        //Call animator and then die, animator will call Reset at the end of death animation
    }

    public void Reset()
    {

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
    }

    public void DisableAllAbilities()
    {
        foreach (IAbility ability in abilities)
        {
            ability.Disable();
        }
    }
}
