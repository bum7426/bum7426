using UnityEngine;
using System.Collections;

public abstract class MonsterManager : MonoBehaviour {

    public float chasingRange;
    public float AttackRange;

    public static float damage;
    
    public float HP;    
    public static int STR;
    public static int DEF;

    public float attackSpeed;   
}
