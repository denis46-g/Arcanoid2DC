using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFastScript : BonusBase
{
    protected override void BonusActivate()
    {
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var ball in balls)
        {
            var rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity *= 1.1f;
        }
    }
}
