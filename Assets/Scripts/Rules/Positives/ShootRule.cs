using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShootRule : Rule
{
    [SerializeField]
    [Tooltip("Amount to divide from 'Amount To Shoot' to define time (works only on Duration Mod Type Rule Dependant)")]
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
        Init();
    }

    public override void Init()
    {
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
                throw new System.InvalidOperationException("Unkown durationModType: " + GetDurationModType());
        }
    }

    public override bool CheckAction(Actions executedAction)
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

    public override bool IsRuleComplete()
    {
        return amountShot >= amountToShoot;
    }

    public override string ToString()
    {
        return "Shoot (" + Mathf.Max(amountToShoot - amountShot,0) + ")";
    }
}
