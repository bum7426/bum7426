using UnityEngine;
using System.Collections;

public class Player : CharManager
{
    public static Transform opponent;
    public static Player player;
    RaycastHit hit;

    public Vector3 attackPos;

    public GameObject attackRange;
    
	void Awake ()
    {
        attackRange.SetActive(false);
        player = this;
	}
	
	void Update ()
    {
        Attack();
	}

    protected override void Attack()
    {
        if (opponent != null && Vector3.Distance(opponent.transform.position, transform.position)<range)
        {            
            if (Input.GetMouseButton(1))
            {
                Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
                attackRange.SetActive(true);
                attackPos = hit.point;// establish the point that we hit with the mouse
                attackPos.y = transform.position.y;//use our height for the LOOKAT function, so we stay level and dont lean the character in weird angles
                Vector3 attackDelta = attackPos - transform.position;//we need the Vector delta which is an un-normalized direction vector
                attackPos = transform.position + attackDelta.normalized * 20.0f;
                transform.LookAt(attackPos);
                opponent.GetComponent<Enemy>().GetHit(damage);
            }
            else if(Input.GetMouseButtonUp(1))
            {
                attackRange.SetActive(false);
            }                
        }        
    }

}
