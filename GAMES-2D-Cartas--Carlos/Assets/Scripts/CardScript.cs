using System.Collections;
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

    public GameObject tableGO;

    private Transform parentPlayer;
    void Start()
    {
        tableGO = GameObject.FindGameObjectWithTag("Table");
        //tableGO = GameObject.Find("Table");
    }

    void Update()
    {
        
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
        parentPlayer = transform.parent;
        transform.SetParent(destino.transform, false);

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


}
