using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager instance;
    public CharacterData tutorialTargetCharacter;
    public CharacterData[] tutorialCandidateCharacters;

    public CandidateCard candidatePrefab;
    public Transform candidateGrid;

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

    }

    }
