using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UI;
using System.Collections.Generic;


public abstract class BasePlayer : MonoBehaviour
{
    public GameObject deckGO;
    public GameObject tableGO;
    private bool finish;
    public CardPrefabs prefabs;

    private bool alive;
    public AudioClip sonidoMorirse;
    public AudioClip sonidoSalvarse;
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
                card.GetComponent<CardScript>().ponerBack(); //Se pone boca a abajo para que no se vea la carta que se está robando
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
                GetComponent<AudioSource>().clip = sonidoSalvarse;
                GetComponent<AudioSource>().Play();
            }
            else
            {
                //si no hay morirse
                GetComponent<AudioSource>().clip = sonidoMorirse;
                GetComponent<AudioSource>().Play();
                alive = false;
                Debug.Log("Pierde el jugador: " + this.name);
                card.GetComponent<Animator>().SetTrigger("lose");
            }


        }
        else if (card.GetComponent<CardScript>().cardType == CardType.noAction) //Si se roba un animal, se comprueba si se tiene 3 de un mismo animal
        {

            StartCoroutine(comprobarTrioAnimales());
        }

  
        
    }

    IEnumerator comprobarTrioAnimales()
    {
        int koalas = 0;
        int cats = 0;
        int turtles = 0;
        int pandas = 0;
        yield return new WaitForSeconds(1.5f); //Espera que el jugador reciba la carta de animal robada

        for (int i = 0; i < transform.childCount; i++) //Cuenta las cartas de cada animal que tiene el jugador en la mano
        {
            if (transform.GetChild(i).GetComponent<Image>().sprite.name == "koala")
            {
                koalas++;
            }
            else if (transform.GetChild(i).GetComponent<Image>().sprite.name == "cat")
            {
                cats++;
            }
            else if (transform.GetChild(i).GetComponent<Image>().sprite.name == "turtle")
            {
                turtles++;
            }
            else if (transform.GetChild(i).GetComponent<Image>().sprite.name == "panda")
            {
                pandas++;
            }
        }
        //Si se tiene 3 animales del mismo tipo, estos se eliminan y se invoca una carta aleatoria entre las siguientes: pass, future, thieve y tornado
        if (koalas >= 3)
        {
            conseguirCartaAleatoria("koala");
        }
        if (cats >= 3)
        {
            conseguirCartaAleatoria("cat");
        }
        if (turtles >= 3)
        {
            conseguirCartaAleatoria("turtle");
        }
        if (pandas >= 3)
        {
            conseguirCartaAleatoria("panda");
        }
    }

    public void conseguirCartaAleatoria(string tipo)
    {
        List<int> cartasADestruir = new List<int>();
        for (int i = 0; i < transform.childCount; i++) //Se recogen las cartas del animal repetido
        {
            if (transform.GetChild(i).GetComponent<Image>().sprite.name == tipo)
            {
                cartasADestruir.Add(i);
            }

        }
        foreach (var carta in cartasADestruir) // se destruyen las cartas de animales repetidos
        {
            Destroy(transform.GetChild(carta).gameObject);
        }
        
        List<GameObject> cartasAEscoger = new List<GameObject>(); //Se rellena la lista con las cartas que se podran invocar
        cartasAEscoger.Add(prefabs.pass);
        cartasAEscoger.Add(prefabs.future);
        cartasAEscoger.Add(prefabs.tornado);
        cartasAEscoger.Add(prefabs.thieve);

        int cartaNueva = Random.Range(0, 4); //Se elige una aleatoria


        //Y se invoca en la mano del jugador
        GameObject card = Instantiate(cartasAEscoger[cartaNueva]);
        card.transform.SetParent(transform);
        card.transform.localPosition = new Vector2(0, 0);
    }

}
