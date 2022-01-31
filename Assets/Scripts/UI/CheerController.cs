using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CheerController : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        TryGetComponent(out anim);
    }

    public void ExecuteCheer() 
    {
        anim.SetTrigger("execute");
    }
}
