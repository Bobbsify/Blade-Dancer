using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NotDashRule : Rule
{
    [SerializeField]
    [Tooltip("Amount to divide from 'Amount of dashes' to define time")]
    [Range(1, 5)]
    private int durationModDivider = 3;

    [SerializeField]
    [Range(1, 4)]
    private int minAmount = 1;

    [SerializeField]
    [Range(5, 10)]
    private int maxAmount = 6;

    private int targetAmountOfDashes;
    private float dashAmount = 0;

    private float durationModFormula => targetAmountOfDashes / durationModDivider;

    public NotDashRule()
    {
        Init();

    }

    public override void Init()
    {
        targetAmountOfDashes = new System.Random().Next(minAmount, maxAmount);

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
                return ++dashAmount < targetAmountOfDashes;
            }
        }
        return false;
    }

    public override bool IsRuleComplete()
    {
        return dashAmount < targetAmountOfDashes;
    }

    public override string ToString()
    {
        return "Dash (" + targetAmountOfDashes + ")";
    }
}
