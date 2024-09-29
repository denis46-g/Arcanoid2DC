using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTwoScript : BonusBase
{
    public GameObject ballPrefab;
    protected override void BonusActivate()
    {
        var playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        playerScript.gameData.balls += 2;
        for (int i = 0; i < 2; i++)
        {
            var ball = Instantiate(ballPrefab).GetComponent<BallScript>();
            ball.ballInitialForce += new Vector2(Random.Range(0, 5), Random.Range(0, 5));
            ball.ballInitialForce *= 1 + playerScript.level * playerScript.ballVelocityMult;
            var pos = ball.transform.position;
            pos.x = Random.Range(10f, Screen.width - 10);
            pos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, pos.z));
            pos.z = 0;
            ball.transform.position = pos;
            var rb = ball.GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            rb.AddForce(ball.ballInitialForce);
        }
    }
}
