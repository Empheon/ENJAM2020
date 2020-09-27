using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YorfLib;
using static YorfLib.SingletonHelper;

public class ActionButton : MonoBehaviour
{
    private Animator m_animator;
    
    void Start()
    {
        InitSingleton(this);
      
        m_animator = GetComponent<Animator>();
    }
    

    public void SuccessAnim()
    {
        m_animator.SetTrigger("success");
    }

    public void FailAnim()
    {
        m_animator.SetTrigger("fail");
    }
}
