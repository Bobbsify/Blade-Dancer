using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRule
{
    //Checks if current action completes the rule
    bool CheckAction(IAction action);
}
