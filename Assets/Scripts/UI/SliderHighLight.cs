using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderHighLight : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectButton;

    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            gameObjectButton.GetComponent<Animator>().SetTrigger("Highlighted");
        }

        if (EventSystem.current.currentSelectedGameObject != this.gameObject && EventSystem.current.currentSelectedGameObject != gameObjectButton && EventSystem.current.currentSelectedGameObject != null)
        {
            gameObjectButton.GetComponent<Animator>().SetTrigger("Normal");
        }

    }
}
