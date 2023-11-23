using System.Collections;
using System.Collections.Generic;
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

    #region MEUS METODOS

    void MoverPersonagem()
    {
        if (newInput.x != 0)
        {
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }
        
        m_Rigbody.velocity = new Vector3(0f, m_Rigbody.velocity.y, newInput.x * movementSpeed);
    }

    void Flip()
    {
        
    }

    void UpdateAnimator()
    {
        m_Animator.SetBool("isWalk", isWalk);
    }

    void Pular(bool isPressed)
    {
        m_Rigbody.AddForce(Vector3.up * jumpForce);
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
