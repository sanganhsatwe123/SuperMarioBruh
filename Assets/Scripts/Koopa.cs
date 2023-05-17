using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shell;
    public float shellSpeed = 12f;

    private bool shelled;
    private bool pushed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(player.starPower)
            {
                Hit();
            }
            else
            {
                if (collision.transform.DotTest(transform, Vector2.down))
                {
                    EnterShell();
                }
                else
                {
                    player.Hit();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(shelled && other.CompareTag("Player"))
        {
            if(!pushed)
            {
                Vector2 dir = new Vector2(transform.position.x - other.transform.position.x, 0f);
                pushedShell(dir);
            }
            else
            {
                Player player = other.GetComponent<Player>();
                if (player.starPower)
                {
                    Hit();
                }
                else
                {
                    player.Hit();
                }
            }
        }
        else
        {
            if (!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
            {
                Hit();
            }
        }
    }

    private void EnterShell()
    {
        shelled = true;

        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shell;
    }

    private void pushedShell(Vector2 dir)
    {
        pushed = true;

        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = dir.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible()
    {
        if (pushed)
        {
            Destroy(gameObject);
        }
    }
}
