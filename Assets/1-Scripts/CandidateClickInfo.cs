using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CandidateClickInfo : MonoBehaviour
{
  
    public static CandidateClickInfo instance;

    public Character currentlySelectedCharacter;

    public Image characterImage;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI aboutMeText;
    public Interest interestPrefab;
    public Transform interestContainer;
    public Transform promptContainer;
    public PromptObject promptPrefab;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeCurrentData(Character character)
    {
        currentlySelectedCharacter = character;
        UpdateInfoWithData(character.data);
    }

    public void UpdateInfoWithData(CharacterData data)
    {
        if (data == null) return;

        characterImage.sprite = data.characterIcon;
        characterName.text = data.characterName;
        aboutMeText.text = data.characterBio;

        // --- Wiggle Persistent UI Elements ---
        ApplyWiggle(characterImage.rectTransform);
        ApplyWiggle(characterName.rectTransform);
        ApplyWiggle(aboutMeText.rectTransform);

        foreach (Transform child in interestContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (string interest in data.interests)
        {
            Interest newInterest = Instantiate(interestPrefab, interestContainer);
            newInterest.GetComponentInChildren<TextMeshProUGUI>().text = interest;
            
            // --- Wiggle Newly Created Interest ---
            ApplyWiggle(newInterest.GetComponent<RectTransform>());
        }

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

    private void ApplyWiggle(RectTransform target)
    {
        if (target == null) return;

        // 1. Stop any existing tweens on this object
        target.DOKill();
        
        // 2. Reset rotation
        target.localRotation = Quaternion.identity;

        // 3. Force the UI to calculate its layout right now
        // This prevents the LayoutGroup from "fighting" the tween on the first frame
        Canvas.ForceUpdateCanvases();

        // 4. Perform the wiggle. 
        // Adding a very tiny delay (0.01s) helps bypass Layout Group resets.
        target.DOPunchRotation(new Vector3(0, 0, 15f), 0.5f, 10, 1).SetDelay(0.01f);
        
        // Optional: Adding a small scale punch makes it look more "alive"
        target.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f, 10, 1).SetDelay(0.01f);
    }
}
