using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] health;

    private PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        health = new GameObject[transform.childCount];
        int i = 0;
        foreach (Transform t in transform)
        {
            health[i] = t.gameObject;
            i++;
        }
    }

    private void Update()
    {
        UpdateHearts();
    }

    private void UpdateHearts()
    {
        int playerCurrentHealth = player.GetHealth();
        for (int i = playerCurrentHealth; i < player.GetMaxHealth(); i++)
        {
            health[i].SetActive(false);
        }
        for (int i = 0; i > playerCurrentHealth; i++)
        {
            health[i].SetActive(true);
        }
    }
}
