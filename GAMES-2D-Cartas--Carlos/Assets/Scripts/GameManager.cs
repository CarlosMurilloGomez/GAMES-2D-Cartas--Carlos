using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ConfigValues config;

    public GameObject players;

    public int currentPlayer;

    private bool playON;

    public GameObject panelWin;
    public TextMeshProUGUI panelWinText;

    public GameObject panelFuture;
    public TextMeshProUGUI[] nombresText;
    private List<string> listaNombres;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {//evita duplicados del objeto gameManager
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }
    void Start()
    {
        generateNames();

        playON = false;
    }

    public void generateNames()
    {
        listaNombres = new List<string>()
        {
            "Carlos", "Nicolas", "Pepe", "Juan", "Pedro", "Antonio", "Sergio"
        };
        for (int i = 0; i < config.numberPlayers - 1; i++)
        {
            int nombreRandom = Random.Range(0, listaNombres.Count - 1);
            nombresText[i].text = listaNombres[nombreRandom];
            listaNombres.RemoveAt(i);
        }
    }



    void Update()
    {
        if (checkWinner())
        {
            playON = false;
        }
        if (playON)
        {
            //Le damos el turno al primer jugador
            if (players.transform.GetChild(currentPlayer).GetComponent<BasePlayer>().isFinish())
            {
                nextPlayer();
                nextPlay();
            }
        }
    }




    public void startGame()
    {
        Debug.Log("Comienza el juego");
        currentPlayer = 0;

        nextPlay();
        playON = true;
    }

    public void nextPlay()
    {
        Debug.Log("Jugada del jugador: "+ players.transform.GetChild(currentPlayer).name);
        players.transform.GetChild(currentPlayer).GetComponent<BasePlayer>().gameTurn();

    }

    public void nextPlayer()
    {
        currentPlayer++;
        if (currentPlayer >= players.transform.childCount)
        {
            currentPlayer = 0;
        }
        Debug.Log("Siguiente jugador: " + players.transform.GetChild(currentPlayer).name);

    }

    public bool checkWinner()
    {
        bool win = false;
        //Debug.Log("Comprobamos si hay ganador");

        List<Transform> noLosers = new List<Transform>();
        foreach (Transform player in players.transform)
        {
            if (player.GetComponent<BasePlayer>().isAlive())
            {
                noLosers.Add(player);
            }
        }

        if (noLosers.Count == 1)
        {
            win = true;
            panelWin.SetActive(true);
            panelWinText.text = "Ganador " + noLosers[0].transform.name;
            Debug.Log("El ganador es el jugador: " + noLosers[0].transform.name);

        }

        return win;
    }


    public void cerrarFuturo()
    {
        panelFuture.SetActive(false);
    }

    public void revancha()
    {
        SceneManager.LoadScene("cardGame");
    }

    public void salir()
    {
        SceneManager.LoadScene("mainMenu");
    }
}
