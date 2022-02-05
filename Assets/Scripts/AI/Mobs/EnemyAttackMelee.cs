using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackMelee : FSMState
{
	[SerializeField]
	[Range(0f, 2f)]
	private float minReactionTime = 1f;
	
	[SerializeField]
	[Range(0f, 5f)]
	private float maxReactionTime = 2f;

	[SerializeField]
	private EnemyChase stateChase;

	[SerializeField]
	private int damage;

	public bool isPlayerDamageable;

	private PlayerController playerController;

	private float reactionTime;

	[SerializeField]
	private float continueToDamage;

	private void OnValidate()
	{
		this.stateChase = this.GetComponent<EnemyChase>();
	}

    private void Start()
    {
		isPlayerDamageable = true;
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
			this.playerController = other.GetComponentInChildren<PlayerController>();
			StartCoroutine(DamagePlayerIfStillInContact());
		}
    }

    private void OnTriggerExit(Collider other)
    {
		if (other.GetComponentInChildren<PlayerController>() != null)
		{
			isPlayerDamageable = false;
			StopAllCoroutines();
		}
	}

    private void OnTriggerStay(Collider other)
    {
		if (other.GetComponentInChildren<PlayerController>() != null)
		{ 
			StopCoroutine(DamagePlayerIfStillInContact());
		}
	}

    private void Attack()
	{
		if (isPlayerDamageable == true)
        {
			playerController.TakeDamage(damage);
			isPlayerDamageable = false;
		}
	}

	private IEnumerator DamagePlayerIfStillInContact()
    {
		yield return new WaitForSeconds(continueToDamage);
		isPlayerDamageable = true;
		StartCoroutine(DamagePlayerIfStillInContact());
    }
}