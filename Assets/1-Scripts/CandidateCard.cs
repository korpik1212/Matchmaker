using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class CandidateCard : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI nameText;
    public Image characterIcon;
    [HideInInspector]
    public Character character;

    public CharacterData testData;
     
    public void Awake()
    {
        character = GetComponent<Character>();
        SetupCard(testData);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
