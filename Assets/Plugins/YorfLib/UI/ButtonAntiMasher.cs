using UnityEngine;
using UnityEngine.UI;

namespace YorfLib
{
	/// <summary>
	/// Prevent users from mashing the button
	/// </summary>
	public class ButtonAntiMasher : MonoBehaviour
	{
		[SerializeField]
		private float m_duration = 0.5f;

		private Timer m_timerUntilActivation;
		private Button m_button;

		private void Awake()
		{
			m_button = GetComponent<Button>();
			m_button.onClick.AddListener(OnButtonClick);
			m_timerUntilActivation = new UnscaledTimer(m_duration, this, () => m_button.interactable = true, true);
		}

		private void OnButtonClick()
		{
			m_button.interactable = false;
			m_timerUntilActivation.Start();
		}
	}
}
