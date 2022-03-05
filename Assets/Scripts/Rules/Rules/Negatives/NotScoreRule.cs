using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotScoreRule : Rule
{
    private bool notScored = true;

    public NotScoreRule(AllRules ruleName, Sprite icon, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
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
                notScored = false;
                return notScored;
            }
        }
        return true;
    }
    public override bool IsRuleComplete()
    {
        return notScored;
    }

    public override string ToString()
    {
        return "Score"; 
    }
}
