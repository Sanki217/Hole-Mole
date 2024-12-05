using UnityEngine;
using TMPro;

public class PlayerListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text playerNameTxt;

    private Player player;

    public void Initialize(Player Player)
    {
        player = Player;

        OnPlayerNameSet(player.PlayerName);

        player.OnPlayerNameSet += OnPlayerNameSet;
    }

    private void OnDestroy()
    {
        if(player != null) player.OnPlayerNameSet -= OnPlayerNameSet;
    }

    private void OnPlayerNameSet(string newPlayerName)
    {
        playerNameTxt.text = newPlayerName;
    }
}
