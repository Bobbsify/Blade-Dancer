using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachRule : Rule
{
    private bool reached = false;

    public ReachRule(AllRules ruleName, Sprite icon, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
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
                reached = true;
                return reached;
            }
        }
        return true;
    }

    public override bool IsRuleComplete()
    {
        return reached;
    }

    public override string ToString()
    {
        return "Reach The Goal";
    }
}
