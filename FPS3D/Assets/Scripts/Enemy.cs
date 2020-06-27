using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health = 100;

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Bullet") {
            TakeDamage();
            Destroy(coll.gameObject);
        }
    }

    void TakeDamage() {
        health -= 20;

        if (health <= 0)
        {
            GameManager.instance.score++;
            Destroy(gameObject);
        }

    }

}
