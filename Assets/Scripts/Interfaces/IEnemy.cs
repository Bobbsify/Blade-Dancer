using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void Chase(GameObject obj);

    void FleeFrom(GameObject obj);
   
}
