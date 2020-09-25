namespace YorfLib
{
	public interface IYorfLibWindowTab
	{
		void OnEnable();

		void OnGUI();

		string GetTabName();
	}
}
