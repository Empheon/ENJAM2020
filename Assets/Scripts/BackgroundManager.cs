using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using YorfLib;
using static YorfLib.SingletonHelper;

public class BackgroundManager : MonoBehaviour
{
    public GameObject bg;
    public GameObject fg;

    private Queue<GameObject> m_bgs;
    private Queue<GameObject> m_fgs;


    private void Awake()
    {
    }

    private void Update()
    {
        Vector3 speed = Vector3.left * Get<MusicManager>().BeatDuration * Time.deltaTime * 4 * 10;
        bg.transform.Translate(speed * 0.5f);
        fg.transform.Translate(speed);
    }
}
