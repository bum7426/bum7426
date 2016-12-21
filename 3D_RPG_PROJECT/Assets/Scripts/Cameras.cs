using UnityEngine;
using System.Collections;

public class Cameras : MonoBehaviour {

    public GameObject Characters;

	// Use this for initialization
	void Start ()
    {
        transform.position = Characters.transform.position;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Characters.transform.position;
    }
}
