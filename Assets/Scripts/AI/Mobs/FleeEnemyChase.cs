﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeEnemyChase : FSMState, IEnemy
{
	[SerializeField]
	[Range(0f, 30f)]
	private float attackDistance = 1f;

	[SerializeField]
	private FleeEnemyAttack stateAttack;

	[SerializeField]
	private float speed;

	private GameObject target;

	private string pg = "Player";

	private Vector3 dir;

    private void OnValidate()
	{
		this.stateAttack = this.GetComponentInChildren<FleeEnemyAttack>();
	}

	private void Update()
	{
		target = GameObject.FindGameObjectWithTag(pg);
		var pos = this.transform.position;
		var targetPos = this.target.transform.position;
		dir = (targetPos - pos) * Time.deltaTime;
		dir.y = 0;
		this.transform.position += -(dir) * speed * Time.deltaTime;

		var distance = Vector3.Distance(pos, targetPos);

		if (distance >= this.attackDistance)
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
