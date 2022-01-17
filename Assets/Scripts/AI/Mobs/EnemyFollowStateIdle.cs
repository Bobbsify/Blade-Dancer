using UnityEngine;

public class EnemyFollowStateIdle : FSMState, IEnemy
{
	[SerializeField]
	private float activationDistance = 3f;

	[SerializeField]
	private EnemyFollowStateChase stateChase;

	private GameObject target;

	private string pg = "Player";

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