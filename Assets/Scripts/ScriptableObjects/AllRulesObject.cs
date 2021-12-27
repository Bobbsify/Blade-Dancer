using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Rules", menuName = "ScriptableObjects/AllRules", order = 0)]
public class AllRulesObject : ScriptableObject
{
    [SerializeField]
    private List<RuleSetting> allRuleSettings;

    public List<RuleSetting> getAll()
    {
        return this.allRuleSettings;
    }
}