using UnityEngine;

public class botScript : MonoBehaviour
{
    public GameObject gameManager;
    private GameObject tableGO;

    void Start()
    {
        tableGO = GameObject.FindGameObjectWithTag("Table");
    }

    void Update()
    {
        
    }

    public void play()
    {
        
        int num = Random.Range(0, transform.childCount);
        GameObject card = transform.GetChild(num).gameObject;
        card.GetComponent<CardScript>().moveCard(tableGO);


        gameManager.GetComponent<GameManager>().nextTurn();
    }
}
