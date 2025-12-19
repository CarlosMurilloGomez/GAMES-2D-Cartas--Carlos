using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{

    public TextMeshProUGUI numJugadoresText;
    public TextMeshProUGUI playerName;
    public ConfigValues config;

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void jugar()
    {
        if (playerName.text.Trim() .Length > 1) {
            config.playerName = playerName.text.Trim();
        }
        else
        {
            config.playerName = "Player";
        }

            config.numberPlayers = int.Parse(numJugadoresText.text);
        SceneManager.LoadScene("cardGame");
    }

    public void irAlTutorial()
    {
        SceneManager.LoadScene("tutorial");
    }
}
