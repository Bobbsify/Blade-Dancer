using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRule : Rule
{
    private int durationOfMove = 3;
    private float moveTimer = 0;

    public MoveRule(AllRules ruleName, int durationOfMove, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
    {
        this.RuleName = ruleName;
        this.SetDurationMod(durationMod);
        this.durationOfMove = durationOfMove;
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
                this.moveTimer += Time.deltaTime;
                return moveTimer >= durationOfMove;
            }
        }
        return false;
    }

    public override bool IsRuleComplete()
    {
        return moveTimer >= durationOfMove;
    }

    public override string ToString()
    {
        return "Move (" + Mathf.Max(durationOfMove - moveTimer,0) + ")";
    }
}
