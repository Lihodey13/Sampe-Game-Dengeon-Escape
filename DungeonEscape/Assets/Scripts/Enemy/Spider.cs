using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    [SerializeField] private GameObject _acidPrefab;
    [SerializeField] Transform _acidFiringPosition;
    private bool _attack = true;
    public int Health { get; set; }

    public void Damage()
    {
        if (isDead)
        {
            return;
        }
        Health--;
        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("death");
            Instantiate(_diamondPrefab, this.transform.position, Quaternion.identity);
            _diamond.UpdateDiamondValue(base.gems);
            Destroy(this.gameObject, 1.5f);
        }
    }
    public override void Init()
    {
        base.Init();
        Debug.Log("spider gem value = " + gems);
        Health = health;
        gems = 50;
    }

    public override void Movement()
    {
        
    }

    public override void Attack()
    {
        GameObject acid = Instantiate(_acidPrefab, _acidFiringPosition.position, Quaternion.identity);
    }

    IEnumerator SpiderAttack()
    {
        //_attack = true;
        yield return new WaitForSeconds(2f);
        _attack = false;
        anim.SetBool("attack", _attack);
        yield return new WaitForSeconds(2f);
        _attack = true;
    }

    public override void Update()
    {
        if (_attack)
        {
            anim.SetBool("attack", _attack);
            StartCoroutine(SpiderAttack());
        }
    }

}
