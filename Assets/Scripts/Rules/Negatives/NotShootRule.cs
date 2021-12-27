using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NotShootRule : Rule
{
    [SerializeField]
    [Range(0, 4)]
    private int minAmount = 0;

    [SerializeField]
    [Range(5, 8)]
    private int maxAmount = 6;

    private int amountNotToShoot;
    private float amountShot = 0;

    private float durationModFormula => 1.0f;

    public override void Init()
    {
        amountNotToShoot = UnityEngine.Random.Range(minAmount, maxAmount);

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
                return ++amountShot < amountNotToShoot;
            }
        }
        return true;
    }

    public override bool IsRuleComplete()
    {
        return amountShot < amountNotToShoot;
    }

    public override string ToString()
    {
        return "Shoot (" + Mathf.Max(amountNotToShoot - amountShot, 0) + ")"; 
    }
}
