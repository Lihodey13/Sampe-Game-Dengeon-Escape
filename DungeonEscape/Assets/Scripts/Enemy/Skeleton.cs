using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
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
        Debug.Log("skeleton gem value = " + gems);
        Health = health;
        gems = 200;
    }
}
