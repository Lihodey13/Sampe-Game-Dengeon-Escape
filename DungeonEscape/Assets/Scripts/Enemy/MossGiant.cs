using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }

    public void Damage()
    {
        Health--;
        TakeDamage(Health);
    }
    public override void Init()
    {
        base.Init();
        Debug.Log("moss giant gem value = " + gems);
        Health = health;
        gems = 500;
    }

}
