using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject searchRoot;

    private Animator animPlayer;

    IAbility[] abilities;

    private void OnValidate()
    {
       this.animPlayer = this.gameObject.GetComponentInChildren<Animator>(true);
    }

    private void Start()
    {
       this.abilities = this.searchRoot.GetComponentsInChildren<IAbility>(true);
    }

    void TryTriggerAbility()
    {
        
    }

    public void DoDeath()
    {
        //Call animator and then die and respawn
    }
}
