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

    private void Awake()
    {
        currentHealth = maxHealth;
        abilities = GetComponentsInChildren<IAbility>();
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
        //Call animator and then die and respawn
    }

    public void Reset()
    {

    }

    private void OnValidate()
    {
       this.animPlayer = this.gameObject.GetComponentInChildren<Animator>(true);
    }

    private void Start()
    {
       this.abilities = GetComponentsInChildren<IAbility>(true);
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
