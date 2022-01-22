using UnityEngine;

public class EnemyIdle : FSMState
{
	[SerializeField]
	private float activationDistance = 3f;

	[SerializeField]
	private EnemyChase stateChase;

	private GameObject target;

	private string pg = "Player";

	private void OnValidate()
	{
		this.stateChase = this.GetComponentInChildren<EnemyChase>();
	}

	private void Update()
	{
		if (this.CheckDistance())
		{
			this.fsm.ChangeState(this.stateChase);
			return;
		}
	}

	private bool CheckDistance()
	{
		target = GameObject.FindGameObjectWithTag(pg);
		var pos = this.transform.position;
		var targetPos = this.target.transform.position;

		var distance = Vector3.Distance(pos, targetPos);

		return distance >= this.activationDistance;
	}

	public override string ToString()
	{
		return "IDLE";
	}
}