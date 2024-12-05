using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Scriptable Object / Song")]
public class Song : ScriptableObject
{
    [field: SerializeField] public AudioClip Music { get; private set; }

    [SerializeField] private Note[] notes = new Note[0];

    private int lastIndex;

    public void Restart()
    {
        lastIndex = 0;
    }

    public bool TryGetNote(int tenthsecond, out Note note)
    {
        note = new();

        if (notes.Length <= lastIndex)
        {
            lastIndex = 0;
        }

        if(notes[lastIndex].TenthSeconds == tenthsecond)
        {
            note = notes[lastIndex];

            lastIndex++;

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
