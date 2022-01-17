using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAtPlayer : MonoBehaviour
{
    private GameObject target;

    private void Start()
    {
        this.target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        this.transform.rotation = Quaternion.LookRotation(target.transform.position);
    }
}
