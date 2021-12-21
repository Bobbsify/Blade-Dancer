using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rule
{
    protected List<Actions> appliedActions = new List<Actions>(); //azioni che causano il completamento della regola
    protected List<IRule> mutuallyExclusives = new List<IRule>(); // regole con cui non può essere associata questa regola(Bidirezionale)
    protected List<RuleObject> ruleRelatedObjects = new List<RuleObject>(); // (nullable) eventuale oggetti correlati all’utilizzo della regola
    public float durationModifier; // modificatore che definisce la quantità in secondi di cui deve aumentare la durata in presenza di questa regola
}
