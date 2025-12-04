using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class DeckScript : MonoBehaviour
{
    public ConfigValues config;
    public CardPrefabs prefabs;

    public GameObject players;
    public GameObject backs;

    public GameObject poisonAnim;

    public GameObject textoStart;
    public GameObject shuffleEffect;

    void Start()
    {

        StartCoroutine(waitDeck());


    }

    public void Start2()
    {
        //Generar el mazo inicial
        generateDeckIni();

        //repartir 
        StartCoroutine(toDealPlayers());


    }

    public void Start3()
    {
        //añadir los poison
        addPoison();

        //mezclar
        shuffleDeck();
        

        textoStart.GetComponent<Animator>().SetTrigger("start");
        StartCoroutine(Destruir(textoStart, 5f));

        //comenzar el juego
        GameManager.Instance.startGame();
    }

    IEnumerator Destruir(GameObject objeto, float segundos)
    {
        yield return new WaitForSeconds(segundos);

        Destroy(objeto);
    }
    void Update()
    {
        
    }
    /*
            Vamos a generar el mazo:
            - pass y future ============= nº jugadores
            - antidote ================== nº jugadores -1
            - panda, koala, cat, turtle = nº jugadores +1 
            
            despues se reparten y se añaden los poison:
            - poison ==================== nº jugadores
        */


    private void generateCards(GameObject prefab, int n)
    {
        for (int i = 0; i < n; i++)
        {
            GameObject card = Instantiate(prefab);
            card.transform.SetParent(transform);
            card.transform.localPosition = new Vector2(0, 0);
        }
        
    }
    public void generateDeckIni()
    {
        Debug.Log("Generamos en mazo inicial");
        generateCards(prefabs.pass, config.numberPlayers);
        generateCards(prefabs.future, config.numberPlayers);
        generateCards(prefabs.antidote, config.numberPlayers - 1);
        generateCards(prefabs.panda, config.numberPlayers + 1);
        generateCards(prefabs.koala, config.numberPlayers + 1);
        generateCards(prefabs.cat, config.numberPlayers + 1);
        generateCards(prefabs.turtle, config.numberPlayers + 1);

    }

    IEnumerator toDealPlayers()
    {
        //3 cartas por jugador
        Debug.Log("Repartir el mazo a los jugadores");

        int i = 0;
        foreach (Transform player in players.transform)
        {
            if (i < config.numberPlayers)
            {
                StartCoroutine(toDeal(player.gameObject));

                yield return new WaitForSeconds(config.initialCards);

                if (player.transform.GetComponent<BasePlayer>() is botScript)
                {
                    backs.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else
            {
                Destroy(player.gameObject);
                Destroy(backs.transform.GetChild(i).gameObject);
            }
            i++;
        }

        //desactivar las cartas al jugador hasta que le toque su turno
        players.transform.GetChild(0).GetComponent<playerScript>().activateCards(false);

        StartCoroutine(waitPoison());

    }


    IEnumerator toDeal(GameObject player)
    {
        Debug.Log("repartimos cartas al jugador: " + player.name);
        //Se reparten X cartas por jugador
        for (int i = 0; i < config.initialCards; i++)
        {
            int num = Random.Range(0, transform.childCount);
            GameObject card = transform.GetChild(num).gameObject;

            if (player.GetComponent<BasePlayer>() is playerScript)
            {
                card.GetComponent<CardScript>().drawCardPlayer(player);
            }
            else
            {                
                card.GetComponent<CardScript>().ponerBack();
                card.GetComponent<CardScript>().moveCard(player);
            }

            yield return new WaitForSeconds(0.5f);
            card.GetComponent<CardScript>().ponerImagen();

        }

    }

    public void addPoison()
    {
        Debug.Log("Añadir las cartas de veneno");
        generateCards(prefabs.poison, (config.numberPlayers-1)*2);

    }

    public void shuffleDeck()
    {
        // 1. Obtener hijos actuales
        List<Transform> oldCards = new List<Transform>();
        foreach (Transform card in transform)
            oldCards.Add(card);

        // 2. Crear una lista auxiliar para barajar
        List<Transform> shuffled = new List<Transform>(oldCards);

        // 3. Barajar la lista (Fisher-Yates)
        for (int i = shuffled.Count - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            var temp = shuffled[i];
            shuffled[i] = shuffled[rand];
            shuffled[rand] = temp;
        }

        // 4. Por cada carta barajada, crear una copia y borrar la original
        for (int i = 0; i < shuffled.Count; i++)
        {
            // Instanciar carta en nueva posición
            GameObject newCard = Instantiate(shuffled[i].gameObject, transform);
            newCard.transform.position = transform.position;
            // Borrar la original
            Destroy(shuffled[i].gameObject);
        }

        backs.transform.GetChild(0).GetComponent<Animator>().SetTrigger("shuffle");
        shuffleEffect.GetComponent<Animator>().SetTrigger("shuffle");

    }

    public GameObject getCard()
    {
        //int num = Random.Range(0, transform.childCount);
        GameObject card = transform.GetChild(0).gameObject;
        return card;
    }

    IEnumerator waitDeck()
    {
        yield return new WaitForSeconds(3.0f);

        Start2();
    }

    IEnumerator waitPoison()
    {
        poisonAnim.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(2.0f);

        Start3();
    }
}
