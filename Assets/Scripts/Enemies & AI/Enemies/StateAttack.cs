using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : FSMState
{
	[SerializeField]
	[Range(0f, 2f)]
	private float minReactionTime = 1f;
	
	[SerializeField]
	[Range(2f, 5f)]
	private float maxReactionTime = 2f;

	[SerializeField]
	private StateChase stateChase;

	private float reactionTime;

	private void OnValidate()
	{
		this.stateChase = this.GetComponent<StateChase>();
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

	private void Attack()
	{
		Debug.LogError("ATTACK");
	}
}