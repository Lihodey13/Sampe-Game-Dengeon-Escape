using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Animator _animSword;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePlayer(float speed)
    {
        _anim.SetFloat("Speed", Mathf.Abs(speed));
    }

    public void JumpPlayer(bool jumping)
    {
        _anim.SetBool("Jumping", jumping);
        //Debug.Log("Jumping is " + jumping);
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack");
        _animSword.SetTrigger("SwordArc");
    }
}
