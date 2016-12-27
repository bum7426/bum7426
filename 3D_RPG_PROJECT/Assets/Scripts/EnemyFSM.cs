using UnityEngine;
using System.Collections;
using System;


public class EnemyFSM : MonsterManager
{
    Player player;
    PlayerManager Level;
    NavMeshAgent navAgent;
    Vector3 OriginPos;

    public Animation anim;

    bool block;
    bool IsDamaged;
    bool IsMoving;
    bool IsAttacking;
    bool IsDied;
    public static bool PlayerHit;

    void Start ()
    {
        navAgent = GetComponent<NavMeshAgent>();
        OriginPos = transform.position;
        Idle();
        IsDied = false;
        PlayerHit = false;        
    }
	
	void Update ()
    {
        Animate();
        Move();
        Attack();
        if(HP <= 0 && !IsDied)
        {
            Death();
        }
    }

    void OnMouseEnter()
    {
        Player.oppenent = transform;
        Debug.Log("힝!");
    }
    
    void Idle()
    {
        if(!IsDied) anim.CrossFade("creature1Idle", 0.25f);
    }

    void Move()
    {
        if(!IsAttacking && !IsDied)
        {
            if (IsInRange(chasingRange))
            {                
                navAgent.stoppingDistance = 2.0f;
                navAgent.SetDestination(player.transform.position);
                transform.LookAt(player.transform.position);
                IsMoving = true;
            }
            else
            {                
                navAgent.stoppingDistance = -2.0f;
                navAgent.SetDestination(OriginPos);
                IsMoving = true;
            }
        }
    }

    void Attack()
    {
        if (IsInRange(AttackRange))
        {
            if (!block && !IsAttacking && !IsDied)
            {
                IsAttacking = true;
                block = true;
                float RA = UnityEngine.Random.Range(0, 10);
                if (RA>=8f)
                {
                    Debug.Log("크리티컬!!!!");
                    PlayerHit = true;                    
                    anim.CrossFade("creature1Attack1", 0.5f);
                    transform.LookAt(player.transform.position);
                    Invoke("P_Hit", 0.03f);                    
                }
                else
                {
                    anim.CrossFade("creature1Attack2", 0.5f);                    
                    transform.LookAt(player.transform.position);                    
                    Debug.Log("나맞음");
                }
                player.HP = player.HP - damage;
                Invoke("UnBlock", attackSpeed);
                Invoke("IsAttack", attackSpeed);
            }
        }
    }

    void P_Hit()
    {
        PlayerHit = false;        
    }

    void Death()
    {
        IsDied = true;
        anim.Play("creature1Die");
        Destroy(gameObject, 5f);
    }

    void UnBlock()
    {
        block = false;
    }

    void Damaged()
    {
        IsDamaged = false;
    }

    void IsAttack()
    {
        IsAttacking = false;
    }

    bool IsInRange(float range)
    {
        if (Vector3.Distance(player.transform.position, transform.position) < range)
        {
            return true;
        }
        return false;
    }

    void Animate()
    {
        if(!IsAttacking && !IsDied)
        {           
            if (navAgent.velocity.magnitude > 0.5f)
            {
                anim.CrossFade("creature1run", 0.25f);
            }
            else
            {
                anim.CrossFade("creature1Idle", 0.25f);
            }          
        }        
    }
  
    void OnTriggerEnter(Collider order)
    {
        if (order.gameObject.tag == "Player")
        {
            Debug.Log("맞음");
            IsDamaged = true;
            IsAttacking = true;
            HP = HP - Player.damage;
            transform.position += Vector3.back * Time.deltaTime * 15.0f;
            if(!IsDied)
            {                
                anim.Stop();                
                transform.LookAt(player.transform.position);
                anim.CrossFade("creature1GetHit", 0);
                Invoke("IsAttack", 0.5f);                
            }
            Invoke("Damaged", 0.1f);            
        }
        
    }        
}
