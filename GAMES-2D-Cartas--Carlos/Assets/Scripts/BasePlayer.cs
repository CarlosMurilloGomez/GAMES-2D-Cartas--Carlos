using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public abstract class BasePlayer : MonoBehaviour
{
    public GameObject deckGO;
    public GameObject tableGO;
    private bool finish;

    private bool alive;

    private void Awake()
    {
        alive = true;
        finish = false;
    }
    void Start()
    {
        tableGO = GameObject.FindGameObjectWithTag("Table");
        //if (deckGO == null)
        //{
        //    deckGO = GameObject.Find("Deck");
        //}
    }

    void Update()
    {
        
    }

    public bool isAlive()
    {
        return alive;
    }

    public void startTurn()
    {
        finish = false;
    }

    public void endTurn()
    {
        finish = true;
    }

    public bool isFinish()
    {
        return finish;
    }

    public abstract void gameTurn();

    public void drawCard(bool desdeArriba)
    {
        Debug.Log("Robamos carta para: " + this.name);

        //Implementar robar carta
        GameObject card = deckGO.GetComponent<DeckScript>().getCard(desdeArriba);


        if (this is playerScript)
        {
            card.GetComponent<CardScript>().drawCardPlayer(gameObject);

        }
        else
        {
            if (card.GetComponent<CardScript>().cardType != CardType.poison)
            {
                card.GetComponent<CardScript>().ponerBack();
            }
            card.GetComponent<CardScript>().moveCard(gameObject);

        }

        if (card.GetComponent<CardScript>().cardType == CardType.poison)
        {
            card.transform.SetParent(GameObject.Find("Poisons").transform);

            GameObject antidoto = null;
            //mirar si hay antidotos
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<CardScript>().cardType == CardType.antidote){
                    antidoto = transform.GetChild(i).gameObject;
                }
            }
            //si hay, lanzarlo
            if ( antidoto != null)
            {

                card.GetComponent<Animator>().SetTrigger("win");
                antidoto.GetComponent<CardScript>().moveCard(tableGO);
            }
            else
            {
                //si no hay morirse
                alive = false;
                Debug.Log("Pierde el jugador: " + this.name);
                card.GetComponent<Animator>().SetTrigger("lose");
            }


        }

  
        
    }

}
