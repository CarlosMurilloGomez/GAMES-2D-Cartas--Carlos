using UnityEngine;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{
    public GameObject gameManager;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void activateCards(bool enabled)
    {
        Debug.Log("Deshabilitamos las cartas del jugador");
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Button>().enabled = enabled;

        }
 
    }

    public void play()
    {

        gameManager.GetComponent<GameManager>().nextTurn();

    }

}
