using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CandidateCard : MonoBehaviour
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
}
