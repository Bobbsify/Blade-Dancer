using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FSM))]
public class EnemyFollowStateDance : FSMState, IEnemy
{
    [SerializeField]
    private float danceDuration = 2.0f;

    [SerializeField]
    private float danceSpeed = 0.5f;

    private FSM fsm;

    private EnemyFollowStateIdle stateIdle;

    private GameObject target;

    private float danceTime = 0.0f;

    private void OnValidate()
    {
        if(stateIdle == null)
        TryGetComponent(out stateIdle);
    }

    private void OnEnable()
    {
        TryGetComponent(out stateIdle);
        TryGetComponent(out fsm);
        StartCoroutine(DoDance());
    }

    private void Update()
    {
        danceTime += Time.deltaTime;
    }

    private IEnumerator DoDance() 
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
        yield return new WaitForSeconds(danceSpeed);
        if (danceTime >= danceDuration)
        {
            fsm.ChangeState(stateIdle);
        }
        else 
        {
            StartCoroutine(DoDance());
        }
    }

	public override string ToString()
	{
		return "DANCE";
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