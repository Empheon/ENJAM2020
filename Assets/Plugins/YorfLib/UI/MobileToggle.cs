using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YorfLib;

[ExecuteInEditMode]
public class MobileToggle : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private bool m_toggleEnabled;

    [SerializeField]
    private float m_padding;

    [Header("Color")]
    [SerializeField]
    private Color m_disabledColor = Color.white;

    [SerializeField]
    private Color m_enabledColor = Color.green;

    [Header("Animation")]
    [SerializeField]
    private EasingType m_easingType = EasingType.CUBIC_INOUT;

    [SerializeField]
    private float m_speed = 10.0f;

    [SerializeField]
    private BoolEvent m_onToggleEvent;

    private RectTransform m_toggleKnob;
    private Image m_toggleBackground;
    private Coroutine m_currentRoutine;

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        m_toggleBackground = GetComponent<Image>();
        m_toggleKnob = (RectTransform)transform.GetChild(0);
        UpdateToggleState();
    }

    private void UpdateToggleState()
    {
        m_toggleKnob.anchoredPosition = GetKnobPosForState(m_toggleEnabled);
        m_toggleBackground.color = m_toggleEnabled ? m_enabledColor : m_disabledColor;
    }

    private Vector2 GetKnobPosForState(bool toggleEnabled)
    {
        RectTransform toggleTransform = (RectTransform) transform;
        return toggleEnabled ? new Vector2(toggleTransform.sizeDelta.x - m_padding - m_toggleKnob.sizeDelta.x, 0.0f) : new Vector2(m_padding, 0.0f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_toggleEnabled = !m_toggleEnabled;
        m_onToggleEvent.Invoke(m_toggleEnabled);
        StartToggleAnimation();
    }

    private void StartToggleAnimation()
    {
        if(m_currentRoutine != null)
        {
            StopCoroutine(m_currentRoutine);
        }

        m_currentRoutine = StartCoroutine(ToggleAnimation());
    }

    private IEnumerator ToggleAnimation()
    {
        float timer = 0.0f;

        Vector2 originalPosition = m_toggleKnob.anchoredPosition;
        Color originalColor = m_toggleBackground.color;

        while(timer < 1.0f)
        {
            timer += Time.deltaTime * m_speed;

            float easedTime = Easing.Ease(m_easingType, timer);

            m_toggleBackground.color = Color.Lerp(originalColor, m_toggleEnabled ? m_enabledColor : m_disabledColor, easedTime);
            m_toggleKnob.anchoredPosition = Vector2.Lerp(originalPosition, GetKnobPosForState(m_toggleEnabled), easedTime);
            yield return null;
        }

        m_currentRoutine = null;
    }

    private void OnValidate()
    {
        Init();
    }
}

[Serializable]
public class BoolEvent : UnityEvent<bool>
{
}
