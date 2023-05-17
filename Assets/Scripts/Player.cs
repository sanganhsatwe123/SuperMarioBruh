using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSprites small;
    public PlayerSprites big;
    private PlayerSprites active;
    public DeathAnimation death { get; private set; }
    public CapsuleCollider2D capsuleCollider;

    public bool Big => big.enabled;
    public bool Small => small.enabled;
    public bool DeathA => death.enabled;
    public bool starPower { get; private set; }


    private void Awake()
    {
        death = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        active = small;
    }

    public void Hit()
    {
        if(!DeathA && !starPower)
        {
            if (Big)
            {
                Shrink();
            }
            else
            {
                Death();
            }
        }
    }

    private void Death()
    {
        small.enabled = false;
        big.enabled = false;
        death.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }

    public void Grow()
    {
        small.enabled = false;
        big.enabled = true;
        active = big;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimate());
    }

    private void Shrink()
    {
        small.enabled = true;
        big.enabled = false;
        active = small;

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimate());
    }

    private IEnumerator ScaleAnimate()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if(Time.frameCount % 4 == 0)
            {
                small.enabled = !small.enabled;
                big.enabled = !small.enabled;
            }

            yield return null;
        }

        small.enabled = false;
        big.enabled = false;

        active.enabled = true;
    }

    public void StarPower(float duration = 10f)
    {
        StartCoroutine(StarAnimation(duration));
    }

    private IEnumerator StarAnimation(float duration)
    {
        starPower = true;

        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if(Time.frameCount % 4 == 0)
            {
                active.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }

        active.spriteRenderer.color = Color.white;
        starPower = false;
    }
}
