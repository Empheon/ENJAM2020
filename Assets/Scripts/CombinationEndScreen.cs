using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using static YorfLib.SingletonHelper;

public class CombinationEndScreen : MonoBehaviour
{
    public GameObject TurningBg;
    public GameObject CharIllu;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    public void PlayAnim()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(1, 0.1f).SetEase(Ease.OutElastic));
        seq.AppendInterval(Get<MusicManager>().BeatDuration * 2 - 0.2f);
        seq.Append(transform.DOScale(0, 0.1f).SetEase(Ease.OutElastic));
    }

    private void Update()
    {
        TurningBg.transform.Rotate(new Vector3(0, 0, 1));
    }
}

