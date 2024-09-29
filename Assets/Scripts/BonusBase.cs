using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusBase : MonoBehaviour
{
    public float bonusFallSpeed = 70;
    public string text = "+100";
    public Color textColor;
    public Color blockColor;

    Rigidbody2D rb;

    void Start()
    {
        //this.GetComponent<Image>().color = blockColor;
        //this.GetComponent<Canvas>().GetComponent<Text>().text = text;
        //this.GetComponent<Canvas>().GetComponent<Text>().color = textColor;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, -bonusFallSpeed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BonusActivate();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
            Destroy(gameObject);
    }

    protected virtual void BonusActivate()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().gameData.points += 100;
    }
}
