using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SamEbac.Core.Singleton;

public class Player : Singleton<Player> //, IDamageable
{
    public List<Collider> colliders;
    public CharacterController characterController;
    public Animator anim;

    [Header(" Player Setup")]
    public float turnSpeed = 1f;
    public float gravity = -9.8f;
    public float jumpSpeed = 15f;
    private float vSpeed = 0f;

    [Header(" Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speed = 10f;
    public float speedRun = 2.5f;


    [Header("Flash")]
    public List<FlashColor> flashColors;

    [Header("Life")]
    public HealthBase healthBase;
    private bool _isAlive = true;
    public UIFillUpdate uiFillUpdate;


    private void OnValidate()
    {
        if (healthBase != null) healthBase = GetComponent<HealthBase>();
    }

    protected override void Awake()
    {
        base.Awake();

        OnValidate();

        healthBase.OnDamage += Damage;
        healthBase.Onkill += OnKill;

    }



    #region Life
    private void OnKill(HealthBase h)
    {
        if (_isAlive)
        {
            _isAlive = false;
            anim.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);

            Invoke(nameof(Revive), 3f);        
        } 
    } 


    private void Revive()
    {
        _isAlive = true;
        healthBase.ResetLife();
        anim.SetTrigger("Revive");
        RespawnPlayer();
        Invoke(nameof(TurnOnColliders), .1f);
    }

    private void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);

    }

    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.instance.ChangeVignette();
    }

    public void Damage(float damage, Vector3 dir)
    {
        //Damage(damage);
    }
    #endregion



    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if (characterController.isGrounded)
        {
            vSpeed = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vSpeed = jumpSpeed;
            }

        }

        var isRunning = inputAxisVertical != 0;
        if (isRunning)
        {
            if (Input.GetKey(keyRun))
            {
                speedVector *= speedRun;
                anim.speed = speedRun;
            }
            else
            {
                anim.speed = 1;
            }
        }
        

        anim.SetBool("Run",isRunning);

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;
        
        characterController.Move(speedVector * Time.deltaTime);

    }

    [NaughtyAttributes.Button]
    public void RespawnPlayer()
    {
        if (CheckPointManager.instance.HasCheckPoint())
        {
            transform.position = CheckPointManager.instance.PositionToRespawntCheckPoint();
        }
    }
   
}
