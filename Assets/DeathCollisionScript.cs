using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollisionScript : MonoBehaviour
{

    public bool hasLost = false;

    private void OnTriggerEnter2D(Collider2D ground)
    { //if the trigger colliders (on the skateboard and the penguin) hit the ground or an obstacle, they toggle the lose bool
        if (ground.CompareTag("Ground"))
        {
            hasLost = true;
        }
    }
}
