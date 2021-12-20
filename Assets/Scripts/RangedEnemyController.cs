using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : MonoBehaviour, IEnemy
{
    [SerializeField]
    private float speed;

    private GameObject player;

    public bool Act;

    private string playerTag = "Player";

    private void Update()
    {
        if (Act)
        {
            FleeFrom(player);
        }
    }

    public void GetPlayer()
    {
        this.player = GameObject.FindGameObjectWithTag(playerTag);
    }

    public void Chase(GameObject obj)
    {
       
    }

    public void FleeFrom(GameObject obj)
    {
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, -(obj.transform.position), this.speed * Time.deltaTime);
    }
}
