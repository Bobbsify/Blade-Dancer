using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rule
{
    [SerializeField]
    [Tooltip("Defines wether or not the amount written in the \"Duration Modifier\" Box affects the duration of the rule or if it is calculated by the rule itself")]
    private DurationModType durationModType;

    [SerializeField]
    private float durationModifier; // modificatore che definisce la quantità in secondi di cui deve aumentare la durata in presenza di questa regola

    [SerializeField]
    protected List<Actions> appliedActions = new List<Actions>(); // actions that cause the rule to be completed

    [SerializeField]
    protected List<RuleObject> ruleRelatedObjects = new List<RuleObject>(); // (Can Be Empty) eventual objects that are correlated to the rule

    protected List<IRule> mutuallyExclusives = new List<IRule>(); // rules this rule cannot be associated with

    public virtual void SetDurationMod(float amount)
    {
        durationModifier = amount;
    }

    public virtual DurationModType GetDurationModType()
    {
        return this.durationModType;
    }
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
    Dash,
    Dance,
    Ring,
    Shoot,
    Left,
    Right,
    Score,
    Reach,
    Gather,
}
