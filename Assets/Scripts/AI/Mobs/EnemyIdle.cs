using UnityEngine;

public class EnemyIdle : FSMState
{
	[SerializeField]
	private float activationDistance = 3f;

	[SerializeField]
	private FSMState nextState;
	
	[SerializeField]
	private bool force = true;

	private GameObject target;

	private string pg = "Player";


	private void OnValidate()
	{
		if (this.nextState == null) 
		{ 
			this.nextState = this.GetComponentInChildren<FSMState>();
		}
	}

	private void Update()
	{
		if (this.CheckDistance() && !force)
		{
			this.fsm.ChangeState(this.nextState);
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

	public void ForceIdle(bool state) 
	{
		this.force = state;
	}

	public override string ToString()
	{
		return "IDLE";
	}
}