using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [Header("Damage Feedback")]
    [SerializeField]
    private Image DamageImage;

    [SerializeField]
    [Range(0.0f, 2.0f)]
    private float damageAnimDuration;

    [SerializeField]
    [Range(0.0f,1.0f)]
    private float targetOpacity = 0.35f;

    [SerializeField]
    [Range(0.01f, 0.2f)]
    private float fadeAmount = 0.01f;


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
        latestPlayerHealth = player.GetMaxHealth();
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
        float phaseDuration = fadeAmount * damageAnimDuration;

        Color tempColor = DamageImage.color;
        Debug.Log("Operation = " + tempColor.a + " + " + fadeAmount);
        tempColor.a += fadeAmount;
        tempColor.a = Mathf.Max(Mathf.Min(tempColor.a, targetOpacity),0);
        DamageImage.color = tempColor;

        yield return new WaitForSeconds(phaseDuration);

        if (DamageImage.color.a == targetOpacity)
        {
            fadeAmount *= -1;
        }

        if (DamageImage.color.a == 0)
        {
            fadeAmount *= -1;
        }
        else
        {
            StartCoroutine(UIDamageTrigger());
        }

    }
}
