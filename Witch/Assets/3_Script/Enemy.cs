using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float _speed;
    public float LifeTime;
    public GameObject[] _EnemySetObj;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < _EnemySetObj.Length; i++)
        {
            Vector3 vAdd = Vector3.zero;
            vAdd.y = Random.Range(-2, 3) * 130.0f;
            _EnemySetObj[i].transform.localPosition += vAdd;
        }
        Invoke("Death", LifeTime);
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime, 0, 0);
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
