using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody m_Rigidbody = null;
    
    [SerializeField][Range(0.1f, 20.0f)]
    private float m_MovementSpeed = 4.5f;
    [SerializeField][Range(1.0f, 2.0f)]
    private float sprintMultiplier = 1.65f;
    [SerializeField]
    private float jumpForce = 2.5f;
    [SerializeField]
    private bool isRunning = false;
    [SerializeField]
    private bool isGrounded = true;

    public float MovementSpeed
    {
        get
        {
            return m_MovementSpeed;
        }    

        set
        {
            m_MovementSpeed = value;
        }
    }

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Movement(horizontal, vertical);        

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded)
            {
                m_Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))        
            isRunning = true;        
        else
            isRunning = false;
    }

    void Movement(float xAxis, float zAxis)
    {
        Vector3 fowardMovement = transform.forward * zAxis;
        Vector3 horizontalMovement = transform.right * xAxis;

        Vector3 movement = Vector3.ClampMagnitude(fowardMovement + horizontalMovement, 1.0f);

        float sprint = 1;

        if (isRunning && zAxis > 0)
            sprint = sprintMultiplier;

        m_Rigidbody.MovePosition(m_Rigidbody.position + movement * Time.deltaTime * m_MovementSpeed * sprint);        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Walkable")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Walkable")
        {
            isGrounded = false;
        }
    }
}
