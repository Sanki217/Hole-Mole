using System.Collections;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public bool Active;

    [SerializeField] private Animator animator;

    public void ShowHit(bool success)
    {
        string animation = success ? "Mole Hammer Hit" : "Mole Hammer Miss";
        animator.Play(animation);

        if (success) DeactivateMole();
    }

    public void ActivateMole()
    {
        StartCoroutine(nameof(TimeWindow));

        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);

        Active = true;
    }

    public void DeactivateMole()
    {
        StopAllCoroutines();

        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

        Active = false;
    }

    private void TimeWindowPassed()
    {
        if (!Active) return;

        DeactivateMole();
    }

    private IEnumerator TimeWindow()
    {
        yield return new WaitForSeconds(0.6f);

        TimeWindowPassed();
    }
}
