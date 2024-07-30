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
        swordCollider.enabled = true;       // Enables the sword Collider for the attack
        Debug.Log("Sword hitbox activated");

        if (attackDirectionY > 0) {
            // up attack
            swordCollider.enabled = true;
            print("Sword Attack up Triggered");
            transform.position = rightAttackOffset;     // Place holder for now *NEED TO FIX*
        } else if (attackDirectionY < 0) {
            // down attack
            swordCollider.enabled = true;
            print("Sword Attack down Triggered");
            transform.position = rightAttackOffset;     // Place holder for now *NEED TO FIX*
        } else if (attackDirectionX != 0) {
            // right or left attack
            if (attackDirectionX > 0) {
                //right attack
                swordCollider.enabled = true;
                print("Sword Attack right Triggered");
                transform.position = rightAttackOffset;
            } else {
                //left attack
                swordCollider.enabled = true;
                print("Sword Attack left Triggered");
                transform.position = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
            }
        }

        // Disable the collider after a short delay
        StartCoroutine(DisableColliderAfterDelay());

    }

    private IEnumerator DisableColliderAfterDelay() {
        yield return new WaitForSeconds(0.1f); // Adjust delay to match attack animation
        swordCollider.enabled = false;
        Debug.Log("Sword hitbox deactivated");
    }

    // Unneeded as well since DisableColliderAfterDelay disables the sword
    public void StopAttack() {
        swordCollider.enabled = false;
        Debug.Log("Sword hitbox deactivated");
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
