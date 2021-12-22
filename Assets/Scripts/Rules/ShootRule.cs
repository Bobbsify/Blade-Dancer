using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShootRule : Rule, IRule
{
    [SerializeField]
    [Tooltip("Amount to divide from 'Amount To Shoot' to define time")]
    [Range(1,5)]
    private int durationModDivider = 3;

    [SerializeField]
    [Range(1,4)]
    private int minAmount = 1;

    [SerializeField]
    [Range(5,10)]
    private int maxAmount = 6;

    private int amountToShoot;
    private float amountShot = 0;

    private float durationModFormula => amountToShoot / durationModDivider;

    public ShootRule()
    {
        this.mutuallyExclusives.Add(this);

        amountToShoot = new System.Random().Next(minAmount, maxAmount);

        switch (GetDurationModType())
        {
            case DurationModType.RuleDependant:
                SetDurationMod(durationModFormula);
                break;
            case DurationModType.FixedAmount:
                //Duration is already set in inspector
                break;
            default:
                throw new System.InvalidOperationException("Unkown durationModType: "+ GetDurationModType());
        }

    }

    public bool CheckAction(Actions executedAction)
    {
        foreach (Actions action in appliedActions)
        {
            if (action == executedAction)
            {
                return ++amountShot >= amountToShoot;
            }
        }
        return false;
    }

    public bool IsRuleComplete()
    {
        return amountShot >= amountToShoot;
    }

    public override string ToString()
    {
        return "Shoot (" + amountToShoot + ")";
    }
}
