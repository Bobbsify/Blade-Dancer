using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject searchRoot;

    [SerializeField]
    private string DashInput;

    [SerializeField]
    private string DanceInput;

    [SerializeField]
    private string ShootInput;

    Dash dash;

    private Animator animPlayer;

    IAbility[] abilities;

    private void OnValidate()
    {
        this.animPlayer = this.gameObject.GetComponentInChildren<Animator>(true);
    }

    private void Start()
    {
       // this.abilities = searchRoot.GetComponentInChildren<IAbility>(true);
    }

    void TryTriggerAbility(string name)
    {
        if (Input.GetButtonDown(this.DashInput))
        {
            this.dash.Trigger(this.gameObject);
        }    
    }

    public void DoDeath()
    {
        //Call animator and then die and respawn
    }
}
