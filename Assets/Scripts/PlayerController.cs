using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MovementController movementController;

    public SpriteRenderer sprite;
    public Animator animator;

    void Awake()
    {
        movementController = GetComponent<MovementController>();
        movementController.lastMovingDirrection = "left";
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("moving", true);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movementController.SetDirection("left");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            movementController.SetDirection("right");
        }
        else if (Input.GetKey(KeyCode.DownArrow)){
            movementController.SetDirection("down");
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            movementController.SetDirection("up");
        }

        bool flipX = false;
        bool flipY = false;
        if (movementController.lastMovingDirrection == "left")
        {
            animator.SetInteger("direction", 0);
        }
        else if(movementController.lastMovingDirrection == "right")
        {
            animator.SetInteger("direction", 0);
            flipX = true;
        }
        else if (movementController.lastMovingDirrection == "up")
        {
            animator.SetInteger("direction", 1);
        }
        else if (movementController.lastMovingDirrection == "down")
        {
            flipY = true;
            animator.SetInteger("direction", 1);
        }

        sprite.flipX = flipX;
        sprite.flipY = flipY;
    }
}
