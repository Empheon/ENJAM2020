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

    private SpriteRenderer m_symbol;

    // Start is called before the first frame update
    void Awake()
    {
        m_symbol = GetComponent<SpriteRenderer>();
        Speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -10)
        {
            gameObject.SetActive(false);
            return;
        }

        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }

    public void Init(ButtonType type, BeatCombination bc)
    {
        Color c;
        if (Get<InputController>().isXbox)
        {
            m_symbol.sprite = Get<GameSettings>().XboxButtonSprite[type];
            if (Get<GameSettings>().XboxButtonColor.TryGetValue(type, out c))
            {
                c.a = 1;
                m_symbol.color = c;
            } else
            {
                m_symbol.color = Get<GameSettings>().ArrowButtonsColor;
            }
        } else
        {
            m_symbol.sprite = Get<GameSettings>().PlayStationButtonSprite[type];
            if (Get<GameSettings>().XboxButtonColor.TryGetValue(type, out c))
            {
                c.a = 1;
                m_symbol.color = c;
            } else
            {
                m_symbol.color = Get<GameSettings>().ArrowButtonsColor;
            }
        }

        bc.OnFailedAction += FailedAction;
        bc.OnMissedAction += MissedAction;
        bc.OnValidAction += ValidAction;
        bc.OnPressFailedAction += FailedPressAction;
    }

    private void ValidAction()
    {

    }

    private void FailedAction()
    {

    }

    private void MissedAction()
    {

    }

    private void FailedPressAction()
    {

    }
}
