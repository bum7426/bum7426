using UnityEngine;
using UnityEngine.EventSystems;

using System;
using System.Collections;

public class SimpleFSM : MonoBehaviour {

    //  행동 델리게이트
    Action      m_Act;

    [SerializeField]//  행동 변경 시간
    float       m_ChangeTime;

    ////////////////////////////////////////////////
    [SerializeField]//  중심점
    Vector3     m_BasePoint;
    [SerializeField]//  이동 한계 영역
    float       m_AreaLength;
    [SerializeField]//  이동 좌표
    Vector3     m_MovePoint;
    [SerializeField]//  이동 속도
    float       m_MoveSpeed;

    ////////////////////////////////////////////////
    [SerializeField]
    GameObject  m_Area = null;
    [SerializeField]
    GameObject m_UIPanel = null;

    // Use this for initialization
    void Start () {
        //  기본은 대기 상태로
        ChangeAct(Idle, m_ChangeTime);
    }
	
	// Update is called once per frame
	void Update () {
        m_Act();

        //  영역 표시
        if( m_Area != null)
            m_Area.transform.localScale = new Vector3(m_AreaLength * 2f, 0.01f, m_AreaLength * 2f);
    }

    public void    ChangeAct(Action _Act, float _NextChangeTime = 0 )
    {
        if( IsInvoking("MoveAct") )
        {
            CancelInvoke("MoveAct");
        }

        switch(_Act.Method.Name)
        {
            case "Idle":
                //  대기
                Invoke("MoveAct", _NextChangeTime);
                break;

            case "Move":
                //  위치 생성
                m_MovePoint = m_BasePoint + UnityEngine.Random.insideUnitSphere * m_AreaLength;
                //  높이 맞춰주기
                m_MovePoint.y = transform.position.y;
                //  바라보기
                transform.LookAt(m_MovePoint);
                break;

            case "Talk":
                transform.LookAt(Camera.main.transform);
                break;
        }

        m_Act = _Act;
    }

    public void    MoveAct()
    {
        ChangeAct(Move);
    }
    public void InitAct()
    {
        ChangeAct(Idle, m_ChangeTime);
    }

    void    Idle()
    {

    }

    void    Move()
    {
        //  해당 지점에 도착하면 다시 대기
        if(Vector3.Distance(m_MovePoint, transform.position) < 0.1 )
        {
            ChangeAct(Idle, m_ChangeTime);
        }

        //  해당 지점으로 이동
        transform.position += transform.forward * m_MoveSpeed * Time.deltaTime;
    }

    void    Talk()
    {
        if( Input.GetKeyDown(KeyCode.Escape) )
        {
            InitAct();
        }
    }

    private void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject() == false)
        {
            ChangeAct(Talk);

            m_UIPanel.SetActive(true);
        }
    }
}
