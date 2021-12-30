using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowStateChase : FSMState, IGameEntity
{
	[SerializeField]
	private NavMeshAgent agent;

	[SerializeField]
	[Range(3f, 6f)]
	private float deactivationDistance = 5f;

	[SerializeField]
	[Range(0f, 3f)]
	private float attackDistance = 1f;

	[SerializeField]
	private EnemyFollowStateIdle stateIdle;

	[SerializeField]
	private EnemyFollowStateAttack stateAttack;

	private Transform target;

	private bool goToFlag;

	private Transform objTarget;

	private void OnValidate()
	{
		this.agent = this.GetComponentInChildren<NavMeshAgent>();
		this.stateIdle = this.GetComponentInChildren<EnemyFollowStateIdle>();
		this.stateAttack = this.GetComponentInChildren<EnemyFollowStateAttack>();
	}

	private void Update()
	{
		if (this.agent.isActiveAndEnabled)
		{
			this.agent.SetDestination(this.target.position);

			var pos = this.transform.position;
			var targetPos = this.target.position;

			var distance = Vector3.Distance(pos, targetPos);

			if (distance > this.deactivationDistance)
			{
				this.fsm.ChangeState(this.stateIdle);
				return;
			}
			
			if (distance <= this.attackDistance)
			{
				this.fsm.ChangeState(this.stateAttack);
				return;
			}
		}

		if (goToFlag)
        {
			this.agent.SetDestination(this.objTarget.position);
        }
	}

	public void FlagReach()
    {
		this.goToFlag = true;
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