using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotBreakRule : Rule
{
    private bool notBroken = true;

    public NotBreakRule(AllRules ruleName, Sprite icon, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
    {
        this.RuleName = ruleName;
        this.RuleIcon = icon;
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
                notBroken = false;
                return notBroken;
            }
        }
        return notBroken;
    }

    public override bool IsRuleComplete()
    {
        return notBroken;
    }

    public override string ToString()
    {
        return "Break"; 
    }
}
