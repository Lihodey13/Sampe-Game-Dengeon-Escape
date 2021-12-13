using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int speed;
    protected int gems;

    [SerializeField] protected Transform pointA, pointB;
    [SerializeField] protected GameObject _diamondPrefab;
    protected Diamond _diamond;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected bool ishit = false;

    protected Player player;
    protected bool isDead;

    public virtual void Attack()
    {

    }

    private void Start()
    {
        Init();
        _diamond = _diamondPrefab.GetComponent<Diamond>();
        if(_diamond == null)
        {
            Debug.Log("Diamond null");
        }
        
    }

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public virtual void Movement()
    {
        if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            anim.SetTrigger("idle");
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            anim.SetTrigger("idle");
        }

        if (!ishit)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
       // Debug.Log("Distance is : " + distance);
        if (distance > 2f)
        {
            //Debug.Log("Distance is : " + distance);
            ishit = false;
            anim.SetBool("inCombat", false);
        }

        Vector3 direction = player.transform.localPosition - transform.localPosition;
        //Debug.Log("Direction is : " + direction.x);

        if(direction.x > 0 && anim.GetBool("inCombat")==true)
        {
            sprite.flipX = false;
        }
        else if(direction.x < 0 && anim.GetBool("inCombat") == true)
        {
            sprite.flipX = true;
        }

    }

    public virtual void Update() {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle") && anim.GetBool("inCombat") == false)
        {
            return;
        }

        Movement();
    }
    
    public virtual void TakeDamage(int newHealth)
    {
        if (isDead)
        {
            return;
        }

        health = newHealth;
        anim.SetTrigger("hit");
        ishit = true;
        anim.SetBool("inCombat", true);

        if (health < 1)
        {
            isDead = true;
            anim.SetTrigger("death");
            CreateDiamond();
            Destroy(this.gameObject, 1.5f);
        }
    }

    public void CreateDiamond()
    {
        Instantiate(_diamondPrefab, this.transform.position, Quaternion.identity);
        _diamond.UpdateDiamondValue(gems);
    }

}
