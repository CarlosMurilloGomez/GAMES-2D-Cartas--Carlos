using UnityEngine;
using UnityEngine.UI;

public class playerScript : BasePlayer
{
    public GameObject gameManager;

    public GameObject deckBack;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void activateCards(bool enabled)
    {
        Debug.Log("activamos/desactivamos las cartas del jugador: " + this.name);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Button>().enabled = enabled;
            deckBack.GetComponent<Button>().enabled = enabled;
        }
 
    }

    public override void gameTurn()
    {
        if (isAlive())
        {
            Debug.Log("Turno del jugador: " + this.name);
            startTurn();
            activateCards(true);
        }

    }

}
