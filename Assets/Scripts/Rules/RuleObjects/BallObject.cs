using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class BallObject : MonoBehaviour
{
    [SerializeField]
    private float SetMass;

    private void Start()
    {
        this.GetComponent<Rigidbody>().mass = SetMass; 
    }
}
