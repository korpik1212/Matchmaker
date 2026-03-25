using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager instance;


    public CharacterData tutorialTargetCharacter;
    public CharacterData[] tutorialCandidateCharacters;

    public CandidateCard candidatePrefab;
    public Transform candidateGrid;



    public Transform canvas;
    public FeedbackObject feedbackPrefab;
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
        int starValue=GetStarValue(val);
        CreatePrefabObject(TargetInfo.instance.character.data.feedbackTexts[0], starValue);
        //instantiate feedback prefab 



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


    public void ChangeRating(int starValue)
    {

    }



    public void CreatePrefabObject(string feedbackText, int starCount)
    {
        FeedbackObject newFeedback = Instantiate(feedbackPrefab, canvas);
        newFeedback.SetupFeedback(feedbackText, starCount);


    }
    }
