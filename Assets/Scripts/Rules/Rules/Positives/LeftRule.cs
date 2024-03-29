﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRule : Rule
{
    private bool left = false;

    public LeftRule(AllRules ruleName, Sprite icon, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
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
                left = true;
                return left;
            }
        }
        return true;
    }

    public override bool IsRuleComplete()
    {
        return left;
    }

    public override string ToString()
    {
        return "Stay Left";
    }
}
