using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour
{
    NavMeshAgent navAgent;
    RaycastHit hit;
    Ray ray;    
    public Animator Anim;
    public static bool IsMove;
    public GameObject attackRange;


    void Start ()
    {
        navAgent    = GetComponent<NavMeshAgent>();        
        Anim        = GetComponentInChildren<Animator>();        
        Anim.SetInteger("WeaponState", 4);
        Anim.SetBool("Idling", true);
        IsMove = false;
        attackRange.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Move();
        Animate();
    }

    void Move()
    {
        if (Input.GetMouseButton(0))
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, (1 << 8) | (1 << 9));
            navAgent.SetDestination(hit.point);            
            Anim.SetBool("Idling", false);
            Player.IsAttacking = false;
            IsMove = true;
        }
        else
        {
            IsMove = false;
            Anim.SetBool("Idling", true);
        }
    }

    void Animate()
    {
        if(!Player.IsAttacking)
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

}
        
