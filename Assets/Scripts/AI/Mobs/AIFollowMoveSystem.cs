using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFollowMoveSystem : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

	private IInputReceiverMove inputReceiverMove;

	private void OnValidate()
	{
		this.agent = this.GetComponentInChildren<NavMeshAgent>();
	}

	private void Awake()
	{
		this.inputReceiverMove = this.GetComponentInChildren<IInputReceiverMove>();
	}

	private void Update()
	{
		if (this.agent.isActiveAndEnabled)
		{
			this.Animate();
		}
	}

	private void Animate()
	{
		var velocity = this.agent.velocity;
		var direction = velocity.normalized;

		this.inputReceiverMove.ReceiveInputMove(direction);
	}
}