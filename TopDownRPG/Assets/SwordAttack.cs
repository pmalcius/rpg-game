using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 3f;

    Vector2 rightAttackOffset;
    Vector2 upAttackOffset;
    Vector2 downAttackOffset;
    Collider2D swordCollider;

    private void Start() {
        swordCollider = GetComponent<Collider2D>();
        rightAttackOffset = transform.position;
    }

    public void AttackRight() {
        print("Attack Right");
        swordCollider.enabled = true;
        transform.position = rightAttackOffset;
    }

    public void AttackLeft() {
        print("Attack Left");
        swordCollider.enabled = true;
        transform.position = new Vector2(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void AttackUp() {
        print("Attack Up");
        swordCollider.enabled = true;
        // Need to add code for the offset
        transform.position = upAttackOffset;
    }

    public void AttackDown() {
        print("Attack Down");
        swordCollider.enabled = true;
        // Need to add code for the offset
        transform.position = downAttackOffset;
    }

    public void StopAttack() {
        swordCollider.enabled = false;
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
