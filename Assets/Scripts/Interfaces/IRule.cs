using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRule
{
    //Checks if current action completes the rule
    bool CheckAction(Actions executedAction);

    //Ritorna true se la regola in quel momento è effettivamente completata
    bool IsRuleComplete();
}
