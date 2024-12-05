using UnityEngine;
using Mirror;
using System;

public class NetworkGameStateManager : NetworkBehaviour, IManager
{
    public event Action<NetworkGameState> OnNetworkGameStateSet;

    [SyncVar(hook = nameof(ClientHandleNetworkGameStateSet))]
    public NetworkGameState NetworkGameState;

    public void ChangeGameState(NetworkGameState newState)
    {
        NetworkGameState = newState;
    }

    public override void OnStartClient()
    {
        ClientHandleNetworkGameStateSet(NetworkGameState, NetworkGameState);
    }

    private void ClientHandleNetworkGameStateSet(NetworkGameState oldValue, NetworkGameState newValue)
    {
        OnNetworkGameStateSet?.Invoke(NetworkGameState);
    }
}

public enum NetworkGameState
{
    Lobby,
    Game
}
