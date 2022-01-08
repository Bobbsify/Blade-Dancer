using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeEnemyChase : FSMState, IGameEntity
{
	[SerializeField]
	private NavMeshAgent agent;

	[SerializeField]
	[Range(0f, 3f)]
	private float attackDistance = 1f;

	[SerializeField]
	private FleeEnemyAttack stateAttack;

	private Transform target;

	private void OnValidate()
	{
		this.agent = this.GetComponentInChildren<NavMeshAgent>();
		this.stateAttack = this.GetComponentInChildren<FleeEnemyAttack>();
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
		//TODO dire la posizione dell oggetto this.objTarget =
	}

	public override string ToString()
	{
		return "CHASE";
	}
}
