using TMPro;
using UnityEngine;

public class Interest : MonoBehaviour
{

    public TextMeshProUGUI interestNameText;
    public void SetupInterest(string interestName)
    {

        interestNameText.text = interestName;
    }
}
