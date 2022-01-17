using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowStateChase : FSMState, IEnemy
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

	private GameObject target;

	private GameObject flagTarget;

	private string pg = "Player";

	private string flag = "Flag";

	private Vector3 dir;

	private Vector3 dirFlag;

	[SerializeField]
    private float speed;

    private void OnValidate()
	{
		this.stateIdle = this.GetComponentInChildren<EnemyFollowStateIdle>();
		this.stateAttack = this.GetComponentInChildren<EnemyFollowStateAttack>();
	}

	private void Update()
	{
		target = GameObject.FindGameObjectWithTag(pg);
		flagTarget = GameObject.FindGameObjectWithTag(flag);

		var pos = this.transform.position;
		var targetPos = this.target.transform.position;

		if (flagTarget != null)
        {
			var flagTargetPos = this.flagTarget.transform.position;
			dirFlag = flagTargetPos - pos;
			this.transform.position += dirFlag * speed * Time.deltaTime;
		}

        else
        {
			dir = targetPos - pos;
			this.transform.position += dir * speed * Time.deltaTime;
		}
		
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