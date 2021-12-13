using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _speed;
    [SerializeField] private int health = 5;
    private bool _grounded = false;
    [SerializeField] private LayerMask _ground;
    private bool _resetJump = false;
    [SerializeField] private Animator _anim;
    [SerializeField] PlayerAnimation _playerAnimation;
    private SpriteRenderer _spritePlayer;
    [SerializeField] private SpriteRenderer _spriteSword;
    private bool _jumping = false;
    public int _diamonds = 0;
    private bool _isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        _spritePlayer = GetComponentInChildren<SpriteRenderer>();
        if(_spritePlayer == null)
        {
            Debug.Log("Sprite is null");
        }

        Health = health;
        Debug.Log("Diamonds start " + _diamonds);
    }

    public int Health { get; set; }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Attack();
    }

    void PlayerMovement()
    {
        if (!_isDead)
        {
            float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
            _grounded = Grounded();
            FlipPlayer(horizontalInput);
            Debug.DrawRay(transform.position, Vector2.down * 0.8f, Color.green);
            if (CrossPlatformInputManager.GetButtonDown("B_btn") && Grounded())
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                StartCoroutine(ResetJumpRoutine());
                _jumping = true;
                _playerAnimation.JumpPlayer(_jumping);
            }

            _rb.velocity = new Vector2(horizontalInput * _speed, _rb.velocity.y);
            _playerAnimation.MovePlayer(horizontalInput);
        }
    }

    void Attack()
    {
        if(CrossPlatformInputManager.GetButtonDown("A_btn") && Grounded())
        {
            _playerAnimation.Attack();
        }
    }

    void FlipPlayer(float horizontalInput)
    {
        if (horizontalInput > 0f)
        {
            _spritePlayer.flipX = false;
            _spriteSword.flipX = false;
            _spriteSword.flipY = false;
        }
        else if (horizontalInput < 0f)
        {
            _spritePlayer.flipX = true;
            _spriteSword.flipX = true;
            _spriteSword.flipY = true;
        }
    }

    bool Grounded()
    {
       RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, _ground.value);
        
        if(hitInfo.collider != null)
        {
            if (!_resetJump)
            {
                //Debug.Log("Grounded");
                _jumping = false;
                _playerAnimation.JumpPlayer(_jumping);
                return true;
            }
        }

        return false;
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.05f);
        _resetJump = false;
    }


    public void Damage()
    {
        Health--;
        Debug.Log("Player health is: " + Health);
        //update Ui display
        UIManager.Instance.UpdateLives(Health);
        if (Health == 0)
        {
            _isDead = true;
            _anim.SetTrigger("death");
            Destroy(this.gameObject, 2);
        }
    }

    public void UpdateDiamonds(int amount)
    {
        _diamonds += amount;
        Debug.Log("Gem count is: " + _diamonds);
        UIManager.Instance.UpdateGemCountOnHud(_diamonds);
    }

    public void MinusDiamonds(int amount)
    {
        _diamonds -= amount;
    }

}
