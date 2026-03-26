using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Settings")]
    public float shrinkScale = 0.9f;
    public float animationDuration = 0.1f;
    
    [Header("Bounce Settings")]
    public Vector3 bounceAmount = new Vector3(0.2f, 0.2f, 0.2f);
    public float bounceDuration = 0.3f;

    private Vector3 originalScale;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 1. Stop any current animations
        transform.DOKill(); 
        
        // 2. Smoothly move to the shrunk scale from wherever it currently is
        transform.DOScale(originalScale * shrinkScale, animationDuration).SetEase(Ease.OutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();
        
        // Return to normal
        transform.DOScale(originalScale, animationDuration).SetEase(Ease.OutQuad);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 1. Stop the current "shrink" or "bounce" animation immediately
        transform.DOKill(); 

        // 2. IMPORTANT: Force the scale back to a base value 
        // We reset it to the "hover" scale so the bounce feels consistent
        transform.localScale = originalScale * shrinkScale;

        // 3. Perform the punch/bounce
        // This adds the bounceAmount to the CURRENT scale and then returns to it
        transform.DOPunchScale(bounceAmount, bounceDuration, 10, 1);
    }
}