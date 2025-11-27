using UnityEngine;
using UnityEngine.UI;

public abstract class BasePlayer : MonoBehaviour
{
    public GameObject deckGO;
    private bool finish;

    private bool alive;
    private void Awake()
    {
        alive = true;
        finish = false;
    }
    void Start()
    {
        if (deckGO == null)
        {
            deckGO = GameObject.Find("Deck");
        }
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

    public void drawCard()
    {
        Debug.Log("Robamos carta para: " + this.name);

        //Implementar robar carta
        GameObject card = deckGO.GetComponent<DeckScript>().getCard();
        card.GetComponent<CardScript>().moveCard(gameObject);
        

        if (card.GetComponent<CardScript>().cardType == CardType.poison)
        {
            alive = false;
            Debug.Log("Pierde el jugador: " + this.name);
        }

        endTurn();
        Debug.Log("Termina el tueno del jugador: " + this.name);
    }
}
