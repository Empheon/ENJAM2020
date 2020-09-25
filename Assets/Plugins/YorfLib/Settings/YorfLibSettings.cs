using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YorfLib
{
	public class YorfLibSettings : ScriptableObjectSingleton<YorfLibSettings>
    {
		[Header("Save System")]
		//public ES3.EncryptionType m_encryptionType = ES3.EncryptionType.AES;

		public string m_encryptionKey = "";

		//public ES3.Location m_saveLocation = ES3.Location.File;

		public string m_saveFileName = "SaveData";

		[Tooltip("Change save data format version. Do not do this unless you prepared your class data")]
		public int m_saveVersion = 1;

		public bool m_canSaveData = true;

		[Header("Performance")]
		public int m_targetFPS = 60;

		public bool m_displayPerformance = false;
	}
}
