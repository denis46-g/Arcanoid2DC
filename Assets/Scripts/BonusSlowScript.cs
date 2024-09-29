using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSlowScript : BonusBase
{
    protected override void BonusActivate()
    {
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var ball in balls)
        {
            var rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity *= 0.9f;
        }
    }
}
