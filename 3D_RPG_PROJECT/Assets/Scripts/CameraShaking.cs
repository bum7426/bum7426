using UnityEngine;
using System.Collections;

//  서브루틴 : 진입점이 한개
//  코  루틴 : 진입점이 여러개

//public class Sample
//{
//    int Count = 0;
//    int Sum = 0;

//    void Update()
//    {
//        if(Count < 100 )
//        {
//            Sum += 1;

//            Count++;
//        }
//    }

//    IEnumerator SumNum()
//    {
//        for( int i = 0; i < 100; ++i)
//        {
//            Sum += 1;

//            yield return null;
//        }
//    }
//}

public class CameraShaking : MonoBehaviour {

    [SerializeField]
    Transform   m_Target;
    [SerializeField]
    float       m_ShakingPower  = 1.0f;
    [SerializeField]
    float       m_ShakingTime   = 1;

    bool        m_IsShaking     = false;
    Vector3     m_OriginPos;

    IEnumerator Shaking()
    {
        float   Rate    = 1;
        m_OriginPos     = m_Target.position;
        m_IsShaking     = true;

        while ( Rate > 0 )
        {
            Vector3 InSideRandom = new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y, 0);
            m_Target.position = m_OriginPos + InSideRandom * m_ShakingPower * Rate;

            Rate -= Time.deltaTime / m_ShakingTime;

            yield return null;
        }

        m_IsShaking         = false;
        m_Target.position   = m_OriginPos;
    }

    public void SetTarget( string _TartgetName )
    {
        m_Target = GameObject.Find(_TartgetName).transform;
    }

    public void StartShaking()
    {
        if (m_IsShaking == true)
        {
            StopCoroutine("Shaking");

            m_Target.position = m_OriginPos;
        }

        StartCoroutine("Shaking");
    }
   
}
