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
        { // When the "Jump" key is pressed (configurable), the jumpFlag is triggered
            jumpFlag = true;
        }
    }

    // FixedUpdate is called as several times per frame
    private void FixedUpdate()
    {
        if (jumpFlag == true)
        { //The reason I use a flag is so that I can run the Jump function in FixedUpdate
            Jump(jumpForce);
            jumpFlag = false;
        }
    }

    private void Jump(float force)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f,force-gameObject.GetComponent<Rigidbody2D>().velocity.y,0f),ForceMode2D.Impulse);
    }
}
