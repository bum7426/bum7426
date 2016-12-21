using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    public CharacterController CC;
    RaycastHit Hit;
    float _Speed = 10f;
    float Distancedir;
    public Animation anim;
    bool MoveOn;
    Vector3 v1, v2, Vdir;
    Quaternion dir;

    
    void Update()
    {
        Char_Move();
    }
    
    void Char_Move()
    {        
        if (Input.GetMouseButton(0))
        {
            //Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit, Mathf.Infinity, (1<<8));
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit, Mathf.Infinity, (1<<8)|(1<<9));
            
            if (!MoveOn) MoveOn = true;
        }     

        if(MoveOn)
        {
            v1 = (Hit.point - transform.position).normalized;
            Distancedir = Vector3.Distance(Hit.point, transform.position);

            if (Distancedir > 1f)
            {
                anim.CrossFade("RUN00_F", 0.25f);
                _Speed = 5f;
                CC.Move(v1 * _Speed * Time.deltaTime);
                CC.Move(new Vector3(0, -0.5f, 0));
            }
            else if (Distancedir > 0.25f)
            {
                anim.CrossFade("WALK00_F", 0.1f);
                _Speed = 2f;
                CC.Move(v1 * _Speed * Time.deltaTime);
                CC.Move(new Vector3(0, -0.5f, 0));
            }
            else
            {
                if (MoveOn) MoveOn = false;
                anim.CrossFade("WAIT00", 0.3f);                
            }                                  

            dir = Quaternion.LookRotation(v1);           
            v2.y = dir.eulerAngles.y;
            dir.eulerAngles = v2;

            transform.rotation = Quaternion.Slerp(transform.rotation, dir, _Speed * Time.deltaTime);            
        }      
    }
}
