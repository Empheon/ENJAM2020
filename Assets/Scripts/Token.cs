using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static YorfLib.SingletonHelper;
using DG.Tweening;

public class Token : MonoBehaviour
{
    public static float Speed;

    public SpriteRenderer Circle;
    public SpriteRenderer Shiny;

    public GameObject Heart;
    public GameObject Dog1;
    public GameObject Dog1Dead;

    private ButtonType m_buttonType;

    private SpriteRenderer m_symbol;

    // Start is called before the first frame update
    void Awake()
    {
        m_symbol = GetComponent<SpriteRenderer>();
        Speed = 0;

        Dog1.SetActive(false);
        Dog1Dead.SetActive(false);
        Heart.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -20)
        {
            gameObject.SetActive(false);
            return;
        }

        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }

    public void Init(ButtonType type, BeatCombination bc)
    {
        m_buttonType = type;
        
        switch(type)
        {
            case ButtonType.DOG1:
                Circle.enabled = false;
                Shiny.enabled = false;
                m_symbol.enabled = false;



                Dog1.SetActive(true);
                return;
        }

        Color c;
        if (Get<InputController>().isXbox)
        {
            m_symbol.sprite = Get<GameSettings>().XboxButtonSprite[type];
            if (Get<GameSettings>().XboxButtonColor.TryGetValue(type, out c))
            {
                c.a = 1;
                m_symbol.color = c;
                Circle.color = c;
            } else
            {
                m_symbol.color = Get<GameSettings>().ArrowButtonsColor;
                Circle.enabled = false;
                Shiny.enabled = false;
            }
        } else
        {
            m_symbol.sprite = Get<GameSettings>().PlayStationButtonSprite[type];
            if (Get<GameSettings>().XboxButtonColor.TryGetValue(type, out c))
            {
                c.a = 1;
                m_symbol.color = c;
                Circle.color = Get<GameSettings>().ArrowButtonsColor;
            } else
            {
                m_symbol.color = Get<GameSettings>().ArrowButtonsColor;
                Circle.enabled = false;
                Shiny.enabled = false;
            }
        }
    }

    public void ValidAction()
    {
        PressButtonAnim();
        Get<ActionButton>().SuccessAnim();
    }

    public void FailedAction()
    {
        PressButtonAnim();
        Get<ActionButton>().FailAnim();
    }

    public void MissedAction()
    {

    }

    public void FailedPressAction()
    {
        Get<ActionButton>().FailAnim();
    }

    private void PressButtonAnim()
    {
        float scaleX = transform.localScale.x;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(scaleX * 1.2f, 0.1f).SetEase(Ease.OutSine));
        seq.Append(transform.DOScale(scaleX, 0.1f).SetEase(Ease.InSine));
        seq.Play();
    }

    public void ActivateHeart()
    {
        Heart.SetActive(true);
    }

    public void DeadDog()
    {
        switch(m_buttonType)
        {
            case ButtonType.DOG1:
                Dog1.SetActive(false);
                Dog1Dead.SetActive(true);
                break;
        }
    }
}
