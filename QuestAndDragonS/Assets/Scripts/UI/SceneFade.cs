using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFade : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private int startTrigger = Animator.StringToHash("startFade");

    public void StartFade()
    {
        animator.SetTrigger(startTrigger);
    }
}
