using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;    // Speed at which the player moves
    public float collisionOffset = 1f;  // Offset to consider before registering a collision

    // Filter to determine which layers the player can collide with
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;


    Vector2 movementInput;  // Stores the player's movement input
    Rigidbody2D rb;         // Reference to the Rigidbody2D component for physics-based movement
    Animator animator;
    SpriteRenderer spriteRenderer;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();   // List to store collision information during movement checks


    bool canMove = true;
    Vector2 lastMovementDirection;  // Stores the last movement direction to determine attack direction

    // Start is called before the first frame update
    void Start() {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ensure swordAttack is correctly referenced
        swordAttack = GetComponentInChildren<SwordAttack>();
    }

    // Called at a fixed interval, used for physics calculations
    private void FixedUpdate() {
        if (canMove) {
            // If movement input is not 0, try to move
            if (movementInput != Vector2.zero) {
                bool success = TryMove(movementInput);

                // if movement is not successfull try moving horizontally
                if (!success) {
                    success = TryMove(new Vector2(movementInput.x, 0));

                    // if movement is not successfull horizontally try verticly
                    if (!success) {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }

                // Set animator parameters based on movement direction
                animator.SetFloat("Speed", moveSpeed);
                animator.SetFloat("Horizontal", movementInput.x);
                animator.SetFloat("Vertical", movementInput.y);

                // Store the last movement direction
                lastMovementDirection = movementInput;

                // Set direction of sprite movement for right and left.
                if (movementInput.x < 0) {  // Moving left
                    spriteRenderer.flipX = true;
                } else if (movementInput.x > 0) {   // Moving right
                    spriteRenderer.flipX = false;
                }
            } else {
                animator.SetFloat("Speed", 0);
            }
        }
    }

    // Attempts to move the player in the given direction
    private bool TryMove(Vector2 direction) {
        if(direction != Vector2.zero) {
            // Casts a line (ray) in the given direction to check for collisions
            int count = rb.Cast(
                    direction,                                          // Direction to move
                    movementFilter,                                     // Filter to specify which layers to consider
                    castCollisions,                                     // List to store results
                    moveSpeed * Time.fixedDeltaTime * collisionOffset   // Distance to move
            );

            // If no collisions detected, move the player
            if (count == 0) {
                // Debug.Log("No collision, moving...");
                rb.MovePosition(rb.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {
                // Debug.Log("Collision detected, stopping movement.");
                return false;
            }
        } else {
            // Wont move if there is no direction to move in
            return false;
        }
        
    }

    // Called when there is input from the player to move
    void OnMove(InputValue movementValue){
        // Store the movement input as a Vector2
        movementInput = movementValue.Get<Vector2>();
    }

    // When left clicked the attack input is fired
    void OnFire(){
        TriggerAttackAnimation();
    }

    private void TriggerAttackAnimation() {
        lockMovement();

        float attackDirectionX = 0;
        float attackDirectionY = 0;

        if (lastMovementDirection.y > 0) {
            attackDirectionY = 1; // Upward attack
        } else if (lastMovementDirection.y < 0) {
            attackDirectionY = -1; // Downward attack
        } else if (lastMovementDirection.x != 0) {
            attackDirectionX = lastMovementDirection.x; // Right or left attack
        }

        animator.SetFloat("attackDirectionX", attackDirectionX);
        animator.SetFloat("attackDirectionY", attackDirectionY);
        animator.SetTrigger("swordAttack");

        spriteRenderer.flipX = attackDirectionX < 0;

        swordAttack.Attack(attackDirectionX, attackDirectionY);
        swordAttack.StopAttack();
    }

    public void SwordAttack() {
        if (swordAttack != null) {
            swordAttack.Attack(lastMovementDirection.x, lastMovementDirection.y);
        }
    }

    public void lockMovement() {
        canMove = false;
    }

    public void unlockMovement() {
        canMove = true;
    }
}
