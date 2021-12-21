using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NotMoveRule : Rule, IRule
{
    bool hasNotMoved = true;

    public NotMoveRule()
    {
        //Azioni che la completano
        this.appliedActions.Add(Actions.Move);
        this.appliedActions.Add(Actions.Dash);

        //Regole mutualmente esclusive
        this.mutuallyExclusives.Add(new MoveRule());
    }

    public bool CheckAction(Actions exectuedAction)
    {
        foreach (Actions action in appliedActions)
        {
            if (action == exectuedAction)
            {
                hasNotMoved = false;
                return false;
            }
        }
        return true;
    }

    public bool IsRuleComplete()
    {
        return hasNotMoved;
    }
}
