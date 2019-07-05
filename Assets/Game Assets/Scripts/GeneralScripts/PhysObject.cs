using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysObject : MonoBehaviour
{
    public float gravity_magnitude = 1f;
    public float min_ground_normal_y = .65f;
    public float acceleration = 1.0f; //Units added per second;
    public float decceleration = 1.0f; //Units deccelerated per second;

    public Vector2 velocity = Vector2.zero;
   
    public bool grounded;
    protected Vector2 target_velocity = Vector2.zero;
    protected Rigidbody2D rb_2d;
    protected ContactFilter2D contact_filter;
    protected const float shell_radius = 0.01f;
    protected List<RaycastHit2D> collision_buffer_list = new List<RaycastHit2D>(16);

    void OnEnable()
    {
        rb_2d = gameObject.GetComponent<Rigidbody2D>();

    }
    // Start is called before the first frame update
    void Start()
    {
        contact_filter.useTriggers = false;
        contact_filter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer)); //This means "Just use the project's collision matrix" -Lorién
        contact_filter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        target_velocity = Vector2.zero;
        ComputeVelocity();
        HandleAnimations();
    }

    protected virtual void ComputeVelocity()
    { }

    protected virtual void HandleAnimations()
    { }

    void FixedUpdate()
    {
        //gravity
        velocity += gravity_magnitude * Physics2D.gravity * Time.fixedDeltaTime;
        grounded = false; //it is checked every frame

        AccelerateAndDeccelerate();

        Vector2 new_pos = velocity * Time.deltaTime;
        
        Movement(new Vector2(0,new_pos.y), true);

        Movement(new Vector2(new_pos.x ,0), false);
    }

    void Movement(Vector2 move, bool y_movement)
    {
        float distance = move.magnitude;
        RaycastHit2D[] collisions_buffer = new RaycastHit2D[16];
        int collision_count = rb_2d.Cast(move, contact_filter, collisions_buffer,distance + shell_radius);
        collision_buffer_list.Clear();

        for (int i = 0; i < collision_count; i++)
        {
            collision_buffer_list.Add(collisions_buffer[i]); //copy the components to a list for a better handling
        }
        
        for(int i = 0; i<collision_buffer_list.Count;i++)
        {
            Vector2 current_normal = collision_buffer_list[i].normal;
            if (current_normal.y > min_ground_normal_y)
            {
                grounded = true;
                velocity.y = 0.0f;
            }

            float modified_distance = collision_buffer_list[i].distance - shell_radius;
            distance = modified_distance < distance ? modified_distance : distance;
        }

        rb_2d.position = rb_2d.position + move.normalized * distance;
    }

    void AccelerateAndDeccelerate()
    {
        Vector2 velocity_x = new Vector2(velocity.x, 0);

        if (velocity_x.magnitude < target_velocity.magnitude && target_velocity.magnitude > 0)
        {
            velocity.x += target_velocity.normalized.x * acceleration * Time.fixedDeltaTime;
        }
        else if (velocity_x.magnitude > target_velocity.magnitude && target_velocity.magnitude > 0)
        {
            velocity.x = target_velocity.x;
        }
        else if (target_velocity.magnitude == 0 && velocity_x.magnitude > 0)
        {
            velocity.x -=  velocity.normalized.x * decceleration * Time.fixedDeltaTime;
        }
    }


    
}
