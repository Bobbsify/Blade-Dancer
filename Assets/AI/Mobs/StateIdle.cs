using UnityEngine;

public class StateIdle : FSMState, IGameEntity
{
	[SerializeField]
	private float activationDistance = 3f;

	[SerializeField]
	private StateChase stateChase;

	private Transform target;

	private void OnValidate()
	{
		this.stateChase = this.GetComponentInChildren<StateChase>();
	}

	private void Update()
	{
		if (this.CheckDistance())
		{
			this.fsm.ChangeState(this.stateChase);
			return;
		}
	}

	void IGameEntity.Init(GameManager gameManager)
	{
		this.target = gameManager.PlayerPawn.transform;
	}

	private bool CheckDistance()
	{
		var pos = this.transform.position;
		var targetPos = this.target.position;

		var distance = Vector3.Distance(pos, targetPos);

		return distance <= this.activationDistance;
	}

	public override string ToString()
	{
		return "IDLE";
	}
}