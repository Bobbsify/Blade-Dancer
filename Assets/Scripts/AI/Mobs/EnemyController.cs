﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(FSM))]
public class EnemyController : MonoBehaviour, IEnemy, IGameEntity
{
    [SerializeField]
    private float maxHealth = 1;

    [SerializeField]
    private SoundPacket deathSound;

    private float currentHealth;

    private Animator animator;

    private FSM stateMachine;

    private GameManager gameManager;

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

    public void DeleteEnemy() 
    {
        Destroy(gameObject);
    }

    public void SetProjectilesRoot(Transform projectilesRoot) 
    {
        if (TryGetComponent(out EnemyAttackRanged rangedAttack))
        { 
            rangedAttack.SetProjectilesRoot(projectilesRoot);
        }
    }

    private void Die() 
    {
        gameManager.PlaySound(deathSound);
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
            stateMachine.ChangeState(dance);
        }
    }

    public void Go()
    {
        if (TryGetComponent(out EnemyIdle idle))
        {
            stateMachine.ChangeState(idle);
            idle.ForceIdle(false);
        }
    }

    public void Stop()
    {
        if (TryGetComponent(out EnemyIdle idle))
        {
            stateMachine.ChangeState(idle);
            idle.ForceIdle(true);
        }

        if(TryGetComponent(out EnemyAttackMelee enemyAttackMelee))
        {
            enemyAttackMelee.NotDamageOnEndStage();
        }
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
