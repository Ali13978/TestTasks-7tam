using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.TryGetComponent<IShootable>(out IShootable i))
        {
            i.ApplyHit();
            Destroy(gameObject);
        }
    }
}
