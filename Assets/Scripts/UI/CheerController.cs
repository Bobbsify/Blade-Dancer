using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class CheerController : MonoBehaviour
{
    private Animator anim;

    private List<Text> cheerTexts = new List<Text>();

    private void OnValidate()
    {
        if (cheerTexts.Count == 0) 
        { 
            cheerTexts.AddRange(GetComponentsInChildren<Text>(true));
        }
    }

    private void Awake()
    {
        TryGetComponent(out anim);
    }

    public void ExecuteCheer(string cheer) 
    {
        foreach (Text text in cheerTexts) 
        {
            text.text = cheer;
        }
        anim.SetTrigger("execute");
    }
}
