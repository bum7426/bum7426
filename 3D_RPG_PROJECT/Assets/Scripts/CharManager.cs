using UnityEngine;
using System.Collections;

public abstract class CharManager : MonoBehaviour
{
    public int health;
    
    public float range;

    // 초당 공격 횟수
    public float attackSpeed;

    public void GetHit(int playerDamage)
    {
        health = health - playerDamage;
        if (health <= 0)
        {
            Debug.Log("die");
        }
    }
    protected abstract void Attack();
}
