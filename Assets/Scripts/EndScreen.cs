using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static YorfLib.SingletonHelper;

public class EndScreen : MonoBehaviour
{
    private Animator m_animator;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        InitSingleton(this);
        m_animator = GetComponent<Animator>();
    }

    public void AnimateEndScreen(bool b, string score)
    {
        m_animator.SetBool("IsShown", b);
        scoreText.text = "Score: " + score;
    } 
}
