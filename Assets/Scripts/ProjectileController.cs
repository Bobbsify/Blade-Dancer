using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 10.0f;

    [SerializeField]
    [Tooltip("Seconds the projectile stays alive for (Set to 0 for infinite lifetime)")]
    [Range(0,30)]
    private int projectileLifetime = 5;

    [SerializeField]
    [Range(1, 3)]
    private int projectileDamage = 1;

    private Team projectileTeam;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
        if (projectileLifetime != 0) 
        {
            StartCoroutine(LifeTime());
        }
    }

    void Update()
    {
        this.transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (projectileTeam) 
        {
                case Team.Player:
                if (other.TryGetComponent(out EnemyController enemy))
                {
                    enemy.TakeDamage(projectileDamage);
                }
                if (other.tag != "Player")
                {
                    Destroy(this.gameObject);
                }
                break;
                case Team.Enemy:
                if (other.TryGetComponent(out PlayerController pc)) 
                {
                    pc.TakeDamage(projectileDamage);
                    Destroy(this.gameObject);
                }
                if (!other.TryGetComponent(out IEnemy e))
                {
                    Destroy(this.gameObject);
                }
                break;
        }
    }

    public void SetTeam(Team team) 
    {
        this.projectileTeam = team;
    }

    private IEnumerator LifeTime() 
    {
        yield return new WaitForSeconds(projectileLifetime);
        if (gameObject != null) { 
            Destroy(this.gameObject);
        }
    }
}

public enum Team 
{ 
    Player,
    Enemy
}
