using UnityEngine;

public class PhysicsMovement : MonoBehaviour, IInputReceiverMove
{
	[SerializeField]
	private new Rigidbody rigidbody;
	
	[SerializeField]
	private float speed = 5f;

	private void OnValidate()
	{
		this.rigidbody = this.GetComponentInChildren<Rigidbody>();
	}

	private void Move(Vector3 direction)
	{
		Vector3 vel = direction * speed;
		this.rigidbody.transform.position += vel * Time.fixedDeltaTime;
	}

	void IInputReceiverMove.ReceiveInputMove(Vector3 direction)
	{
		this.Move(direction);
	}
}