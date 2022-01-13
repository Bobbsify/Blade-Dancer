using UnityEngine;

public class EnemyFollowStateIdle : FSMState
{
	[SerializeField]
	private float activationDistance = 3f;

	[SerializeField]
	private EnemyFollowStateChase stateChase;

	[SerializeField]
	private Transform target;

	private void OnValidate()
	{
		this.stateChase = this.GetComponentInChildren<EnemyFollowStateChase>();
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
		var pos = this.transform.position;
		var targetPos = this.target.position;

		var distance = Vector3.Distance(pos, targetPos);

		return distance >= this.activationDistance;
	}

	public override string ToString()
	{
		return "IDLE";
	}
}