using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShootRule : Rule, IRule
{
    [SerializeField]
    private int minAmount, maxAmount;

    private int duration;
    private float moveTimer = 0;

    public ShootRule()
    {

    }

    public bool CheckAction(Actions exectuedAction)
    {
        throw new System.NotImplementedException();
    }

    public bool IsRuleComplete()
    {
        throw new System.NotImplementedException();
    }
}
