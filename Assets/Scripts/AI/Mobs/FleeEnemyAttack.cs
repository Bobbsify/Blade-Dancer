using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeEnemyAttack : FSMState, IEnemy
{
	[SerializeField]
	[Range(0f, 2f)]
	private float minReactionTime = 1f;

	[SerializeField]
	[Range(0f, 5f)]
	private float maxReactionTime = 2f;

	[SerializeField]
	private FleeEnemyChase stateChase;

	[SerializeField]
	private GameObject projectile;

	[SerializeField]
	private Transform objSpawnPos;

	[SerializeField]
	[Range(0f, 30f)]
	private float ChaseDistance = 5f;

	private GameObject target;

	private string pg = "Player";

	private float reactionTime;

	private void OnValidate()
	{
		this.stateChase = this.GetComponent<FleeEnemyChase>();
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

			reAttack();
		}
	}

	public override void OnStateEnter()
	{
		base.OnStateEnter();

		this.reactionTime = Random.Range(this.minReactionTime, this.maxReactionTime);
	}

	public void reAttack()
    {
		this.reactionTime = Random.Range(this.minReactionTime, this.maxReactionTime);
	}

	public override string ToString()
	{
		return "ATTACK";
	}

	private void Attack()
	{
		GameObject proj = Instantiate(projectile, objSpawnPos.position, objSpawnPos.rotation, null);
		proj.GetComponent<ProjectileController>().SetTeam(Team.Enemy);
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
