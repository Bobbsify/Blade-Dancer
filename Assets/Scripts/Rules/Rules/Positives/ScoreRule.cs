using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRule : Rule
{
    private bool scored = false;

    public ScoreRule(AllRules ruleName, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
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
                scored = true;
                return scored;
            }
        }
        return true;
    }

    public override bool IsRuleComplete()
    {
        return scored;
    }

    public override string ToString()
    {
        return "Score";
    }
}
