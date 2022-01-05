using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private Animator animPlayer;

    IAbility[] abilities;

    public void DoDeath()
    {
        //Call animator and then die and respawn
    }

    public void Reset()
    {

    }

    private void OnValidate()
    {
       this.animPlayer = this.gameObject.GetComponentInChildren<Animator>(true);
    }

    private void Start()
    {
       this.abilities = GetComponentsInChildren<IAbility>(true);
    }
}
