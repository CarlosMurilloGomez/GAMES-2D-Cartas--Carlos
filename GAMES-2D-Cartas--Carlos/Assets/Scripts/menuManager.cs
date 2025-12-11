using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{

    public TextMeshProUGUI numJugadoresText;
    public ConfigValues config;

    void Start()
    {
        
    }

    public void jugar()
    {
        config.numberPlayers = int.Parse(numJugadoresText.text);
        SceneManager.LoadScene("countGame");
    }
}
