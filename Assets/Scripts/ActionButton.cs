using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YorfLib;
using static YorfLib.SingletonHelper;
using DG.Tweening;

public class ActionButton : MonoBehaviour
{
    private Animator m_animator;
    public Animator CharacterAnimator;
    
    void Start()
    {
        InitSingleton(this);
      
        m_animator = GetComponent<Animator>();

        Get<MusicManager>().OnMusicBeat += CharacterAnim;
    }
    

    public void SuccessAnim()
    {
        m_animator.SetTrigger("success");
    }

    public void FailAnim()
    {
        m_animator.SetTrigger("fail");
    }

    private void CharacterAnim()
    {
        CharacterAnimator.SetTrigger("play");
        float scaleY = CharacterAnimator.transform.localScale.y;

        CharacterAnimator.transform.DOScaleY(scaleY, 0.5f * 1.5f + 1).SetEase(Ease.InOutElastic);
    }
}
