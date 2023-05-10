using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprites : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement movement;

    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public AnimatedSprite run;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerMovement>();
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void LateUpdate()
    {
        run.enabled = movement.running;

        if (movement.Jumping) 
        {
            spriteRenderer.sprite = jump;
        }
        else
        {
            if (movement.sliding) 
            {
                spriteRenderer.sprite = slide;
            }
            else
            {
                if (!movement.running)
                {
                    spriteRenderer.sprite = idle;
                }
            }
        }
    }
}
