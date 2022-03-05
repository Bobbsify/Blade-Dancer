using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotKillRule : Rule
{
    private int amountNotToKill;
    private int amountKilled = 0;

    public NotKillRule(AllRules ruleName, Sprite icon, int amountNotToKill, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
    {
        this.RuleName = ruleName;
        this.RuleIcon = icon;
        SetDurationMod(durationMod);
        this.amountNotToKill = amountNotToKill;
        this.appliedActions = appliedActions;
        this.mutuallyExclusives = mutuallyExclusives;
        this.ruleRelatedObjects = CalculateKill(ruleRelatedObjects, amountNotToKill);

    }

    private List<RuleObject> CalculateKill(List<RuleObject> ruleRelatedObjects, int amount)
    {
        List<RuleObject> enemies = ruleRelatedObjects;
        List<RuleObject> enemiesToKill = new List<RuleObject>();
        for (int i = 0; i < amount; i++)
        {
            enemiesToKill.Add(enemies[UnityEngine.Random.Range(0,enemies.Count)]);
        }
        return enemiesToKill;
    }
    public override RulePacket ToPacket()
    {
        return new RulePacket(this.RuleName, GetRuleIcon(), amountKilled, amountNotToKill, this.IsRuleComplete(),IsReverse());
    }

    public override bool CheckAction(Actions executedAction)
    {
        foreach (Actions action in appliedActions)
        {
            if (action == executedAction)
            {
                return ++amountKilled < 0;
            }
        }
        return true;
    }

    public override bool IsRuleComplete()
    {
        return amountKilled <= 0;
    }

    public override string ToString()
    {
        return "Kill (" + Mathf.Max(amountNotToKill - amountKilled, 0) + ")";
    }
}
