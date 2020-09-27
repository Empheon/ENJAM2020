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

    public static float Speed;

    private void Awake()
    {
        Speed = 0;
    }

    private void Update()
    {
        if (bg != null)
        {
            bg.transform.Translate(Vector3.left * Speed * Time.deltaTime * 10 * 0.5f);
        }
        fg.transform.Translate(Vector3.left * Speed * Time.deltaTime * 10);
    }
}
