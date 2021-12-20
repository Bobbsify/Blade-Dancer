using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rule
{
    List<IAction> appliedActions; //azioni che causano il completamento della regola
    List<IRule> mutuallyExclusives; // regole con cui non può essere associata questa regola(Bidirezionale)
    List<RuleObject> ruleRelatedObjects; // (nullable) eventuale oggetti correlati all’utilizzo della regola
    float durationMultiplier; // moltiplicatore che definisce la quantità in secondi di cui deve aumentare la durata in presenza di questa regola

}
