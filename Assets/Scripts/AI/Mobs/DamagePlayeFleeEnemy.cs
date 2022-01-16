using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayeFleeEnemy : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private int redamageable;

    private bool isDamageable;

    private PlayerController playerController;

    private void Start()
    {
        isDamageable = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponentInChildren<PlayerController>() != null)
        {
            this.playerController = other.GetComponentInChildren<PlayerController>();

            if(isDamageable == true)
            {
                playerController.TakeDamage(damage);
                isDamageable = false;
                StartCoroutine(reDamageable());
            }
           
        }
    }

    private IEnumerator reDamageable()
    {
        yield return new WaitForSeconds(redamageable);
        isDamageable = true;
    }
}
