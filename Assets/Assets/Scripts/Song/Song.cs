using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Scriptable Object / Song")]
public class Song : ScriptableObject
{
    [field: SerializeField] public AudioClip Music { get; private set; }

    [SerializeField] private Note[] notes = new Note[0];

    private int currentIndex;

    public void Restart()
    {
        currentIndex = 0;
    }

    public bool TryGetNote(int tenthsecond, out Note note)
    {
        note = new();

        if (notes.Length <= currentIndex)
        {
            currentIndex = 0;
        }

        if(notes[currentIndex].TenthSeconds <= tenthsecond)
        {
            note = notes[currentIndex];

            currentIndex++;

            return true;
        }

        return false;
    }
}

[Serializable]
public class Note
{
    [field: SerializeField] public int TenthSeconds { get; private set; }
    [field: SerializeField] public int Key { get; private set; }
}
