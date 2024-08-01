using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    public float damage = 3f;

    Vector2 rightAttackOffset;
    public Quaternion startQuaternion;


    private void Start() {
        swordCollider.enabled = false;                  // Makes sure the sword collider is disabled
        rightAttackOffset = transform.position;
        startQuaternion = transform.rotation;
    }

    public void Attack(float attackDirectionX, float attackDirectionY) {
        if (attackDirectionY > 0) {
            // up attack
            transform.localPosition = new Vector3(rightAttackOffset.y * -(1/2), rightAttackOffset.x * -(1/2));
            transform.Rotate(0, 0, 90);
            swordCollider.enabled = true;

            // Disable the collider after a short delay
            //StartCoroutine(DisableColliderAfterDelay());

            // Rotates back to (0, 0, 0)
            //transform.Rotate(0, 0, 270);            
        } else if (attackDirectionY < 0) {
            // down attack
            transform.localPosition = new Vector3(rightAttackOffset.y * -(3/4), rightAttackOffset.x * -(2));
            transform.Rotate(0, 0, 90);
            swordCollider.enabled = true;

            // Disable the collider after a short delay
            //StartCoroutine(DisableColliderAfterDelay());

            // Rotates back to (0, 0, 0)
            //transform.Rotate(0, 0, 270);
        } else if (attackDirectionY == 0) {
            // right or left attack
            if (attackDirectionX > 0) {
                //right attack
                transform.localPosition = rightAttackOffset;
                swordCollider.enabled = true;

                // Disable the collider after a short delay
                //StartCoroutine(DisableColliderAfterDelay());
            } else {
                //left attack
                transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
                swordCollider.enabled = true;

                // Disable the collider after a short delay
                //StartCoroutine(DisableColliderAfterDelay());
            }
        }
    }

    // Make a startAttack


    // Instead of the timmer, I will use a endAttack Animation after the attack is done.


    private IEnumerator DisableColliderAfterDelay() {
        yield return new WaitForSeconds(0.1f); // Adjust delay to match attack animation
        swordCollider.enabled = false;
        //Debug.Log("Sword hitbox deactivated");
    }

    // Unneeded as well since DisableColliderAfterDelay disables the sword
    public void StopAttack() {
        swordCollider.enabled = false;
        transform.rotation = startQuaternion;
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
