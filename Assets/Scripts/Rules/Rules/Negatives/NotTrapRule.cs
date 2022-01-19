using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotTrapRule : Rule
{
    private bool notTrapped = true;

    public NotTrapRule(AllRules ruleName, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
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
                notTrapped = true;
                return notTrapped;
            }
        }
        return notTrapped;
    }

    public override bool IsRuleComplete()
    {
        return notTrapped;
    }

    public override string ToString()
    {
        return "Trap";
    }
}
