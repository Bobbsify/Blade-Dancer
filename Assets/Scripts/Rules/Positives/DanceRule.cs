﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DanceRule : Rule
{
    private float durationModFormula => 1.0f;

    private bool complete = false;

    public DanceRule()
    {

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
                complete = true;
                return complete;
            }
        }
        return false;
    }

    public override bool IsRuleComplete()
    {
        return complete;
    }

    public override string ToString()
    {
        return "Dance";
    }
}
