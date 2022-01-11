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
        Vector3 position = toFollow.transform.position + startingPos;
        position.x = startingPos.x;
        position.z = startingPos.z;
        this.transform.position = position;
    }
}
