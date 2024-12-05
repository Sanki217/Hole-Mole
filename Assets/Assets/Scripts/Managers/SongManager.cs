using System;
using UnityEngine;

public class SongManager : MonoBehaviour, IManager
{
    public event Action<Note> OnNoteBroadcasted;

    [SerializeField] private Song currentSong;

    private NetworkGameStateManager networkGameStateManager;
    private SoundManager soundManager;

    private int currentTenthSecond;

    private void Start()
    {
        GameManager.Instance.TryGetManager(out networkGameStateManager);
        GameManager.Instance.TryGetManager(out soundManager);

        networkGameStateManager.OnNetworkGameStateSet += OnGameStateSet;

        currentSong.Restart();

        InvokeRepeating(nameof(TenthSecondTick), 0.1f, 0.1f);
    }

    private void TenthSecondTick()
    {
        if (networkGameStateManager.NetworkGameState != NetworkGameState.Game) return;

        currentTenthSecond++;

        if (currentSong.TryGetNote(currentTenthSecond, out Note note))
        {
            OnNoteBroadcasted?.Invoke(note);         
        }
    }

    private void OnGameStateSet(NetworkGameState NetworkGameState)
    {
        if (networkGameStateManager.NetworkGameState != NetworkGameState.Game) return;

        soundManager.PlaySongMusic(currentSong.Music);
    }
}
