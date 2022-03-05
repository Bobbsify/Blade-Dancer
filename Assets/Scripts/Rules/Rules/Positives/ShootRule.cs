using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRule : Rule
{
    private int amountToShoot = 3;
    private float amountShot = 0;
    
    public ShootRule(AllRules ruleName, Sprite icon, int amountToShoot, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
    {
        this.RuleName = ruleName;
        this.RuleIcon = icon;
        SetDurationMod(durationMod);
        this.amountToShoot = amountToShoot;
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
                return ++amountShot >= amountToShoot;
            }
        }
        return false;
    }

    public override RulePacket ToPacket()
    {
        return new RulePacket(this.RuleName, RuleIcon, amountShot, amountToShoot, this.IsRuleComplete(),IsReverse());
    }

    public override bool IsRuleComplete()
    {
        return amountShot >= amountToShoot;
    }

    public override string ToString()
    {
        return "Shoot (" + amountShot + "/" + amountToShoot + ")";
    }
}
