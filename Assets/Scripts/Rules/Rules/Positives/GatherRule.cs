using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherRule : Rule
{
    private int amountToGather;
    private int amountGathered = 0;

    public GatherRule(AllRules ruleName, int amountToGather, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
    {
        this.RuleName = ruleName;
        SetDurationMod(durationMod);
        this.amountToGather = amountToGather;
        this.appliedActions = appliedActions;
        this.mutuallyExclusives = mutuallyExclusives;
        this.ruleRelatedObjects = CalculateGather(ruleRelatedObjects, amountToGather);

    }

    private List<RuleObject> CalculateGather(List<RuleObject> ruleRelatedObjects, int amount)
    {
        RuleObject collectible = new RuleObject(ruleRelatedObjects[0].GetName(), ruleRelatedObjects[0].GetRuleObj(), ruleRelatedObjects[0].GetPositionType());
        List<RuleObject> amountToGather = new List<RuleObject>();
        for (int i = 0; i < amount; i++)
        {
            amountToGather.Add(collectible);
        }
        return amountToGather;
    }

    public override bool CheckAction(Actions executedAction)
    {
        foreach (Actions action in appliedActions)
        {
            if (action == executedAction)
            {
                return ++amountGathered >= amountToGather;
            }
        }
        return false;
    }
    public override RulePacket ToPacket()
    {
        return new RulePacket(this.RuleName, amountGathered + "/" + amountToGather, this.IsRuleComplete());
    }

    public override bool IsRuleComplete()
    {
        return amountGathered >= amountToGather;
    }

    public override string ToString()
    {
        return "Gather (" + Mathf.Max(amountToGather - amountGathered,0) + ")";
    }
}
