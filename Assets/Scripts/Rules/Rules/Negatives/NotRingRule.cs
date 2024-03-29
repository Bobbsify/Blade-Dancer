﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NotRingRule : Rule
{
    private bool notRang = true;

    public NotRingRule(AllRules ruleName, Sprite icon, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
    {
        this.RuleName = ruleName;
        this.RuleIcon = icon;
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
                notRang = false;
                return notRang;
            }
        }
        return notRang;
    }

    public override bool IsRuleComplete()
    {
        return notRang;
    }

    public override string ToString()
    {
        return "Ring";
    }
}
