using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FeedbackObject : MonoBehaviour
{
    public Image[] startImages = new Image[5];
    public TextMeshProUGUI feedbackText;

    [Header("Animation Settings")]
    public float appearDuration = 0.5f;
    public Ease appearEase = Ease.OutBack; // OutBack gives it a nice "pop" effect

    private void Awake()
    {
        // 1. Set the scale to 0 immediately so it is invisible before the animation starts
        transform.localScale = Vector3.zero;

        // 2. Ensure the Pivot is Top-Left (X=0, Y=1)
        // This makes the scale grow towards the bottom-right.
        RectTransform rect = GetComponent<RectTransform>();
        rect.pivot = new Vector2(0, 1);
    }

    private void Start()
    {
        // Start the grow animation
        transform.DOScale(1f, appearDuration).SetEase(appearEase);

        // Destroy the object after 2 seconds
        Destroy(this.gameObject, 2f);
    }

    public void SetupFeedback(string text, int starCount)
    {
        feedbackText.text = text;
        for (int i = 0; i < startImages.Length; i++)
        {
            if (i < starCount)
            {
                startImages[i].color = Color.white;
            }
            else
            {
                startImages[i].color = Color.black;
            }
        }
    }
}