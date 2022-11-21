using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float jumpForce = 5f;
    public bool jumpFlag = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpFlag = true;
        }
    }

    // FixedUpdate is called as several times per frame
    private void FixedUpdate()
    {
        if (jumpFlag == true)
        {
            Jump(jumpForce);
            jumpFlag = false;
        }
    }

    private void Jump(float force)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f,force-gameObject.GetComponent<Rigidbody2D>().velocity.y,0f),ForceMode2D.Impulse);
    }
}
