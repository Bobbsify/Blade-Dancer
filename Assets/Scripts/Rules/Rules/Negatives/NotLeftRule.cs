using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotLeftRule : Rule
{
    private bool right = true;

    public NotLeftRule(AllRules ruleName, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
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
                right = false;
                return right;
            }
        }
        return true;
    }

    public override bool IsRuleComplete()
    {
        return right;
    }

    public override string ToString()
    {
        return "Stay Left";
    }
}
