using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {


    public float    _speed      = 100.0f;
    //private float   _halfHeight;
	// Use this for initialization
	void Start ()
    {
        //_halfHeight = Screen.height * 0.5f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        PlayerMove();      
    }

    void PlayerMove()
    {
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            transform.Translate(0, _speed * Time.deltaTime * 0.01f, 0);
            transform.localPosition = new Vector3
            (transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, -270.0f, 250.0f), transform.localPosition.z);
        }

        if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            transform.Translate(0, _speed * Time.deltaTime * 0.01f * (-1), 0);
            transform.localPosition = new Vector3
            (transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, -270.0f, 250.0f), transform.localPosition.z);
        }

        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            transform.Translate(_speed * Time.deltaTime * 0.01f, 0, 0);
            transform.localPosition = new Vector3
            (Mathf.Clamp(transform.localPosition.x, -450.0f, 500.0f), transform.localPosition.y, transform.localPosition.z);
        }

        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            transform.Translate(_speed * Time.deltaTime * 0.01f * (-1), 0, 0);
            transform.localPosition = new Vector3
            (Mathf.Clamp(transform.localPosition.x, -450.0f, 500.0f), transform.localPosition.y, transform.localPosition.z);
        }
    }//이동


    
    /////////////////////////////////////////
    
    public int _hp = 100;
    public int _hpDam = 1;
    public Animator Anim;
    public UISprite GuageBarWidget;
    public UISprite _Face;

    void OnTriggerEnter2D(Collider2D other)
    {
        _hp -= _hpDam;

        GuageBarWidget.fillAmount = _hp * 0.01f;

        if (_hp <= 0)
        {
            // GM 오브젝트의 GameOver 메서드를 실행하라.
            GameObject.Find("GM").SendMessage("GameOver", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            _Face.spriteName = "Player_5_FaceAngry";
            Anim.SetTime(0);
            Anim.SetBool("damageChk", true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        _Face.spriteName = "Player_5_Face";
    }


    void DamageEnd()
    {
        Anim.SetBool("damageChk", false);
    }


}
