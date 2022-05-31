using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCotroller : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed = 4;
    public float jumpForse = 6;
    private float moveInput;

    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded; //��������� �� �����?
    public Transform feetPos; //������� ��� ������
    public float checkRadius; //��������� ������ ����� � �����
    public LayerMask whatIsGround; //���� �� �������� �������������

    public Joystick joystick;

    private Animator anim; //�������� �������� ������
    

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveInput = joystick.Horizontal;
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput <0)
        {
            Flip();
        }
        if (moveInput == 0)
        {
            anim.SetBool("is_runing", false);
        }
        else
        {
            anim.SetBool("is_runing", true);
        }
    }

    private void Update()
    {
        float verticalMove = joystick.Vertical;

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if( isGrounded == true && verticalMove >= 0.5f) //�������� �� ���������� �� ����� � ������� �������
        {
            rb.velocity = Vector2.up * jumpForse; //���� �������� �� rb �� y
            anim.SetTrigger("takeOff");
        }

        if (isGrounded == true)
        {
            anim.SetBool("is_jumping", false);
        }
        else
        {
            anim.SetBool("is_jumping", true);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale; //����� ����������� ���������
        Scaler.x *= -1;
        transform.localScale = Scaler;//��������� ���������

        if(moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if ( moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
