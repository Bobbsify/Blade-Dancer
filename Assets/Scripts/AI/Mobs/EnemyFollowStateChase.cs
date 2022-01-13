using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowStateChase : FSMState
{
	[SerializeField]
	[Range(0f, 30f)]
	private float deactivationDistance;

	[SerializeField]
	[Range(0f, 3f)]
	private float attackDistance;

	[SerializeField]
	private EnemyFollowStateIdle stateIdle;

	[SerializeField]
	private EnemyFollowStateAttack stateAttack;

	[SerializeField]
	private Transform target;

	private Vector3 dir;

	[SerializeField]
    private float speed;

    private void OnValidate()
	{
		this.stateIdle = this.GetComponentInChildren<EnemyFollowStateIdle>();
		this.stateAttack = this.GetComponentInChildren<EnemyFollowStateAttack>();
	}

	private void Update()
	{
		var pos = this.transform.position;
		var targetPos = this.target.position;
		dir = targetPos - pos;
		this.transform.position += dir * speed * Time.deltaTime;

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

	public override string ToString()
	{
		return "CHASE";
	}
}