using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpenWeb : MonoBehaviour {

    [SerializeField]
    InputField m_URL;

    WebViewObject m_Web;
	// Use this for initialization
	void Start ()
    {
        //웹뷰 오브젝트 얻기
        m_Web = gameObject.GetComponent<WebViewObject>();

        //없을 경우 추가하기
        if(m_Web == null)
        {
            m_Web = gameObject.AddComponent<WebViewObject>();
        }

        m_Web.Init(RecvURL);
        m_Web.SetMargins(50, 150, 50, 50);
        m_Web.LoadURL(m_URL.text); //http:// 필수
        m_Web.SetVisibility(true);        
	}

    void RecvURL(string _msg)
    {
        m_URL.text = _msg;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(m_URL.isFocused == false)
        {
            m_Web.EvaluateJS("Unity.call(location.href);");
        }
        
	}

    public void PageBack()
    {
        if(m_Web.CanGoBack())
        {
            m_Web.GoBack();
        }
    }

    public void PageFoward()
    {
        if(m_Web.CanGoForward())
        {
            m_Web.GoForward();
        }
    }

    public void NewLoad()
    {
        if(m_URL.wasCanceled == false)
        m_Web.LoadURL(m_URL.text);
    }

}
