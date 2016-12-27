using UnityEngine;
using System.Collections;

public class Player : PlayerManager
{
    public static Transform oppenent;    
    public static Player player;
    public Transform Cam;

    public bool IsAttacking;
    public bool IsMove;
    public bool IsDied;

    RaycastHit hit;
    NavMeshAgent navAgent;

    public GameObject attackSensor;
    public Animator Anim; 

    void Awake ()
    {
        InitAnimator();
        navAgent = GetComponent<NavMeshAgent>();
        attackSensor.SetActive(false);
        player = this;
        IsAttacking = false;
        IsMove = false;
        IsDied = false;
        damage = 15;
    }

	void Update ()
    {
        Attack();
        Move();
        Animate();
        MoveChk();
        if (HP <= 0 && !IsDied)
        {
            Death();
        }

        if (EnemyFSM.PlayerHit)
        {
            Invoke("IsHit", 0.3f);
            Invoke("MotionDelay", 0.5f);
        }
        Debug.Log(EnemyFSM.PlayerHit);
    }

    void InitAnimator()
    {
        Anim.SetInteger("WeaponState", 4);
        Anim.SetBool("Idling", true);
        Anim = GetComponentInChildren<Animator>();
    }

    void MotionDelay()
    {
        IsMove = false;
        IsAttacking = false;
    }

    void Move()
    {
        if (Input.GetMouseButton(0))
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, (1 << 8) | (1 << 9));
            navAgent.SetDestination(hit.point);
            Anim.SetBool("Idling", false);

            IsMove = true;
        }
    }

    void Animate()
    {
        if (!IsAttacking)
        {
            if (navAgent.velocity.magnitude > 0.5f)
            {
                Anim.SetBool("Idling", false);
            }
            else
            {
                Anim.SetBool("Idling", true);
            }
        }
    }

    void Attack()
    {        
        if (Input.GetMouseButtonDown(1))
        {
            if(!IsAttacking)
            {
                IsAttacking = true;

                if(oppenent != null)
                {
                    transform.LookAt(oppenent.position);
                }
                //Cam.GetComponent<CameraShaking>().StartShaking();
                attackSensor.SetActive(true);
                Anim.SetTrigger("Use");
                Anim.SetBool("Idling", true);

                Invoke("IsAttack", attackSpeed);
                Debug.Log("공격");
            }
        }
        else
        {
            attackSensor.SetActive(false);            
        }        
    }

    void IsAttack()
    {
        IsAttacking = false;
    }

    void MoveChk()
    {        
        if (IsAttacking)
        {
            Anim.SetBool("Idling", true);
            navAgent.Stop();            
        }
        else
        {
            navAgent.Resume();
        }     
    }

    void IsHit()
    {
        Anim.CrossFade("Dual.Pain", 0);
        Anim.SetTime(0);
        Cam.GetComponent<CameraShaking>().StartShaking();
        IsAttacking = true;
        Invoke("IsAttack", 1f);
    }

    void Death()
    {
        Anim.SetInteger("Death", 1);
        IsAttacking = true;
    }
}
