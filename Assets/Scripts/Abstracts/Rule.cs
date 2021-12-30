using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rule : IRule
{
    protected AllRules RuleName { get; set; }
    
    private DurationModType DurationModType { get; set; }
    
    private float durationModifier; // modificatore che definisce la quantità in secondi di cui deve aumentare la durata in presenza di questa regola

    [Header("Rule Functionality Tweaking")]
    [SerializeField]
    protected List<Actions> appliedActions = new List<Actions>(); // actions that cause the rule to be completed

    [SerializeField]
    protected List<RuleObject> ruleRelatedObjects = new List<RuleObject>(); // (Can Be Empty) eventual objects that are correlated to the rule

    [SerializeField]
    protected List<AllRules> mutuallyExclusives = new List<AllRules>(); // (Can Be Empty) eventual objects that are correlated to the rule

    public virtual void SetDurationMod(float amount)
    {
        this.durationModifier = amount;
    }

    public virtual bool IsMutuallyExclusive(AllRules r)
    {
        foreach (AllRules rule in mutuallyExclusives)
        {
            if (rule == r)
            {
                return true;
            }
        }
        return false;
    }

    public virtual List<RuleObject> GetRuleRelatedObjects()
    {
        return this.ruleRelatedObjects;
    }

    public virtual bool IsReverse()
    {
        return this.RuleName.ToString().ToLower().Contains("not");
    }

    public virtual AllRules GetRuleName()
    {
        return this.RuleName;
    }

    public virtual bool CheckAction(Actions executedAction)
    {
        throw new System.NotImplementedException();
    }

    public virtual bool IsRuleComplete()
    {
        throw new System.NotImplementedException();
    }

}

[System.Serializable]
public class RuleObject
{
    [SerializeField]
    private string name;

    [SerializeField]
    private GameObject ruleObjPrefab;

    [SerializeField]
    private PositionType pos;

    public string GetName()
    {
        return this.name;
    }

    public GameObject GetRuleObj()
    {
        return this.ruleObjPrefab;
    }

    public PositionType GetPositionType()
    {
        return this.pos;
    }
}

// 0 Any Random
// 1, 2, 3 Limited Random
// 4, 5, 6... Fixed Positions
public enum PositionType
{
    Random = 0,
    AnyCorner,
    AnyLeft,
    AnyRight,
    TopLeftCorner,
    TopCenter,
    TopRightCorner,
    CenterLeft,
    Center,
    CenterRight,
    BotLeftCorner,
    BotCenter,
    BotRightCorner,
}

//Defines wether or not the amount written in the "Duration Modifier" Box affects the duration of the rule or if it is calculated by the rule itself
public enum DurationModType
{
    RuleDependant,
    FixedAmount
}

public enum Actions
{
    Move,
    Shoot,
    Dash,
    Dance,
    Ring,
    Left,
    Right,
    Break,
    Gather,
    Kill,
    Score,
    Trap,
    Survive,
    Reach
}

public enum AllRules
{
    Move,
    Shoot,
    Dash,
    Dance,
    Ring,
    Left,
    Right,
    Break,
    Gather,
    Kill,
    Score,
    Trap,
    Survive,
    Reach,
    //Negatives
    NotMove,
    NotShoot,
    NotDash,
    NotDance,
    NotRing,
    NotLeft,
    NotRight,
    NotBreak,
    NotGather,
    NotKill,
    NotScore,
    NotTrap,
    NotSurvive,
    NotReach
}
