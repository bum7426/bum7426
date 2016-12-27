using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterChk : MonoBehaviour
{
    List<GameObject> Mob = new List<GameObject>();

    void OnTriggerEnter(Collider order)
    {
        if(order.gameObject.tag == "Monster")
        {
            Mob.Add(order.gameObject);
            Debug.Log("충돌됨");
        }
    }

    void OnTriggerExit(Collider order)
    {
        if(order.gameObject.tag == "Monster")
        {
            Mob.Remove(order.gameObject);
            Debug.Log("나감");
        }
    }
}
