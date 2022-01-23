using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    void Trigger();
    void SendActionToGameManager();
    void Enable();
    void Disable();
}