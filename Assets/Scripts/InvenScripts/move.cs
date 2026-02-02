using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 5f;

    Rigidbody m_Rigidbody;
    Vector3 m_Movemont;


    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movemont = new Vector3(horizontal, 0, vertical).normalized;

        transform.position += m_Movemont * moveSpeed * Time.deltaTime;


    }
}
