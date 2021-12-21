using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IInputEntity, IInputReceiverMove, IInputReceiverShoot
{
    List<IAbility> abilities = new List<IAbility>();
    IInputManager inputManager;

    Dash dashAbility = new Dash();
    Shoot shootAbility = new Shoot();

    [SerializeField]
    private float speed = 5.0f;

    private void Awake()
    {
        abilities.Add(dashAbility);
        abilities.Add(shootAbility);
    }

    public void ReceiveInputMove(Vector3 direction)
    {
        this.transform.position += direction * Time.deltaTime * speed;
    }

    public void InitInput(IInputManager inputManager)
    {
        this.inputManager = inputManager;
    }

    public void ReceiveInputRangedAttack()
    {
        dashAbility.Trigger(gameObject);
    }

    public void ReceiveInputShoot()
    {
       
    }
}
