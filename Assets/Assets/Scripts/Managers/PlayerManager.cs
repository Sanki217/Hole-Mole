using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IManager
{
    public event Action<List<Player>> OnPlayerListUpdated;

    public int PlayerCount => players.Count;

    private List<Player> players = new List<Player>();

    public void AddPlayer(Player player)
    {
        players.Add(player);

        OnPlayerListUpdated?.Invoke(players);
    }

    public void RemovePlayer(Player player)
    {
        players.Remove(player);

        OnPlayerListUpdated?.Invoke(players);
    }
}
