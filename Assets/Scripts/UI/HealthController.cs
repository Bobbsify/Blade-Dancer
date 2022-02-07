using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private Image DamageImage;

    [SerializeField]
    [Range(0.0f, 2.0f)]
    private float DamageAnimDuration;

    private GameObject[] health;

    private PlayerController player;

    private Color whiteColor = new Color(0.95311f, 0.95311f, 0.95311f);
    private Color purpleColor = new Color(0.56078f, 0.35294f, 0.85882f);

    private int latestPlayerHealth;



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
        latestPlayerHealth = player.GetHealth();
    }

    public void UpdateHearts()
    {
        int playerCurrentHealth = player.GetHealth();
        if (playerCurrentHealth < latestPlayerHealth) 
        {
            //Do damage Anim
            StartCoroutine(UIDamageTrigger());
        }

        for (int i = playerCurrentHealth; i < player.GetMaxHealth(); i++)
        {
            health[i].GetComponent<Image>().color = whiteColor;
        }
        for (int i = 0; i < playerCurrentHealth; i++)
        {
            health[i].GetComponent<Image>().color = purpleColor;
        }
        latestPlayerHealth = playerCurrentHealth;
    }

    private IEnumerator UIDamageTrigger() 
    {
        DamageImage.enabled = true;
        yield return new WaitForSeconds(DamageAnimDuration);
        DamageImage.enabled = false;
    }
}
