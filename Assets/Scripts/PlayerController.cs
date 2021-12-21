using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IInputReceiverMove
{
    List<IAbility> abilities = new List<IAbility>();

    [SerializeField]
    private float speed = 5.0f;

    private void Awake()
    {
    }

    public void ReceiveInputMove(Vector3 direction)
    {
        this.transform.position += direction * Time.deltaTime * speed;
    }
}
