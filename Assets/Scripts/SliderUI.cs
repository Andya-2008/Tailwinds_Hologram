using UnityEngine;
using System.Collections;

public class SliderUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.0f;
    public float minAlpha = 0.0f; // Fully transparent
    public float maxAlpha = 2.0f; // Fully opaque

    [SerializeField] float reappearTime = 3f;
    public float timeSinceDisappeared;

    void Update()
    {
        if (canvasGroup != null)
        {
            Debug.Log("Fading");
            StartCoroutine(RepeatingFade());
        }
        
    }

    private IEnumerator RepeatingFade()
    {
        // Infinite loop for continuous fading
        while (true)
        {
            // Fade In (from current alpha to maxAlpha)
            yield return StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, maxAlpha, fadeDuration));

            // Fade Out (from maxAlpha to minAlpha)
            yield return StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, minAlpha, fadeDuration));
        }
    }

    // The core fade coroutine (slightly modified for reusability)
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float startAlpha, float endAlpha, float duration)
    {
        if (Time.time - timeSinceDisappeared > reappearTime)
        {
            float startTime = Time.time;
            float endTime = startTime + duration;

            while (Time.time < endTime)
            {
                    float t = (Time.time - startTime) / duration;
                // Use smooth step for a more natural pulse effect if desired, otherwise Mathf.Lerp is fine
                cg.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
                yield return null; // Wait for the next frame
            }

            cg.alpha = endAlpha; // Ensure the final alpha value is set correctly at the end of the transition
        }
        else
        {
            cg.alpha = 0;
        }
    }
}