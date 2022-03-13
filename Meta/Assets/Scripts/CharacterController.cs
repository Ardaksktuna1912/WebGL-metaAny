using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    float horizontal = 0, vertical = 0;
    Animator anim;
    Rigidbody fizik;
    public GameObject KafaKamerasi;
    float UstAltRot = 0, SagSol = 0;
    Vector3 Mesafe;
    RaycastHit Rhit;
    float hiz = 3;

    void Start()
    {
        anim = GetComponent<Animator>();
        fizik = GetComponent<Rigidbody>();
        Mesafe = KafaKamerasi.transform.position - transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            anim.SetBool("JumpParam",true);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            hiz *= 2;
            anim.SetBool("RunParam",true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            hiz /= 2;
            anim.SetBool("RunParam",false);
        }
        

        
    }
    void FixedUpdate()
    {
        Move();
        Rotation();
       

        anim.SetFloat("HorizontalParam", horizontal);
        anim.SetFloat("VerticalParam", vertical);

    }
    public void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 vec = new Vector3(horizontal, 0, vertical);

        vec = transform.TransformDirection(vec);
        vec.Normalize();
        fizik.position += vec * Time.deltaTime * hiz;
    }
    public void Rotation()
    {
        KafaKamerasi.transform.position = transform.position + Mesafe;
        UstAltRot += Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * -150;
        SagSol += Input.GetAxis("Mouse X") * Time.fixedDeltaTime * 150;
        UstAltRot = Mathf.Clamp(UstAltRot, -20, 20);
        KafaKamerasi.transform.rotation = Quaternion.Euler(UstAltRot, SagSol, transform.eulerAngles.z);

        if (horizontal != 0 || vertical != 0)
        {
            Physics.Raycast(Vector3.zero, KafaKamerasi.transform.GetChild(0).forward, out Rhit);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(Rhit.point.x, 0, Rhit.point.z)), 0.5f);
            Debug.DrawLine(Vector3.zero, Rhit.point);
        }
    }

    public void JumpFalse()
    {
       
        anim.SetBool("JumpParam", false);
    }
    public void JumpAddForce()
    {
        fizik.AddForce(0, Time.fixedDeltaTime * 10000, 0);

    }
}
