using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour
{
    NavMeshAgent navAgent;
    RaycastHit hit;
    Ray ray;
    public GameObject attackRange;


    void Start ()
    {
        navAgent = GetComponent<NavMeshAgent>();
        attackRange.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
	}

    void Move()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, (1<<8)))
            {
                navAgent.SetDestination(hit.point);
                //attackRange.SetActive(false);
            }
        }
    }
}
