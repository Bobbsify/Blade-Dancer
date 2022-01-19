using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageRule : Rule
{
    private bool tookDamage = false;
    
    public TakeDamageRule(AllRules ruleName, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
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
                tookDamage = true;
                return tookDamage;
            }
        }
        return tookDamage;
    }

    public override bool IsRuleComplete()
    {
        return tookDamage;
    }

    public override string ToString()
    {
        return "Take Damage";
    }
}
