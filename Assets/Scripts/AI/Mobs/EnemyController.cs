using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(FSM))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 1;

    private float currentHealth;

    private Animator animator;

    private FSM stateMachine;

    private void Start()
    {
        TryGetComponent(out stateMachine);
        TryGetComponent(out animator);

        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount) 
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        if (currentHealth == 0) 
        {
            Die();
        }
    }

    public void DestroyEnemy() 
    {
        Destroy(gameObject);
    }

    private void Die() 
    {
        stateMachine.DisalbeStates();
        animator.SetTrigger("death");
    }
}
