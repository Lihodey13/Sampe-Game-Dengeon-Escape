using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    Spider spider;

    private void Start()
    {
        spider = GetComponentInParent<Spider>();
        if(spider == null)
        {
            Debug.Log("Spider is null");
        }
    }

    public void Fire()
    {
        spider.Attack();
    }
}
