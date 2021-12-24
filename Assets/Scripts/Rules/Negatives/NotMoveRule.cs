using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NotMoveRule : Rule
{
    bool hasNotMoved = true;

    private float durationModFormula => -2f;

    public NotMoveRule()
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

    public override bool CheckAction(Actions exectuedAction)
    {
        foreach (Actions action in appliedActions)
        {
            if (action == exectuedAction)
            {
                hasNotMoved = false;
                return false;
            }
        }
        return true;
    }

    public override bool IsRuleComplete()
    {
        return hasNotMoved;
    }

    public override string ToString()
    {
        return "Move";
    }
}
