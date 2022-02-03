using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRanged : FSMState
{
	[SerializeField]
	[Range(0f, 2f)]
	private float minReactionTime = 1f;

	[SerializeField]
	[Range(0f, 5f)]
	private float maxReactionTime = 2f;

	[SerializeField]
	private EnemyFlee stateChase;

	[SerializeField]
	private GameObject projectilePrefab;

	[SerializeField]
	private Transform objSpawnPos;

	[SerializeField]
	[Range(0f, 30f)]
	private float ChaseDistance = 5f;

	private GameObject target;

	private Transform projectilesRoot;

	private string pg = "Player";

	private float reactionTime;

	private void OnValidate()
	{
		this.stateChase = this.GetComponent<EnemyFlee>();
	}

	private void Update()
	{
		target = GameObject.FindGameObjectWithTag(pg);
		var pos = this.transform.position;
		var targetPos = this.target.transform.position;
		var dir = Vector3.Distance(pos, targetPos);

		this.reactionTime -= Time.deltaTime;
		if (this.reactionTime <= 0f)
		{
			this.Attack();

			if(dir < ChaseDistance)
            {
				this.fsm.ChangeState(this.stateChase);
				return;
			}

			ReAttack();
		}
	}

	public override void OnStateEnter()
	{
		base.OnStateEnter();

		this.reactionTime = Random.Range(this.minReactionTime, this.maxReactionTime);
	}

	public void ReAttack()
    {
		this.reactionTime = Random.Range(this.minReactionTime, this.maxReactionTime);
	}

	public void SetProjectilesRoot(Transform root) 
	{
		this.projectilesRoot = root;
	}

	public override string ToString()
	{
		return "ATTACK";
	}

	private void Attack()
	{
		GameObject proj = Instantiate(projectilePrefab, objSpawnPos.position, objSpawnPos.rotation, null);
		proj.transform.parent = projectilesRoot;
		proj.GetComponent<ProjectileController>().SetTeam(Team.Enemy);
	}
}
