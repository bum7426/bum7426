using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GM : MonoBehaviour {

    public GameObject   _enemySet;
    public GameObject   _nearBgObj;
    public Transform    _PlayerObjPool;
    public bool         _SpawnChk = true;
    public UILabel      _ScoreText;

    // Use this for initialization
    void Start ()
    {
        Invoke("LevelUp", _TimerForLevelLim);
    }
	
	// Update is called once per frame
	void Update ()
    {
        _ScoreText.text = (Time.timeSinceLevelLoad * 100.0f).ToString("N0");
        if (_nearBgObj.transform.localPosition.x < -2460.0f && _SpawnChk)
        {
            GameObject Set1 = Instantiate(_enemySet);
            //프리팹을 실제 오브젝트로 생성
            Set1.transform.parent = _PlayerObjPool;
            Set1.transform.localScale = Vector3.one;
            Set1.transform.localPosition = Vector3.zero;
            _SpawnChk = false;
        }

        if (_nearBgObj.transform.localPosition.x > -1300.0f && _SpawnChk == false)
        {
            _SpawnChk = true;
        }
    }


    public GameObject _ResultUI;
    public UILabel _ResultText;
    void GameOver()
    {
        Time.timeScale = 0.0f;
        _ResultUI.SetActive(true);
        _ResultText.text = "Your Score is \n" + _ScoreText.text;
    }

    void ReGame()
    {
        SceneManager.LoadScene("1_play");
        Time.timeScale = 1.0f;
        _ResultUI.SetActive(false);
    }

    public float _TimerForLevel = 0.0f;
    public float _TimerForLevelLim = 10.0f;

    void LevelUp()
    {
        Time.timeScale *= 1.2f;

        if (Time.timeScale >= 3)
        {
            Time.timeScale = 3;
        }
        else
        {
            //GameObject.Find("Player").SendMessage("DamageUp", SendMessageOptions.DontRequireReceiver);
            Invoke("LevelUp", _TimerForLevelLim * Time.timeScale);
            print(Time.timeScale);
        }
    }
}
