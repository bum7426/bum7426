using UnityEngine;
using System.Collections;

public abstract class PlayerManager : MonoBehaviour {
    public float MaxHP;
    public float HP;
    public int   STR;
    public int   DEF;
    public int   Level;
    public int   EXP;
    public static float damage;
    public float attackSpeed;

    // Use this for initialization
    void Start ()
    {
        HP = MaxHP;	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void DamageCalc()
    {
        damage = (STR - MonsterManager.DEF) * 20f;
    }

    void LevelUp()
    {
    }


}
