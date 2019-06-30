using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysObject
{
    public float max_speed = 7.0f;
    public float take_off_speed = 7.0f;
    public float jump_cancel_attenuator = .5f;
    public float horizontal_air_attenuator = .5f;

    private SpriteRenderer sprite;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
        if (!grounded)
            move.x *= horizontal_air_attenuator;

        if(Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = take_off_speed;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            if(velocity.y > 0)
            {
                velocity.y = velocity.y * jump_cancel_attenuator;
            }

          
        }
        target_velocity = move * max_speed;
    }

    protected override void HandleAnimations()
    {

        bool flip = (sprite.flipX ? (velocity.x > 0.1f) : (velocity.x < -0.1f));
        if (flip)
            sprite.flipX = !sprite.flipX;

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / max_speed);

    }
}
