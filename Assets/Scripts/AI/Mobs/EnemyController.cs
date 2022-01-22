using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(FSM))]
public class EnemyController : MonoBehaviour, IEnemy
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

    public void Chase()
    {
        if (TryGetComponent(out EnemyChase chase)) 
        {
            stateMachine.ChangeState(chase);
        }
    }

    public void Flee()
    {
        if (TryGetComponent(out EnemyFlee flee))
        {
            stateMachine.ChangeState(flee);
        }
    }

    public void Dance()
    {
        if (TryGetComponent(out EnemyDance dance))
        {
            Debug.Log("Do a lil' dance");
            stateMachine.ChangeState(dance);
        }
    }
}
