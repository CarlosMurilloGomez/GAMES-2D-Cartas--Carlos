using UnityEngine;

[CreateAssetMenu(fileName = "ConfigValues", menuName = "Scriptable Objects/ConfigValues")]
public class ConfigValues : ScriptableObject
{
    public int numberPlayers;

    public int maxPlayers;

    public int initialCards;

    public string playerName;
    
}
