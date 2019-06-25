using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automove : PhysObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target_velocity = Vector2.left;
    }
}
