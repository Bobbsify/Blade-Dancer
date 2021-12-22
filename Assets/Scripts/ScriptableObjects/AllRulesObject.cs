using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Rules", menuName = "ScriptableObjects/AllRules", order = 0)]
public class AllRulesObject : ScriptableObject
{
    [Header("-- Move --")]
    public MoveRule moveRule;
    public NotMoveRule doNotMoveRule;

    [Header("-- Shoot --")]
    public ShootRule shootRule;
    public NotShootRule doNotShootRule;

    /* [Header("-- Dash --")]

     [Header("-- Dance --")]
     */
}