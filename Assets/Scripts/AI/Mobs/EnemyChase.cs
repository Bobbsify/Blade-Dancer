using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : FSMState
{
	[SerializeField]
	[Range(0f, 30f)]
	private float deactivationDistance = 15.0f;

	[SerializeField]
	[Range(0f, 3f)]
	private float attackDistance = 0.5f;

	[SerializeField]
	private EnemyIdle stateIdle;

	[SerializeField]
	private EnemyAttackMelee stateAttack;

	[SerializeField]
	private float speed = 7.0f;

	private GameObject target;

	private GameObject flagTarget;

	private string pg = "Player";

	private string flag = "Flag";

    private void OnValidate()
	{
		this.stateIdle = this.GetComponentInChildren<EnemyIdle>();
		this.stateAttack = this.GetComponentInChildren<EnemyAttackMelee>();
	}

    private void Awake()
    {
		target = GameObject.FindGameObjectWithTag(pg);
		flagTarget = GameObject.FindGameObjectWithTag(flag);
	}

    private void Update()
	{
		var targetPos = this.target.transform.position;

		if (flagTarget != null)
        {
			var flagTargetPos = this.flagTarget.transform.position;
			this.transform.position = Vector3.MoveTowards(this.transform.position, flagTargetPos, speed * Time.deltaTime);
		}
        else
        {
			this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);
		}
		
		var distance = Vector3.Distance(this.transform.position, targetPos);

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