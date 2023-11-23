using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody m_Rigbody;
    private Animator m_Animator;
    private Vector2 newInput;

    public float movementSpeed;
    public float jumpForce;
    private bool isLookLeft;

    private bool isWalk;
    private bool isGround;
    public LayerMask whatIsGround;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Rigbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoverPersonagem();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        isGround = Physics.CheckSphere(transform.position, 0.2f, whatIsGround);
    }

    #region MEUS METODOS

    void MoverPersonagem()
    {

        if (newInput.x > 0 && isLookLeft == true)
        {
            Flip();
        }
        else if (newInput.x < 0 && isLookLeft == false)
        {
            Flip();
        }
        /*
        if (newInput.x != 0)
        {
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }
        */
        isWalk = newInput.x != 0 ? true : false; //IGUAL OS IF ELSE SO QUE MEOR E ORGANIZADO
        
        
        m_Rigbody.velocity = new Vector3(0f, m_Rigbody.velocity.y, newInput.x * movementSpeed);
    }

    void Flip()
    {
        isLookLeft = !isLookLeft;
        switch(isLookLeft)
        {
            case true:
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                break;
            
            case false:
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                break;
        }
    }

    void UpdateAnimator()
    {
        m_Animator.SetBool("isWalk", isWalk);
        m_Animator.SetBool("isGround", isGround);
    }

    void Pular(bool isPressed)
    {
        if (isPressed = true && isGround == true)
        {
             m_Rigbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
       
    }

    void Atacar()
    {
        
    }

    #endregion
    #region NEW INPUT SYSTEM

    public void OnMovimentar(InputAction.CallbackContext value)
    {
        newInput = value.ReadValue<Vector2>();
    }

    public void OnAtacar(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Atacar();
        }
    }
    
    public void OnPular(InputAction.CallbackContext value)
    {
        if (value.started) //AO APERTAR O BOTÃO
        {
            Pular(true);
        }

        if (value.canceled) // AO SOLTAR O BOTÃO PULO
        {
            Pular(false);
        }
    }
    
    #endregion
}
