using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BallObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponentInChildren<ProjectileController>(true))
        {
            Destroy(collision.gameObject);
        }
    }
}
