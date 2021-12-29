using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DanceRule : Rule
{
    private float durationModFormula => 1.0f;

    private bool complete = false;

    public DanceRule(AllRules ruleName, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
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
                complete = true;
                return complete;
            }
        }
        return false;
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
