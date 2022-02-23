using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfRideController : MonoBehaviour
{
    [SerializeField]
    private Sprite officeSprite;

    [SerializeField]
    private Transform carPosition;

    [SerializeField]
    private Vector3 distanceSpawned;

    [SerializeField]
    private float slowTime;

    [SerializeField]
    private List<Transform> itemsToMove;

    private float slowFraction = 100;

    private GameObject office;

    public void Execute() 
    {
        office = new GameObject();
        SpriteRenderer spriteRenderer = (SpriteRenderer) office.AddComponent(typeof(SpriteRenderer));
        spriteRenderer.sprite = officeSprite;
        office.transform.position = carPosition.position + distanceSpawned;
        StartCoroutine(DoSlow());
    }

    private IEnumerator DoSlow()
    {
        Vector3 movementDistance = new Vector3(distanceSpawned.x / slowFraction, 0, 0);
        for (int i = 0; i < slowFraction; i++)
        {
            office.transform.position -= movementDistance;
            foreach (Transform item in itemsToMove) 
            {
                item.position -= movementDistance;
            }
            yield return new WaitForSeconds(slowTime / slowFraction);
        }
    }
}
