using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NotMoveRule : Rule, IRule
{
    bool hasNotMoved = true;

    private float durationModFormula => -2f;

    public NotMoveRule()
    {
        //Mtually exclusive rules
        this.mutuallyExclusives.Add(this);
        this.mutuallyExclusives.Add(new MoveRule());

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

    public bool CheckAction(Actions exectuedAction)
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

    public bool IsRuleComplete()
    {
        return hasNotMoved;
    }

    public override string ToString()
    {
        return "Move";
    }
}
