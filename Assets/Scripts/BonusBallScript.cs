using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBallScript : BonusBase
{
    protected override void BonusActivate()
    {
        var playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        playerScript.gameData.balls++;
    }
}
