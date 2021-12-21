using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MoveRule : Rule, IRule
{
    private const float MERCY = 1.0f;

    [SerializeField]
    private int minDuration, maxDuration;

    private int duration;
    private float moveTimer = 0;

    public MoveRule()
    {
        this.appliedActions.Add(Actions.Move);
        duration = new System.Random().Next(minDuration, maxDuration);
        durationModifier = duration + MERCY;
    }

    public bool CheckAction(Actions executedAction)
    {
        foreach (Actions action in appliedActions)
        {
            if (action == executedAction)
            {
                this.moveTimer += Time.deltaTime;
                return moveTimer >= duration;
            }
        }
        return false;
    }

    public bool IsRuleComplete()
    {
        return moveTimer >= duration;
    }

    public override string ToString()
    {
        return "Move (" + Mathf.Max(duration - moveTimer,0) + ")";
    }
}

public enum Actions
{
    Move,
    Dash,
    Dance,
    Ring,
}
