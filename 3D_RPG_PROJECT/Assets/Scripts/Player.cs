using UnityEngine;
using System.Collections;

public class Player : PlayerManager
{
    public static Transform opponent;
    public static Player player;
    public static bool IsAttacking;    

    RaycastHit hit;
    
    public GameObject attackSensor;
    public Animator Anim;
    

    NavMeshAgent navAgent;

    void Awake ()
    {
        Anim = GetComponentInChildren<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        attackSensor.SetActive(false);
        player = this;
        IsAttacking = false;
        damage = 15;

    }
	
	void Update ()
    {
        Attack();
        MobChk();
        MoveChk();
    }

    protected override void Attack()
    {        
        if (Input.GetMouseButtonUp(1))
        {
            //Vector3 pos = Input.mousePosition;
            //pos.z = 10;
            //Vector3 target = Camera.main.ScreenToWorldPoint(pos);
            //transform.LookAt(target);
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);            
            
            transform.LookAt(hit.point);
            Anim.SetTrigger("Use");            
            Anim.SetBool("Idling", true);            
            IsAttacking = true;
            Debug.Log("공격");            
        }
        else
        {
            IsAttacking = false;
        }        
    }

    void MoveChk()
    {
        if(IsAttacking)
        {            
            navAgent.enabled = false;
        }
        else
        {
            navAgent.enabled = true;
        }
    }

    void MobChk()
    {
        if(IsAttacking)
        {
            attackSensor.SetActive(true);
        }
        else
        {
            attackSensor.SetActive(false);
        }
    }
}
