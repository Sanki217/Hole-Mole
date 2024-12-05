using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviour
{
    [SerializeField] private PlayerListItem playerListItemPrefab;
    [SerializeField] private Transform content;

    private List<PlayerListItem> playerListItems = new List<PlayerListItem>();

    private PlayerManager playerManager;

    private void Start()
    {
        GameManager.Instance.TryGetManager(out playerManager);

        playerManager.OnPlayerListUpdated += OnPlayerListUpdated;
    }


    private void OnPlayerListUpdated(List<Player> players)
    {
        if (playerListItems.Count > 0)
            ClearPlayerListItems();

        playerListItems.Clear();

        Debug.Log(players.Count);

        foreach (Player player in players)
        {
            PlayerListItem item = Instantiate(playerListItemPrefab, content);

            item.Initialize(player);
        }
    }

    private void ClearPlayerListItems()
    {
        for (int i = playerListItems.Count - 1; i >= 0; i--)
        {
            PlayerListItem item = playerListItems[i];

            playerListItems.RemoveAt(i);

            Destroy(item.gameObject);
        }
    }
}
