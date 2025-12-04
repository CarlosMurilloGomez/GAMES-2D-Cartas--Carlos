using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class botScript : BasePlayer
{
    public GameObject back;
    private GameObject tableGO;

    void Start()
    {
        tableGO = GameObject.FindGameObjectWithTag("Table");
    }

    void Update()
    {
        if (transform.childCount == 0 || !isAlive())
        {
            back.SetActive(false);
        }
        else
        {
            back.SetActive(true);
        }
    }

    public override void gameTurn()
    {
        if (isAlive())
        {
            Debug.Log("Turno del bot: " + this.name);
            startTurn();

            StartCoroutine(turnTime());

        }
        

    }
    IEnumerator turnTime()
    {
        yield return new WaitForSeconds(2.0f);

        int num = Random.Range(0, transform.childCount);
        GameObject card = transform.GetChild(num).gameObject;
        
        while (card.GetComponent<CardScript>().cardType == CardType.antidote)
        {
            num = Random.Range(0, transform.childCount);
            card = transform.GetChild(num).gameObject;
        }

        card.GetComponent<CardScript>().moveCard(tableGO);

        yield return new WaitForSeconds(1f);

        drawCard();

        yield return new WaitForSeconds(2f);

        endTurn();
    }


}
