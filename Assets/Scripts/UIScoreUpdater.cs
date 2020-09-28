using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreUpdater : MonoBehaviour
{
    public Text scoreText;
    private float score;

    public void UpdateText(float f)
    {
        score += f;
        scoreText.text = Mathf.RoundToInt(score * 100).ToString();
    }
}
