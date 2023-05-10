using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static LayerMask layerMask = LayerMask.GetMask("Default");
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 dir)
    {
        if (rigidbody.isKinematic)
        {
            return false;
        }

        float radius = 0.25f;
        float distance = 0.375f;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, dir.normalized, distance, layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody;
    }

    public static bool DotTest(this Transform transform, Transform other ,Vector2 testdir)
    {
        Vector2 dir = other.position - transform.position;
        return Vector2.Dot(dir.normalized, testdir) > 0.25f;
    }
}
