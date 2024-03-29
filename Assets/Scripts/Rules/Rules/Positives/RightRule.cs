﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightRule : Rule
{
    private bool right = false;

    public RightRule(AllRules ruleName, Sprite icon, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
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
                right = true;
                return right;
            }
        }
        return true;
    }

    public override bool IsRuleComplete()
    {
        return right;
    }

    public override string ToString()
    {
        return "Stay Right";
    }
}
