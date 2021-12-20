using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRule
{
    bool CheckAction(IAction action);
}
