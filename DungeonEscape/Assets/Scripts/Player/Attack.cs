using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canHit = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable hit = collision.GetComponent<IDamageable>();
        if(hit != null)
        {
            if (_canHit)
            {
                hit.Damage();
                _canHit = false;
                StartCoroutine(CanHit());
            }
        }
    }

    IEnumerator CanHit()
    {
        yield return new WaitForSeconds(0.5f);
        _canHit = true;
    }
}
