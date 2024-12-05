using System.Collections;
using UnityEngine;
using Steamworks;
using TMPro;

public class ServerListItem : MonoBehaviour
{
    public CSteamID LobbyID { get; private set; }
    public string LobbyName { get; private set; }

    [SerializeField] private TMP_Text serverNameText;

    public void Initialize(CSteamID lobbyID, string name)
    {
        LobbyID = lobbyID;
        LobbyName = name;

        serverNameText.text = name;
    }

    public void Join()
    {
        SteamMatchmaking.JoinLobby(LobbyID);
    }
}
