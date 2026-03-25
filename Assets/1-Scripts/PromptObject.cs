using TMPro;
using UnityEngine;

public class PromptObject : MonoBehaviour
{


    public TextMeshProUGUI promptInfoText;
    public TextMeshProUGUI promptAnswerText;


    public void SetupPrompt(string promptInfo, string promptAnswer)
    {
        promptInfoText.text = promptInfo;
        promptAnswerText.text = promptAnswer;
    }

}

