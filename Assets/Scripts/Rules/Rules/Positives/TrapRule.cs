using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRule : Rule
{
    private bool trapped = false;

    public TrapRule(AllRules ruleName, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
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
                trapped = true;
                return trapped;
            }
        }
        return trapped;
    }

    public override bool IsRuleComplete()
    {
        return trapped;
    }

    public override string ToString()
    {
        return "Trap";
    }
}
