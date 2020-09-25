using UnityEngine;
using static YorfLib.SingletonHelper;

namespace YorfLib
{
	[ExecutionOrder(-10000), ExecuteInEditMode]
	public class YorfLibAnchor : MonoBehaviour
	{
		public void Awake()
		{
			InitSingleton(this);

			InitSingleton(gameObject.GetComponent<TimerManager>());
			InitSingleton(gameObject.GetComponent<PerformanceReporter>());
			InitSingleton(gameObject.GetComponent<PerformanceDebugger>());
			//InitSingleton(gameObject.GetComponent<SaveManager>());
			InitSingleton(gameObject.GetComponent<GizmosHelper>());

            //YGameplayCore gameplayCore = gameObject.GetComponent<YGameplayCore>();
            //gameplayCore.InitGameplayCore();
            //InitSingleton(gameplayCore);
		}
	}
}
