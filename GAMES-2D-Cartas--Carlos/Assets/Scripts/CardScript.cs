using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;



public enum CardType
{
    poison,
    antidote,
    pass,
    future,
    noAction
        //tornado (barajar las cartas)
        //ladron (robar desde abajo)
}
public class CardScript : MonoBehaviour
{
    public CardType cardType;

    private GameObject tableGO;

    public Sprite imagen;
    public Sprite imagenBack;

    void Start()
    {
        tableGO = GameObject.FindGameObjectWithTag("Table");
    }

    void Update()
    {
        
    }

    public void ponerImagen()
    {
        GetComponent<Image>().sprite = imagen;
    }

    public void ponerBack()
    {
        GetComponent<Image>().sprite = imagenBack;
    }

    public void antidoteAction()
    {
        Debug.Log("Accion: antidote");
        moveCard(tableGO);

    }
    public void passAction()
    {
        Debug.Log("Accion: pass");
        moveCard(tableGO);

    }
    public void futureAction()
    {
        Debug.Log("Accion: future");
        moveCard(tableGO);
    }
    public void noAction()
    {
        Debug.Log("Accion: noAction");
        moveCard(tableGO);
    }

    public void moveCard(GameObject destino)
    {
        transform.SetParent(destino.transform);

        StartCoroutine(moveCardCoroutine(destino));
    }


    IEnumerator moveCardCoroutine(GameObject destino)
    {

        float timeElipsed = 0f;
        float durMove = 1f;

        while (timeElipsed < durMove)
        {
            float factorT = timeElipsed / durMove;
            transform.position = Vector3.Lerp(transform.position, destino.transform.position, factorT);
            timeElipsed += Time.deltaTime;

            yield return null;
        }
        transform.position = destino.transform.position;


    }

    public void drawCardPlayer(GameObject player)
    {
        transform.SetParent(GameObject.Find("Canvas").transform);

        StartCoroutine(drawCardPlayerCoroutine(player));


    }

    IEnumerator drawCardPlayerCoroutine(GameObject destino)
    {

        float timeElipsed = 0f;
        float durMove = 1f;

        while (timeElipsed < durMove)
        {
            float factorT = timeElipsed / durMove;
            transform.position = Vector3.Lerp(transform.position, destino.transform.position, factorT);
            timeElipsed += Time.deltaTime;

            yield return null;
        }
        transform.SetParent(destino.transform);

    }


}
