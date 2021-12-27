using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotDashRule : Rule
{
    private int targetAmountOfDashes = 3;
    private float dashAmount = 0;

    public NotDashRule(AllRules ruleName, int targetAmountOfDashes, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
    {
        this.RuleName = ruleName;
        this.targetAmountOfDashes = targetAmountOfDashes;
        this.SetDurationMod(durationMod);
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
                return ++dashAmount < targetAmountOfDashes;
            }
        }
        return false;
    }

    public override bool IsRuleComplete()
    {
        return dashAmount < targetAmountOfDashes;
    }

    public override string ToString()
    {
        return "Dash (" + Mathf.Max(targetAmountOfDashes - dashAmount, 0) + ")";
    }
}
