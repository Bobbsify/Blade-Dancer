using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NotMoveRule : Rule
{
    bool hasNotMoved = true;

    public NotMoveRule(AllRules ruleName, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
    {
        this.RuleName = ruleName;
        this.SetDurationMod(durationMod);
        this.appliedActions = appliedActions;
        this.mutuallyExclusives = mutuallyExclusives;
        this.ruleRelatedObjects = ruleRelatedObjects;
    }
    
    public override bool CheckAction(Actions exectuedAction)
    {
        foreach (Actions action in appliedActions)
        {
            if (action == exectuedAction)
            {
                hasNotMoved = false;
                return hasNotMoved;
            }
        }
        return hasNotMoved;
    }

    public override bool IsRuleComplete()
    {
        return hasNotMoved;
    }

    public override string ToString()
    {
        return "Move";
    }
}
