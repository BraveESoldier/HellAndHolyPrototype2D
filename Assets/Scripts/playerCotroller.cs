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

    private bool isGrounded; //Находится на земле?
    public Transform feetPos; //Позиция ног игрока
    public float checkRadius; //Насколько близко игрок к земле
    public LayerMask whatIsGround; //Слой от которого отталкивается

    public Joystick joystick;

    private Animator anim; //Указываю аниматор игрока
    

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

        if( isGrounded == true && verticalMove >= 0.5f) //Проверка на нахождения на земле и нажатие пробела
        {
            rb.velocity = Vector2.up * jumpForse; //Дает скорость на rb по y
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
        Vector3 Scaler = transform.localScale; //берем оригиналное положение
        Scaler.x *= -1;
        transform.localScale = Scaler;//приминяет изменения

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
