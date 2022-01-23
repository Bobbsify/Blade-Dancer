using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotTakeDamageRule : Rule
{
    private bool tookNoDamage = true;

    public NotTakeDamageRule(AllRules ruleName, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
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
                tookNoDamage = false;
                return tookNoDamage;
            }
        }
        return tookNoDamage;
    }

    public override bool IsRuleComplete()
    {
        return tookNoDamage;
    }

    public override string ToString()
    {
        return "Take Damage";
    }
}
