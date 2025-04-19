using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupSlide : MonoBehaviour
{
    public RectTransform popupPanel;
    public Button playButton;

    public float slideDuration = 0.5f;

    private Vector2 hiddenPos = new Vector2(400f, 0f);  // offscreen right
    private Vector2 visiblePos = new Vector2(-10f, 0f); // slightly visible

    void Start()
    {
        // Start offscreen
        popupPanel.anchoredPosition = hiddenPos;

        // Slide in when the scene starts
        StartCoroutine(Slide(popupPanel, hiddenPos, visiblePos, slideDuration));

        // Hook up the Play button
        playButton.onClick.AddListener(() =>
        {
            StartCoroutine(Slide(popupPanel, visiblePos, hiddenPos, slideDuration));
        });
    }

    IEnumerator Slide(RectTransform panel, Vector2 from, Vector2 to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            panel.anchoredPosition = Vector2.Lerp(from, to, t);
            yield return null;
        }

        panel.anchoredPosition = to; // Snap to final position
    }
}
