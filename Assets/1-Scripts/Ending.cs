using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public TextMeshProUGUI ratingtext, currentRatingText, bossComment;

    public void TriggerEnd()
    {
        currentRatingText.text = ratingtext.text + "/5";

        float rating = 0f;
        string cleanText = ratingtext.text.Replace("Rating:", "").Replace("/ 5", "").Replace("/5", "").Trim();
        float.TryParse(cleanText, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out rating);

        if (rating < 3f)
        {
            bossComment.text = "Poor performance. We expect better matches.";
        }
        else
        {
            bossComment.text = "Excellent work! Our clients are very happy.";
        }
    }

    public void ResetToMainScene()
    {
        SceneManager.LoadScene(2);
    }
}