using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NotShootRule : Rule, IRule
{
    [SerializeField]
    [Range(0, 4)]
    private int minAmount = 0;

    [SerializeField]
    [Range(5, 8)]
    private int maxAmount = 6;

    private int amountToShoot;
    private float amountShot = 0;

    private float durationModFormula => 1.0f;
    
    public NotShootRule()
    {
        this.mutuallyExclusives.Add(this);

        switch (GetDurationModType())
        {
            case DurationModType.RuleDependant:
                SetDurationMod(durationModFormula);
                break;
            case DurationModType.FixedAmount:
                //Duration is already set in inspector
                break;
            default:
                throw new System.InvalidOperationException("Unkown durationModType: " + GetDurationModType());
        }

    }

    bool IRule.CheckAction(Actions executedAction)
    {
        foreach (Actions action in appliedActions)
        {
            if (action == executedAction)
            {
                return ++amountShot < amountToShoot;
            }
        }
        return true;
    }

    bool IRule.IsRuleComplete()
    {
        return amountShot < amountToShoot;
    }

    public override string ToString()
    {
        return "Shoot (" + amountToShoot + ")"; 
    }
}
