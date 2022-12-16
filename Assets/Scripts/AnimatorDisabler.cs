using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorDisabler : MonoBehaviour
{
    private Animator m_Animator;
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        //m_Animator.StopPlayback();
    }

    public void DisableAnimator()
    {
        Debug.Log($"animation event");
        //m_Animator.StopPlayback();
        //m_Animator.enabled = false;
    }
}
