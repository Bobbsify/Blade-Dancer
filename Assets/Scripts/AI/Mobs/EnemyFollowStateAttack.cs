using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowStateAttack : FSMState
{
	[SerializeField]
	[Range(0f, 2f)]
	private float minReactionTime = 1f;
	
	[SerializeField]
	[Range(0f, 5f)]
	private float maxReactionTime = 2f;

	[SerializeField]
	private EnemyFollowStateChase stateChase;

	[SerializeField]
	private Transform target;

	private bool isPlayerDamageable;

	private float reactionTime;

	private void OnValidate()
	{
		this.stateChase = this.GetComponent<EnemyFollowStateChase>();
	}

    private void Start()
    {
		isPlayerDamageable = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<PlayerController>() != null)
        {
			isPlayerDamageable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
		if (other.GetComponentInChildren<PlayerController>() != null)
		{
			isPlayerDamageable = false;
		}
	}

    private void Attack()
	{
		if (isPlayerDamageable == true)
        {
			Debug.LogError("ATTACK"); // mettere il danno o distruggere il player
		}
	}
}