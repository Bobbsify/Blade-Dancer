using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MoveRule : Rule
{
    private const float MERCY = 1.0f;

    [SerializeField]
    [Range(2,5)]
    private int minDuration = 2;

    [SerializeField]
    [Range(5, 10)]
    private int maxDuration = 10;

    private int durationOfMove;
    private float moveTimer = 0;

    private float durationModFormula => durationOfMove + MERCY;

    public MoveRule()
    {
        durationOfMove = new System.Random().Next(minDuration, maxDuration);

        switch (GetDurationModType())
        {
            case DurationModType.RuleDependant:
                SetDurationMod(durationModFormula);
                break;
            case DurationModType.FixedAmount:
                //Duration is already set in inspector
                break;
            default:
                throw new System.InvalidOperationException("Unkown durationModType: " + GetDurationModType());
        }
    }

    public override bool CheckAction(Actions executedAction)
    {
        foreach (Actions action in appliedActions)
        {
            if (action == executedAction)
            {
                this.moveTimer += Time.deltaTime;
                return moveTimer >= durationOfMove;
            }
        }
        return false;
    }

    public override bool IsRuleComplete()
    {
        return moveTimer >= durationOfMove;
    }

    public override string ToString()
    {
        return "Move (" + Mathf.Max(durationOfMove - moveTimer,0) + ")";
    }
}
