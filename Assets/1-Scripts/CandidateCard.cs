using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class CandidateCard : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler,IPointerUpHandler
{
    public TextMeshProUGUI nameText;
    public Image characterIcon;
    [HideInInspector]
    public Character character;
    private Vector3 originalScale;
    private Quaternion originalRotation;
    public CharacterData testData;
     
    public void Awake()
    {
        originalScale = transform.localScale;
        originalRotation = transform.rotation;
        character = GetComponent<Character>();
    }



    public void SetupCard(CharacterData data)
    {
        character.AssignData(data);
        characterIcon.sprite = data.characterIcon;
        nameText.text = data.characterName;
    }


    public void SelectCard()
    {
        CandidateClickInfo.instance.ChangeCurrentData(character);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        /*
        if (hoverCursor != null)
        {
            Cursor.SetCursor(hoverCursor, cursorHotspot, CursorMode.Auto);
        }
        */

        transform.DOKill();
        transform.DOScale(originalScale * 1.15f, 0.25f).SetEase(Ease.OutBack).SetUpdate(true);
        transform.DORotate(new Vector3(0, 0, 1.5f), 0.25f).SetEase(Ease.OutBack).SetUpdate(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
       // Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        transform.DOKill();
        transform.DOScale(originalScale, 0.2f).SetEase(Ease.OutQuad).SetUpdate(true);
        transform.DORotateQuaternion(originalRotation, 0.2f).SetEase(Ease.OutQuad).SetUpdate(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(originalScale * 0.95f, 0.1f).SetEase(Ease.OutQuad).SetUpdate(true);
        transform.DORotate(new Vector3(0, 0, -0.5f), 0.1f).SetEase(Ease.OutQuad).SetUpdate(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(originalScale * 1.05f, 0.15f).SetEase(Ease.OutBack).SetUpdate(true);
        transform.DORotate(new Vector3(0, 0, 1.5f), 0.15f).SetEase(Ease.OutBack).SetUpdate(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SelectCard();
    }
}
