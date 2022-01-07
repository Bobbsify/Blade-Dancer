using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    float projectileSpeed = 10.0f;

    [SerializeField]
    [Range(1, 3)]
    int projectileDamage = 1;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void Update()
    {
        this.transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IEnemy enemy))
        {
            Destroy(other.gameObject);
        }
        else if (other.TryGetComponent(out PlayerController pc)) 
        {
            pc.TakeDamage(projectileDamage);
        }
        Destroy(this.gameObject);
    }
}
