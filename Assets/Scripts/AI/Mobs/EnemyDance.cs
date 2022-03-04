using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FSM))]
public class EnemyDance : FSMState
{
    [SerializeField]
    private float danceDuration = 2.0f;

    [SerializeField]
    private float danceSpeed = 0.5f;

    [SerializeField]
    private int charge = 1;

    [SerializeField]
    private ParticleSystem danceParticles;

    [SerializeField]
    [Range(0.1f, 5.0f)]
    private float speedMultiplier = 1.5f;

    [SerializeField]
    private float particleDefaultSpeed;

    [SerializeField]
    [Range(4, 20)]
    private float particleDefaultAmount = 4;

    private EnemyIdle stateIdle;

    private GameObject target;

    private float danceTime = 0.0f;

    private void OnValidate()
    {
        if(stateIdle == null)
        TryGetComponent(out stateIdle);

        if (danceParticles == null)
        {
            danceParticles = transform.Find("DanceParticles").GetComponent<ParticleSystem>();
        }
        else
        {
            particleDefaultSpeed = danceParticles.main.startSpeed.constant;
        }
    }

    private void OnEnable()
    {
        TryGetComponent(out stateIdle);
        TryGetComponent(out fsm);
        danceTime = 0.0f;
        StartCoroutine(DoDance());
    }

    private void Update()
    {
        danceTime += Time.deltaTime;
    }

    private IEnumerator DoDance() 
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
        var main = danceParticles.main;
        main.startSpeed = particleDefaultSpeed + (charge * speedMultiplier);
        danceParticles.Emit(Mathf.FloorToInt(charge * particleDefaultAmount));
        yield return new WaitForSeconds(danceSpeed);
        if (danceTime >= danceDuration)
        {
            fsm.ChangeState(stateIdle);
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            this.StopAllCoroutines();
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
}