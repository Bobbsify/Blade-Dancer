using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RangedEnemyFlee : FSMState, IGameEntity
{
	[SerializeField]
	private NavMeshAgent agent;

	[SerializeField]
	[Range(0f, 3f)]
	private float attackDistance = 1f;

	[SerializeField]
	private StateIdle stateIdle;

	[SerializeField]
	private StateAttack stateAttack;

	private Transform target;

	private void OnValidate()
	{
		this.agent = this.GetComponentInChildren<NavMeshAgent>();
		this.stateIdle = this.GetComponentInChildren<StateIdle>();
		this.stateAttack = this.GetComponentInChildren<StateAttack>();
	}

	private void Update()
	{
		if (this.agent.isActiveAndEnabled)
		{
			this.agent.SetDestination(-(this.target.position));

			var pos = this.transform.position;
			var targetPos = this.target.position;

			var distance = Vector3.Distance(pos, targetPos);

			if (distance >= this.attackDistance)
			{
				this.fsm.ChangeState(this.stateAttack);
				return;
			}
		}
	}

	void IGameEntity.Init(GameManager gameManager)
	{
		this.target = gameManager.PlayerPawn.transform;
	}

	public override string ToString()
	{
		return "CHASE";
	}
}
