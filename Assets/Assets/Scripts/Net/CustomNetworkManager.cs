using UnityEngine;
using Mirror;
using System;
using System.Collections.Generic;

public class CustomNetworkManager : NetworkManager
{
    public override void OnStartClient()
    {
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

        foreach (var prefab in spawnablePrefabs)
        {
            NetworkClient.RegisterPrefab(prefab);
        }
    }

    public void Disconnect()
    {
        if (NetworkServer.active) StopHost();

        StopClient();
    }

    public void JoinLocal()
    {
        networkAddress = "localhost";
        StartClient();
    }

    public void HostLocal()
    {
        StartHost();
    }
}
