using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowStateAttack : FSMState, IEnemy
{
	[SerializeField]
	[Range(0f, 2f)]
	private float minReactionTime = 1f;
	
	[SerializeField]
	[Range(0f, 5f)]
	private float maxReactionTime = 2f;

	[SerializeField]
	private EnemyFollowStateChase stateChase;

	[SerializeField]
	private int damage;

	private bool isPlayerDamageable;

	private PlayerController playerController;

	private float reactionTime;

	private void OnValidate()
	{
		this.stateChase = this.GetComponent<EnemyFollowStateChase>();
	}

    private void Start()
    {
		isPlayerDamageable = false;
    }
    private void Update()
	{
		this.reactionTime -= Time.deltaTime;

		if(this.reactionTime <= 0f)
		{
			this.Attack();
			this.fsm.ChangeState(this.stateChase);
			return;
		}
	}

	public override void OnStateEnter()
	{
		base.OnStateEnter();

		this.reactionTime = Random.Range(this.minReactionTime, this.maxReactionTime);
	}

	public override string ToString()
	{
		return "ATTACK";
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<PlayerController>() != null)
        {
			isPlayerDamageable = true;
			this.playerController = other.GetComponentInChildren<PlayerController>();
		}
    }

    private void OnTriggerExit(Collider other)
    {
		if (other.GetComponentInChildren<PlayerController>() != null)
		{
			isPlayerDamageable = false;
		}
	}

    private void Attack()
	{
		if (isPlayerDamageable == true)
        {
			playerController.TakeDamage(damage);
		}
	}

    public void Chase(GameObject obj)
    {
        throw new System.NotImplementedException();
    }

    public void FleeFrom(GameObject obj)
    {
        throw new System.NotImplementedException();
    }

    public void Dance()
    {
        throw new System.NotImplementedException();
    }
}