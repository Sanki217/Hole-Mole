using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Mole : MonoBehaviour
{
    public bool Active;

    [SerializeField] private Animator animator;

    [SerializeField] private VisualEffect hitVFX;
    [SerializeField] private VisualEffect missFVX;
    [SerializeField] private GameObject lightEffect;

    private DifficultyManager difficultyManager;

    private void Start()
    {
        GameManager.Instance.TryGetManager(out difficultyManager);
    }

    public void ShowHit(bool success)
    {
        string animation = success ? "Mole Hammer Hit" : "Mole Hammer Miss";

        VisualEffect vfx = success ? hitVFX : missFVX;

        vfx.Play();

        animator.Play(animation);

        if (success) DeactivateMole();
    }

    public void ActivateMole()
    {
        StartCoroutine(nameof(TimeWindow));

        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);

        lightEffect.SetActive(true);

        Active = true;
    }

    public void DeactivateMole()
    {
        StopAllCoroutines();

        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

        lightEffect.SetActive(false);

        Active = false;
    }

    private void TimeWindowPassed()
    {
        if (!Active) return;

        DeactivateMole();
    }

    private IEnumerator TimeWindow()
    {
        yield return new WaitForSeconds(difficultyManager.GetDifficultyInFloat());

        TimeWindowPassed();
    }
}
