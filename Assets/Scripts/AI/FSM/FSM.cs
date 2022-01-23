using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour, IGameEntity
{
	[Header("Debug")]

	[SerializeField]
	private string debug_CurrentState;

	[Header("Setup")]
	
	[SerializeField]
	private FSMState initialState;

	[SerializeField]
	private FSMState[] states;

	[SerializeField]
	private GameManager gameManager;

	private bool canBeKilled;

	private FSMState currentState;

	private void OnValidate()
	{
		this.states = this.GetComponentsInChildren<FSMState>();
	}

	private void Awake()
	{
		for (int i = 0; i < this.states.Length; i++)
		{
			var state = states[i];
			state.Init(this);
		}

		this.ChangeState(this.initialState);
	}

    private void Start()
    {
		this.canBeKilled = true;
    }

    public void ChangeState(FSMState newState)
	{
		if(this.currentState != null)
		{
			this.currentState.OnStateExit();
		}

		this.currentState = newState;

		if (this.currentState != null)
		{
			this.currentState.OnStateEnter();
		}

		this.debug_CurrentState = this.currentState != null ? this.currentState.ToString() : "NULL";
	}

	public void DisalbeStates()
	{
		this.currentState.OnStateExit();
	}

	public void EnableStates()
	{
		this.currentState = initialState;
		this.currentState.OnStateEnter();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponentInChildren<ProjectileController>() != null)
		{
			if (this.canBeKilled == true)
			{
				SendActionToGameManager();
				canBeKilled = false;
			}
		}
	}

	public void SendActionToGameManager()
	{
		this.gameManager.ActionEventTrigger(Actions.Kill);
	}

	public void Init(GameManager gameManager)
    {
		this.gameManager = gameManager;
    }
}