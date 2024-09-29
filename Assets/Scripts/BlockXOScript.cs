using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockXOScript : MonoBehaviour
{
    PlayerScript playerScript;
    public GameObject textObject;
    Text textComponent;
    public char mark;
    public int points;

    // Start is called before the first frame update
    void Start()
    {
        if (textObject != null)
        {
            textComponent = textObject.GetComponent<Text>();
            textComponent.text = mark.ToString();
        }

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        {
            if (mark == 'O')
                mark = 'X';
            else
                mark = 'O';

            playerScript.BlockMarkChanged();

           if (textComponent != null)
                textComponent.text = mark.ToString();
        }
    }

    public void Deleter()
    {
        print(points);
        Destroy(gameObject);
        playerScript.BlockDestroyed(points);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
