using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class botScript : BasePlayer
{
    public GameObject back;

    void Start()
    {
        tableGO = GameObject.FindGameObjectWithTag("Table");
    }

    void Update()
    {
        
        if (transform.childCount == 0)
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
        back.GetComponent<Image>().color = Color.yellow;
        yield return new WaitForSeconds(2.0f);

        int num = Random.Range(0, transform.childCount);
        GameObject card = transform.GetChild(num).gameObject;

        //Se comprueba que tenga mas cosas a parte de antidotos
        bool onlyAntidote = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<CardScript>().cardType != CardType.antidote)
            {
                onlyAntidote = false;
            }
        }

        //Si tiene mas cartas a parte de antidotos, busca una carta para jugar que no lo sea
        if (!onlyAntidote)
        {
            while (card.GetComponent<CardScript>().cardType == CardType.antidote)
            {
                num = Random.Range(0, transform.childCount);
                card = transform.GetChild(num).gameObject;
            }
        }

        card.GetComponent<AudioSource>().Play();
        card.GetComponent<CardScript>().moveCard(tableGO);



        if (card.GetComponent<CardScript>().cardType != CardType.pass) // Si no es una carta de pasar, hay que robar
        {
            if (card.GetComponent<CardScript>().cardType == CardType.tornado) // Si se juega la carta de tornado, se baraja el mazo
            {
                deckGO.GetComponent<DeckScript>().shuffleDeck();
                yield return new WaitForSeconds(3f);

            }
            else
            {
                yield return new WaitForSeconds(1f);
            }

            bool desdeArriba = true;
            if (card.GetComponent<CardScript>().cardType == CardType.thieve) // Si juega una carta de ladron, roba desde abajo
            {
                desdeArriba = false;
            }

            drawCard(desdeArriba);
        }


        yield return new WaitForSeconds(2f);

        back.GetComponent<Image>().color = Color.white;
        endTurn();
    }


}
