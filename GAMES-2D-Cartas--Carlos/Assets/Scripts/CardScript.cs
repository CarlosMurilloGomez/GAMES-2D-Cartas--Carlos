using System.Collections;
using UnityEngine;



public enum CardType
{
    poison,
    antidote,
    pass,
    future,
    noAction
}
public class CardScript : MonoBehaviour
{
    public CardType cardType;

    public GameObject tableGO;
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
        StartCoroutine("moveCardCoroutine", destino);
    }
    IEnumerator moveCardCoroutine(GameObject destino)
    {        
        transform.SetParent(destino.transform);

        float timeElipsed = 0f;
        float durMove = 10f;
        while (timeElipsed < durMove)
        {
            float factorT = timeElipsed / durMove;
            transform.position = Vector3.Lerp(transform.position, destino.transform.position, factorT);
            timeElipsed += Time.deltaTime;

            yield return null;
        }
    }

}
