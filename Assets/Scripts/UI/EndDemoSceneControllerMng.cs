using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndDemoSceneControllerMng : MonoBehaviour
{
    [SerializeField]
    public GameObject Button;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(Button);
    }
}
