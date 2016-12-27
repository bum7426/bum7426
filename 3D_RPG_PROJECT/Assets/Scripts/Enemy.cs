using UnityEngine;
using System.Collections;

public class Enemy : CharManager
{
    Player player;
    NavMeshAgent navAgent;

    public float chasingRange;
    public int damage;
    Vector3 OriginPos;
    Vector3 stopRange = new Vector3(1, 0, 1);
    bool block;

	void Start ()
    {
        player = Player.player;
        OriginPos = transform.position;
        navAgent = GetComponent<NavMeshAgent>();        
    }
	
	// Update is called once per frame
	void Update ()
    {
        Attack();
        FollowPlayer();
    }

    void OnMouseEnter()
    {
        Player.opponent = transform;
        Debug.Log("데헷");
    }

    void FollowPlayer()
    {
        if(IsInRange(chasingRange))
        {
            navAgent.SetDestination(player.transform.position + stopRange);
            transform.LookAt(player.transform.position);
        }
        else
        {
            navAgent.SetDestination(OriginPos);
        }        
    }

    protected override void Attack()
    {
        if (IsInRange(range))
        {
            if(!block)
            {
                player.GetHit(damage);
                block = true;
                Invoke("UnBlock", attackSpeed);                
            }            
        }
    }

    void UnBlock()
    {
        block = false;
    }

    bool IsInRange(float range)
    {
        if (Vector3.Distance(player.transform.position, transform.position) < range)
        {
            return true;
        }
        return false;
    }

    void OnTriggerEnter(Collider order)
    {
        if (order.gameObject.tag == "Player")
        {
            Debug.Log("맞음");
            if (health > 0)
                health = health - Player.damage;
            //else
            //    Destroy(gameObject,0.5f);
        }
    }


}
