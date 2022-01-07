using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject toFollow;

    private Vector3 startingPos;

    private void Start()
    {
        startingPos = this.transform.position;
        toFollow = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate()
    {
        this.transform.position = toFollow.transform.position + startingPos;
    }
}
