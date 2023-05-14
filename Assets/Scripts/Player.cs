using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSprites small;
    public PlayerSprites big;

    public DeathAnimation death { get; private set; }

    public bool Big => big.enabled;
    public bool Small => small.enabled;
    public bool DeathA => death.enabled;


    private void Awake()
    {
        death = GetComponent<DeathAnimation>();
    }

    public void Hit()
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

    private void Shrink()
    {

    }

    private void Death()
    {
        small.enabled = false;
        big.enabled = false;
        death.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }
}
