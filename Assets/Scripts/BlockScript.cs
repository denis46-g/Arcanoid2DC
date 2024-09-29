using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockScript : MonoBehaviour
{
    PlayerScript playerScript;
    public GameObject textObject;
    Text textComponent;
    public int hitsToDestroy;
    public int points;

    public GameObject bonus;
    public GameObject bonusSlow;
    public GameObject bonusFast;
    public GameObject bonusBall;
    public GameObject bonusTwo;
    public GameObject bonusTen;
    List<GameObject> bonuses = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        if (textObject != null)
        {
            textComponent = textObject.GetComponent<Text>();
            textComponent.text = hitsToDestroy.ToString();
        }

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        if (bonus != null)
            for (int i = 0; i < GameDataScript.bf; i++)
                bonuses.Add(bonus);
        if (bonusSlow != null) 
            for (int i = 0; i < GameDataScript.bsf; i++)
                bonuses.Add(bonusSlow);
        if (bonusFast != null)
            for (int i = 0; i < GameDataScript.bff; i++)
                bonuses.Add(bonusFast);
        if (bonusBall != null)
            for (int i = 0; i < GameDataScript.bbf; i++)
                bonuses.Add(bonusBall);
        if (bonusTwo != null)
            for (int i = 0; i < GameDataScript.b2f; i++)
                bonuses.Add(bonusTwo);
        if (bonusTen != null)
            for (int i = 0; i < GameDataScript.b10f; i++)
                bonuses.Add(bonusTen);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        {
            hitsToDestroy--;
            if (hitsToDestroy == 0)
            {
                print(points);
                Destroy(gameObject);
                playerScript.BlockDestroyed(points);
                if (bonuses.Count != 0)
                {
                    int index = Random.Range(0, bonuses.Count);
                    var cur_bonus = bonuses[index];
                    var obj = Instantiate(cur_bonus, gameObject.transform.position, Quaternion.identity);
                }
            }
            else if (textComponent != null)
                textComponent.text = hitsToDestroy.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
