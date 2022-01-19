using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotDanceRule : Rule
{
    private float durationModFormula => 1.0f;

    private bool complete = true;

    public NotDanceRule(AllRules ruleName, float duration, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
    {
        this.RuleName = ruleName;
        SetDurationMod(duration);
        this.appliedActions = appliedActions;
        this.mutuallyExclusives = mutuallyExclusives;
        this.ruleRelatedObjects = ruleRelatedObjects;
    }
    public override bool CheckAction(Actions executedAction)
    {
        foreach (Actions action in appliedActions)
        {
            if (action == executedAction)
            {
                complete = false;
                return complete;
            }
        }
        return complete;
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
