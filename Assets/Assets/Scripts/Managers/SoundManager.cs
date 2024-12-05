using UnityEngine;

public class SoundManager : MonoBehaviour, IManager
{
    private HitManager hitManager;

    [SerializeField] private AudioClip[] missAudios = new AudioClip[0];
    [SerializeField] private AudioClip[] hitAudios = new AudioClip[0];

    [SerializeField] private AudioSource soundEffectAudioSource;
    [SerializeField] private AudioSource musicAudioSource;

    public void Start()
    {
        GameManager.Instance.TryGetManager(out hitManager);

        hitManager.OnHitInfoReceived += OnHitInfoReceived;
    }

    public void PlaySongMusic(AudioClip music)
    {
        musicAudioSource.PlayOneShot(music);
    }

    private void OnHitInfoReceived(HitInfo info)
    {
        if (info.Success) { PlaySuccessAudio(); }
        else { PlayerMissAudio(); }
    }

    private void PlaySuccessAudio()
    {
        soundEffectAudioSource.volume = 0.2f;
        soundEffectAudioSource.PlayOneShot(hitAudios[Random.Range(0, hitAudios.Length)]);
    }

    private void PlayerMissAudio()
    {
        soundEffectAudioSource.volume = 0.6f;
        soundEffectAudioSource.PlayOneShot(missAudios[Random.Range(0, missAudios.Length)]);
    }
}
