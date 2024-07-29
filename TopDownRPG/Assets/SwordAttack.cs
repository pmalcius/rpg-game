using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 3f;

    Vector2 rightAttackOffset;
    //Vector2 upAttackOffset;
    //Vector2 downAttackOffset;
    Collider2D swordCollider;

    private void Start() {
        swordCollider = GetComponent<Collider2D>();
        rightAttackOffset = transform.position;
        //upAttackOffset = transform.position;
        //downAttackOffset = transform.position;
    }

    public void Attack(float attackDirectionX, float attackDirectionY) {
        swordCollider.enabled = true;

        //print("Sword Attack Triggered");

        if (attackDirectionY > 0) {
            // up attack
            print("Sword Attack up Triggered");
            transform.position = rightAttackOffset;
        } else if (attackDirectionY < 0) {
            // down attack
            print("Sword Attack down Triggered");
            transform.position = rightAttackOffset;
        } else if (attackDirectionX != 0) {
            // right or left attack
            if (attackDirectionX > 0) {
                //right attack
                print("Sword Attack right Triggered");
                transform.position = rightAttackOffset;
            } else {
                //left attack
                print("Sword Attack left Triggered");
                transform.position = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
            }
        }

        //swordCollider.enabled = false;
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
