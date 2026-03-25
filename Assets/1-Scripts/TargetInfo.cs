using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TargetInfo : MonoBehaviour
{
    public static TargetInfo targetInfo;
    [HideInInspector]
    public Character character;


    public Image characterImage;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI aboutMeText;
    public Interest interestPrefab;
    public Transform interestContainer;
    public Transform promptContainer;
    public PromptObject promptPrefab;


    private void Awake()
    {
        if (targetInfo == null)
        {
            targetInfo = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        character = GetComponent<Character>();
    }


    public void ChangeCurrentData(Character character)
    {
        //character.data = character.data;
        UpdateInfoWithData(character.data);
    }

    public void UpdateInfoWithData(CharacterData data)
    {
        if (data == null) return;

        characterImage.sprite = data.characterIcon;
        characterName.text = data.characterName;
        aboutMeText.text = data.characterBio;

        foreach (Transform child in interestContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (string interest in data.interests)
        {
            Interest newInterest = Instantiate(interestPrefab, interestContainer);
            newInterest.GetComponentInChildren<TextMeshProUGUI>().text = interest;
        }

        foreach (Transform child in promptContainer)
        {
            Destroy(child.gameObject);
        }

        if (promptPrefab != null && data.promptList != null)
        {
            foreach (Prompt prompt in data.promptList)
            {
                PromptObject newPrompt = Instantiate(promptPrefab, promptContainer);
                newPrompt.SetupPrompt(prompt.promptText, prompt.answerText);


            }
        }
    }
}
