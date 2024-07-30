using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    Collider2D swordCollider;
    public float damage = 3f;

    Vector2 rightAttackOffset;

    private void Start() {
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = false;                  // Makes sure the sword collider is disabled
        rightAttackOffset = transform.position;
    }

    public void Attack(float attackDirectionX, float attackDirectionY) {
        //print("AttackDirectionX: " + attackDirectionX);
        //print("AttackDirectionY: " + attackDirectionY);


        swordCollider.enabled = true;       // Enables the sword Collider for the attack
        //Debug.Log("Sword hitbox activated");

        if (attackDirectionY > 0) {
            // up attack
            transform.localPosition = new Vector3(rightAttackOffset.y * -(1/2), rightAttackOffset.x * -(1/2));     // Place holder for now *NEED TO FIX*
            transform.Rotate(0, 0, 90);
            swordCollider.enabled = true;
            transform.Rotate(0, 0, 270);
            //print("Sword Attack up Triggered");
            
        } else if (attackDirectionY < 0) {
            // down attack
            transform.localPosition = rightAttackOffset;     // Place holder for now *NEED TO FIX*
            transform.Rotate(0, 0, 90);
            swordCollider.enabled = true;
            transform.Rotate(0, 0, 270);
            //print("Sword Attack down Triggered");

        } else if (attackDirectionY == 0) {
            // right or left attack
            if (attackDirectionX > 0) {
                //right attack
                transform.localPosition = rightAttackOffset;
                transform.Rotate(0, 0, 0);
                swordCollider.enabled = true;
                //print("Sword Attack right Triggered");
                
            } else {
                //left attack
                transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
                transform.Rotate(0, 0, 0);
                swordCollider.enabled = true;
                //print("Sword Attack left Triggered");
                
            }
        }

        // Disable the collider after a short delay
        StartCoroutine(DisableColliderAfterDelay());

    }

    private IEnumerator DisableColliderAfterDelay() {
        yield return new WaitForSeconds(0.1f); // Adjust delay to match attack animation
        swordCollider.enabled = false;
        //Debug.Log("Sword hitbox deactivated");
    }

    // Unneeded as well since DisableColliderAfterDelay disables the sword
    public void StopAttack() {
        swordCollider.enabled = false;
        //Debug.Log("Sword hitbox deactivated");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy") {
            // Try to deal damage
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null) {
                print("Health: " + enemy.Health);
                enemy.Health -= damage;
            }
        }
    }
}
