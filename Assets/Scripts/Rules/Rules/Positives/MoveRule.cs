using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRule : Rule
{
    private int durationOfMove = 3;
    private float moveTimer = 0.0f;

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
                this.moveTimer += Time.fixedDeltaTime;
                return moveTimer >= durationOfMove;
            }
        }
        return false;
    }
    public override RulePacket ToPacket()
    {
        float time = durationOfMove - moveTimer;
        float truncatedTime = (float)Math.Round(time * 100f) / 100f;
        return new RulePacket(this.RuleName, ""+Mathf.Max(truncatedTime,0), this.IsRuleComplete(), IsReverse());
    }

    public override bool IsRuleComplete()
    {
        return moveTimer >= durationOfMove;
    }

    public override string ToString()
    {
        float time = durationOfMove - moveTimer;
        float truncatedTime = (float)Math.Round(time * 100f) / 100f;
        return "Move (" + Mathf.Max(truncatedTime, 0) + ")";
    }
}
