using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillRule : Rule
{
    private int amountToKill;
    private int amountKilled = 0;

    public KillRule(AllRules ruleName, Sprite icon, int amountToKill, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
    {
        this.RuleName = ruleName;
        this.RuleIcon = icon;
        SetDurationMod(durationMod);
        this.amountToKill = amountToKill;
        this.appliedActions = appliedActions;
        this.mutuallyExclusives = mutuallyExclusives;
        this.ruleRelatedObjects = CalculateKill(ruleRelatedObjects, amountToKill);

    }

    private List<RuleObject> CalculateKill(List<RuleObject> ruleRelatedObjects, int amount)
    {
        List<RuleObject> enemies = ruleRelatedObjects;
        List<RuleObject> enemiesToKill = new List<RuleObject>();
        for (int i = 0; i < amount; i++)
        {
            enemiesToKill.Add(enemies[UnityEngine.Random.Range(0, enemies.Count)]);
        }
        return enemiesToKill;
    }

    public override bool CheckAction(Actions executedAction)
    {
        foreach (Actions action in appliedActions)
        {
            if (action == executedAction)
            {
                return ++amountKilled >= amountToKill;
            }
        }
        return false;
    }
    public override RulePacket ToPacket()
    {
        return new RulePacket(this.RuleName, RuleIcon, amountKilled, amountToKill, this.IsRuleComplete(),IsReverse());
    }

    public override bool IsRuleComplete()
    {
        return amountKilled >= amountToKill;
    }

    public override string ToString()
    {
        return "Kill (" + Mathf.Max(amountToKill - amountKilled, 0) + ")";
    }
}
