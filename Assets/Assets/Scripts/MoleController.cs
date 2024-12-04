using System.Collections;
using UnityEngine;
using TMPro;

public class MoleController : MonoBehaviour
{
    public float minY = 0f; // Mole's resting Y position
    public float maxY = 1f; // Mole's active Y position
    public int riseFrames = 30; // Frames to go from minY to maxY
    public int stayFrames = 60; // Frames to stay at maxY
    public int fallFrames = 30; // Frames to go from maxY to minY
    public Collider hammerCollider; // Assign the hammer's collider
    public TextMeshProUGUI scoreText; // Reference to the score UI

    private int score = 0; // Player's score
    private bool isHit = false; // Tracks if the mole was hit
    private Vector3 initialPosition;

    private Coroutine animationCoroutine; // Tracks the current animation coroutine

    void Start()
    {
        initialPosition = transform.position; // Store the initial position
        StartCoroutine(MoleRoutine()); // Start the mole behavior
    }

    IEnumerator MoleRoutine()
    {
        while (true)
        {
            // Rise phase
            yield return MoveMole(minY, maxY, riseFrames);

            // Stay phase
            for (int i = 0; i < stayFrames; i++)
            {
                if (isHit) break; // Interrupt if mole is hit
                yield return null;
            }

            // Fall phase
            yield return MoveMole(maxY, minY, fallFrames);

            // Reset hit flag
            isHit = false;
        }
    }

    IEnumerator MoveMole(float startY, float endY, int frames)
    {
        for (int i = 0; i <= frames; i++)
        {
            if (isHit) break; // Interrupt if mole is hit
            float t = (float)i / frames;
            float newY = Mathf.Lerp(startY, endY, t);
            transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
            yield return null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == hammerCollider && !isHit)
        {
            isHit = true; // Mark the mole as hit
            score += 100; // Add points
            UpdateScore(); // Update the score display

            // Interrupt current animation and fall back to minY
            if (animationCoroutine != null)
                StopCoroutine(animationCoroutine);
            animationCoroutine = StartCoroutine(MoveMole(transform.position.y, minY, fallFrames));
        }
    }

    void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }
}
