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

    public GameObject panelPausa;


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
        nombresText[0].text = config.playerName;
        listaNombres = new List<string>()
        {
            "Carlos", "Nicolas", "Pepe", "Juan", "Pedro", "Antonio", "Sergio"
        };
        for (int i = 0; i < config.numberPlayers - 1; i++)
        {
            int nombreRandom = Random.Range(0, listaNombres.Count - 1);
            nombresText[i+1].text = listaNombres[nombreRandom];
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

            //Pulsar esc para abrir el menu de pausa
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                panelPausa.gameObject.SetActive(panelPausa.activeSelf!);
            }
        }


    }

    public void menuPausa(bool enable)
    {
        panelPausa.gameObject.SetActive(enable);
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
        for (int i = 0; i < players.transform.childCount; i++)
        {
            if (players.transform.GetChild(i).GetComponent<BasePlayer>().isAlive())
            {
                noLosers.Add(players.transform.GetChild(i));
            }
            else
            {
                nombresText[i].color = Color.red;
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
