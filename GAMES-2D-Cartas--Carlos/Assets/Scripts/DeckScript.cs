using UnityEngine;
using UnityEngine.Rendering;

public class DeckScript : MonoBehaviour
{
    public ConfigValues config;
    public CardPrefabs prefabs;
    void Start()
    {
        generateDeckIni();
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

        generateCards(prefabs.pass, config.numberPlayers);
        generateCards(prefabs.future, config.numberPlayers);
        generateCards(prefabs.antidote, config.numberPlayers - 1);
        generateCards(prefabs.panda, config.numberPlayers + 1);
        generateCards(prefabs.koala, config.numberPlayers + 1);
        generateCards(prefabs.cat, config.numberPlayers + 1);
        generateCards(prefabs.turtle, config.numberPlayers + 1);

    }

    public void toDeal(GameObject player)
    {
        //3 cartas por jugador
        for (int i = 0; i < 3; i++)
        {
            int num = Random.Range(0, transform.childCount);
            GameObject card = transform.GetChild(num).gameObject;
            card.transform.SetParent(player.transform);
            card.transform.localPosition = new Vector2(0, 0);
        }

    }

    public void addPoison()
    {
        Debug.Log("Añadir las cartas de veneno");
        generateCards(prefabs.poison, config.numberPlayers);

    }
}
