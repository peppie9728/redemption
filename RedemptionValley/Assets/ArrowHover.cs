using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHover : MonoBehaviour
{
    private bool movingUp = true; // A flag to control movement direction

    void Update()
    {
        // Move up when below y = 9
        if (movingUp && transform.position.y < 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime, transform.position.z);
        }
        // Move down when y reaches 9 or more
        else if (!movingUp && transform.position.y > 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime, transform.position.z);
        }

        // Check if we need to switch direction
        if (transform.position.y >= 2)
        {
            movingUp = false; // Start moving down
        }
        else if (transform.position.y <= 1)
        {
            movingUp = true; // Start moving up
        }
    }
}
