﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttack : FSMState
{
	[SerializeField]
	[Range(0f, 2f)]
	private float minReactionTime = 1f;

	[SerializeField]
	[Range(2f, 5f)]
	private float maxReactionTime = 2f;

	[SerializeField]
	private RangedEnemyFlee stateChase;


	[SerializeField]
	GameObject projectile;

	[SerializeField]
	Transform projectileSpawnPosition;
	private float reactionTime;

	private void OnValidate()
	{
		this.stateChase = this.GetComponent<RangedEnemyFlee>();
	}

	private void Update()
	{
		this.reactionTime -= Time.deltaTime;

		if (this.reactionTime <= 0f)
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

	private void Attack()
	{
		Instantiate(this.projectile, this.projectileSpawnPosition);
	}
}