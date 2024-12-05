using UnityEngine;
using Mirror;
using Steamworks;

public class SteamLobby : MonoBehaviour, IManager
{
    private CustomNetworkManager _manager => NetworkManager.singleton as CustomNetworkManager;

    private Callback<LobbyCreated_t> _lobbyCreated;
    private Callback<GameLobbyJoinRequested_t> _joinRequest;
    private Callback<LobbyEnter_t> _lobbyEntered;

    public ulong CurrentLobbyID;
    private const string HostAddressKey = "HostAddress";

    private void Start()
    {
        if (!SteamManager.Initialized) return;
        _lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        _joinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
        _lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }


    public void LeaveLobby() => SteamMatchmaking.LeaveLobby(new CSteamID(CurrentLobbyID));

    public void JoinLobby(CSteamID lobbyID)
    {
        SteamMatchmaking.JoinLobby(lobbyID);
    }

    public void HostLobby()
    {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, NetworkManager.singleton.maxConnections);
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK) return;
        _manager.StartHost();

        CSteamID steamID = new CSteamID(callback.m_ulSteamIDLobby);

        SteamMatchmaking.SetLobbyData(steamID, HostAddressKey, SteamUser.GetSteamID().ToString());
        SteamMatchmaking.SetLobbyData(steamID, "name", SteamFriends.GetPersonaName().ToString());
    }

    private void OnJoinRequest(GameLobbyJoinRequested_t callback)
    {
        JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        CSteamID steamID = new CSteamID(callback.m_ulSteamIDLobby);
        CurrentLobbyID = callback.m_ulSteamIDLobby;

        if (NetworkServer.active) return;
        _manager.networkAddress = SteamMatchmaking.GetLobbyData(steamID, HostAddressKey);

        Debug.Log("Setting network address to " + HostAddressKey);
        _manager.StartClient();
    }
}
