using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using static YorfLib.SingletonHelper;

public class LifeBar : MonoBehaviour
{
    public float Life = 100;

    public GameObject Fuller;
    public UnityEngine.UI.Image Filling;

    private float LifeMax = 100;
    private float LifeMin = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitSingleton(this);
        //Life = LifeMax;
    }

    // Update is called once per frame
    void Update()
    {
        Life = Mathf.Min(Life, LifeMax);
        if (Life >= LifeMax && !Fuller.activeInHierarchy)
        {
            Fuller.SetActive(true);
        }
        else if (Life < LifeMax && Fuller.activeInHierarchy)
        {
            Fuller.SetActive(false);
        }

        Filling.fillAmount = Life / (LifeMax - LifeMin);
        if (Filling.fillAmount < 0.1f)
        {
            Get<GameController>().Loose();
        }
    }
}
