using UnityEngine;

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

            int num = Random.Range(0, transform.childCount);
            GameObject card = transform.GetChild(num).gameObject;
            card.GetComponent<CardScript>().moveCard(tableGO);


            drawCard();
        }
        


    }
}
