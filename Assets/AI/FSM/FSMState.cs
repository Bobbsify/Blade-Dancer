using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState : MonoBehaviour
{
	protected FSM fsm;

	public virtual void OnStateEnter() 
	{
		this.enabled = true;
	}

	public virtual void OnStateExit() 
	{
		this.enabled = false;
	}

	public void Init(FSM fsm)
	{
		this.fsm = fsm;
		this.enabled = false;
	}
}