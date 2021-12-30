using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotReachRule : Rule
{
    private bool notReached = true;

    public NotReachRule(AllRules ruleName, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
    {
        this.RuleName = ruleName;
        SetDurationMod(durationMod);
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
                notReached = false;
                return notReached;
            }
        }
        return true;
    }

    public override bool IsRuleComplete()
    {
        return notReached;
    }

    public override string ToString()
    {
        return "Reach The Goal";
    }
}
