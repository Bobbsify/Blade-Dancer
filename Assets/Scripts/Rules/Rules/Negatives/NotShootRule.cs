using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotShootRule : Rule
{
    private int amountNotToShoot = 3;
    private float amountShot = 0;

    public NotShootRule(AllRules ruleName, int amountNotToShoot, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
    {
        this.RuleName = ruleName;
        SetDurationMod(durationMod);
        this.amountNotToShoot = amountNotToShoot;
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
                return ++amountShot < amountNotToShoot;
            }
        }
        return true;
    }
    public override RulePacket ToPacket()
    {
        return new RulePacket(this.RuleName, amountShot + "/" + amountNotToShoot, this.IsRuleComplete(),IsReverse());
    }

    public override bool IsRuleComplete()
    {
        return amountShot < amountNotToShoot;
    }

    public override string ToString()
    {
        return "Shoot (" + Mathf.Max(amountNotToShoot - amountShot, 0) + ")"; 
    }
}
