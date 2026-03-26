using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{


    public static GameManager instance;


    public CharacterData tutorialTargetCharacter;
    public CharacterData[] tutorialCandidateCharacters;

    public CandidateCard candidatePrefab;
    public Transform candidateGrid;



    public Transform canvas;
    public FeedbackObject feedbackPrefab;



    public TextMeshProUGUI ratingText;
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
    public void Start()
    {
        SetupTutorialCharacters();
        ratingList.Add(1);
        ratingList.Add(1);
        ratingList.Add(1);
        ratingList.Add(1);
        ratingList.Add(1);
    }

    public void SetupTutorialCharacters()
    {
        TargetInfo.instance.ChangeCurrentData(tutorialTargetCharacter);
        foreach (CharacterData candidateData in tutorialCandidateCharacters)
        {
            CandidateCard newCandidate = Instantiate(candidatePrefab, candidateGrid);
            newCandidate.SetupCard(candidateData);

        }

    }


    public void Match()
    {

        Debug.Log("compatibility score:" + TargetInfo.instance.character.CheckCompability(CandidateClickInfo.instance.currentlySelectedCharacter));
        float val = TargetInfo.instance.character.CheckCompability(CandidateClickInfo.instance.currentlySelectedCharacter);
        int starValue = GetStarValue(val);
       // string feedbackText = TargetInfo.instance.character.data.feedbackTexts[0];
        CreatePrefabObject("helo", starValue);
        ChangeRating(starValue);
        //instantiate feedback prefab 



    }



    public List<int> ratingList = new List<int>();

    public void ChangeRating(int starValue)
    {


        ratingList.Add(starValue);

        float average = 0;
        foreach (int rating in ratingList)
        {
            average += rating;
        }
        average /= ratingList.Count;
        ratingText.text = "Rating: " + average.ToString("0.0") + " / 5";

        //showcase new rating avarage
    }



    public void CreatePrefabObject(string feedbackText, int starCount)
    {
        FeedbackObject newFeedback = Instantiate(feedbackPrefab, canvas);
        newFeedback.SetupFeedback(feedbackText, starCount);


    }






    public int GetStarValue(float val)
    {
        if (val >= 0.8f)
        {
            return 5;
        }
        else if (val >= 0.6f)
        {
            return 4;
        }
        else if (val >= 0.4f)
        {
            return 3;
        }
        else if (val >= 0.2f)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }


}
