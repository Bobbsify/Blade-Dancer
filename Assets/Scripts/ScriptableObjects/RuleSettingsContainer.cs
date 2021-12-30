using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllRulesContainer", menuName = "ScriptableObjects/AllRulesContainer",order = 0)]
public class RuleSettingsContainer : ScriptableObject
{
    public List<RuleSetting> ruleSettings;

    internal List<RuleSetting> getAll()
    {
        return this.ruleSettings;
    }
}
