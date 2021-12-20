using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RuleObjects", menuName = "ScriptableObjects/RuleObjects", order = 1)]
public class RuleObjects : ScriptableObject
{
    public RuleObject[] allRuleObjects;

    public RuleObject GetObject(string name)
    {
        foreach (RuleObject obj in allRuleObjects) 
        {
            if (obj.name == name) return obj;
        }
        throw new System.MissingFieldException("Non è stato trovato un RuleObject con il nome " + name);
    }
}

[System.Serializable]
public class RuleObject 
{

    public string name;
    public GameObject ruleObjPrefab;
    public PositionType pos;
}

public enum PositionType
{
    Random,
    AnyCorner,
    TopLeftCorner,
    TopCenter,
    TopRightCorner,
    CenterLeft,
    Center,
    CenterRight,
    BotLeftCorner,
    BotCenter,
    BotRightCorner,
    AnyLeft,
    AnyRight,
}