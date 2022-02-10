using UnityEngine;

public class PhysicsMovement : MonoBehaviour, IAbility, IInputReceiverMove, IGameEntity
{
	[SerializeField]
	private float speed = 5f;

	private GameManager gameManager;

	private Animator anim;

	private void Awake()
    {
		TryGetComponent(out anim);    
    }

    private void Move(Vector3 direction)
	{
		Vector3 vel = direction * speed;
		anim.SetFloat("speed", vel.magnitude);
		anim.SetFloat("verticalSpeed", direction.z);
		anim.SetFloat("horizontalSpeed", direction.x);
		this.transform.position += vel * Time.fixedDeltaTime;
		if (direction != Vector3.zero) 
		{ 
			SendActionToGameManager();
		}
	}
	public void Trigger()
	{
		throw new System.Exception("Trigger not available for "+this);
	}

	public void SendActionToGameManager()
	{
		this.gameManager.ActionEventTrigger(Actions.Move);
	}

	void IInputReceiverMove.ReceiveInputMove(Vector3 direction)
	{
		if (this.enabled)
		{ 
			this.Move(direction);
		}
	}

    void IGameEntity.Init(GameManager gameManager)
    {
		this.gameManager = gameManager;
    }

    void IAbility.Enable()
    {
		this.enabled = true;
    }

    void IAbility.Disable()
    {
		this.enabled = false;
    }
}