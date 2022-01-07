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
}
