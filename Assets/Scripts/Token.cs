using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static YorfLib.SingletonHelper;
using DG.Tweening;

public class Token : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -10)
        {
            Get<GameController>().PoolReturnToken(this);
            return;
        }

        transform.Translate(Vector3.left * 0.1f);
    }
}
