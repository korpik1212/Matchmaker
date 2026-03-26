using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackObject : MonoBehaviour
{


    public Image[] startImages = new Image[5];
    public TextMeshProUGUI feedbackText;


    private void Start()
    {
        Destroy(this.gameObject, 2f);
    }


    public void SetupFeedback(string text,int starCount)
    {

        feedbackText.text = text;
        for(int i=0; i<startImages.Length; i++)
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


