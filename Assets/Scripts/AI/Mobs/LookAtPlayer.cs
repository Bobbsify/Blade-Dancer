using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private GameObject target;

    private void Start()
    {
        this.target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
       transform.LookAt(target.transform.position);
    }
}
