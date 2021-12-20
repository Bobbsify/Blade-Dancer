using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    void Trigger(GameObject obj);

    void SendActionToGameManager();
}