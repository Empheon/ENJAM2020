using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

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
        //Life = LifeMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (Life >= LifeMax && !Fuller.activeInHierarchy)
        {
            Fuller.SetActive(true);
        }
        else if (Life < LifeMax && Fuller.activeInHierarchy)
        {
            Fuller.SetActive(false);
        }

        Filling.fillAmount = Life / (LifeMax - LifeMin);
    }
}
