using UnityEngine;
using Steamworks;

public class ServerList : MonoBehaviour
{
    [SerializeField] private ServerListItem serverListItemPrefab;
    [SerializeField] private Transform listTransform;

    public void RefreshServers()
    {
        int nFriends = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagAll);

        if (nFriends == -1)
            nFriends = 0;

        for (int i = 0; i < nFriends; ++i)
        {
            CSteamID friendSteamID = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagAll);

            string friendName = SteamFriends.GetFriendPersonaName(friendSteamID);

            if (SteamFriends.GetFriendGamePlayed(friendSteamID, out FriendGameInfo_t friendGameInfo))
            {
                CSteamID lobbyID = friendGameInfo.m_steamIDLobby;
                ShowServer(lobbyID, friendName);
            }
        }
    }

    public void ShowServer(CSteamID lobbyID, string name)
    {
        ServerListItem item = Instantiate(serverListItemPrefab, listTransform);
        item.Initialize(lobbyID, name);
    }
}
