using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ConfigValues config;

    public GameObject deckGO;

    public GameObject[] players;
    public GameObject[] playerBacks;

    private int currentPlayer;
    void Start()
    {
        initDeck();
    }

    void Update()
    {

    }
    public void initDeck()
    {
        //Generar el amzo inicial
        Debug.Log("Generamos el mazo inicial");
        deckGO.GetComponent<DeckScript>().generateDeckIni();

        //repartir 
        toDealPlayers();

        //desactivar las cartas al jugador hasta que le toque su turno
        players[0].GetComponent<playerScript>().activateCards(false);

        //añadir los poison
        deckGO.GetComponent<DeckScript>().addPoison();

        //comenzar el juego
        startGame();

        //mezclar


    }

    public void toDealPlayers()
    {
        //3 cartas por jugador
        Debug.Log("Repartir el mazo a los jugadores");
        for (int i = 0; i < config.numberPlayers; i++)
        {
            if (i > 0)
            {
                playerBacks[i - 1].SetActive(true);
            }
            ;
            deckGO.GetComponent<DeckScript>().toDeal(players[i]);

        }

    }


    public void startGame()
    {
        Debug.Log("Comienza el juego");
        currentPlayer = 0;
        nextTurn();
    }

    public void nextTurn()
    {
        if (currentPlayer == config.numberPlayers - 1)
        {
            currentPlayer = 0;
            players[0].GetComponent<playerScript>().activateCards(true);

            Debug.Log("Turno del jugador: " + (currentPlayer + 1));
            players[currentPlayer].GetComponent<playerScript>().play();
        }
        else
        {
            currentPlayer++;
            players[0].GetComponent<playerScript>().activateCards(false);

            Debug.Log("Turno del jugador: " + (currentPlayer + 1));
            players[currentPlayer].GetComponent<botScript>().play();


        }


    }

}
