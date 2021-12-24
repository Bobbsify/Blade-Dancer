using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
	[Header("Debug")]

	[SerializeField]
	private string debug_CurrentState;

	[Header("Setup")]
	
	[SerializeField]
	private FSMState initialState;

	[SerializeField]
	private FSMState[] states;

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
}