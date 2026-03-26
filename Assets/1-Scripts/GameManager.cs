using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CandidateCard candidatePrefab;
    public Transform candidateGrid;

    public List<DayInfo> days;
    public int dayTime = 0;
    public int dayInexd = -1;

    public Transform canvas;
    public FeedbackObject feedbackPrefab;

    public List<CharacterData> allCandidates = new List<CharacterData>();
    public TextMeshProUGUI ratingText;
    public List<int> ratingList = new List<int>();



    public MatchAnimationSequence matchAnimationSequence;

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
        FinishDay();
    }

    public void OnTargetFinished()
    {
        dayTime++;

        if (dayTime >= days[dayInexd].targetDatas.Count)
        {
            FinishDay();
            AudioManager.instance.PlayAudio(AudioManager.instance.gameAudios.endOfDayAudio);

        }
        else
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.gameAudios.nextTargetAudio);

            TargetInfo.instance.ChangeCurrentData(days[dayInexd].targetDatas[dayTime]);
        }
    }

    public void FinishDay()
    {
        dayInexd++;
        dayTime = 0;

        if (dayInexd >= days.Count)
        {
            return;
        }

        TargetInfo.instance.ChangeCurrentData(days[dayInexd].targetDatas[dayTime]);

        for (int i = candidateGrid.childCount - 1; i >= 0; i--)
        {
            Destroy(candidateGrid.GetChild(i).gameObject);
        }

        DayInfo currentDay = days[dayInexd];
        List<CharacterData> dailyCandidates = new List<CharacterData>(currentDay.fixedCandidates);
        List<CharacterData> pool = new List<CharacterData>(allCandidates);

        foreach (CharacterData fixedCandidate in dailyCandidates)
        {
            pool.Remove(fixedCandidate);
        }

        int remainingCount = currentDay.candidateCount - dailyCandidates.Count;

        for (int i = 0; i < remainingCount; i++)
        {
            if (pool.Count == 0) break;
            int randIndex = Random.Range(0, pool.Count);
            dailyCandidates.Add(pool[randIndex]);
            pool.RemoveAt(randIndex);
        }

        for (int i = 0; i < dailyCandidates.Count; i++)
        {
            CharacterData temp = dailyCandidates[i];
            int randomIndex = Random.Range(i, dailyCandidates.Count);
            dailyCandidates[i] = dailyCandidates[randomIndex];
            dailyCandidates[randomIndex] = temp;
        }

        foreach (CharacterData candidateData in dailyCandidates)
        {
            CandidateCard newCandidate = Instantiate(candidatePrefab, candidateGrid);
            newCandidate.SetupCard(candidateData);
        }
    }

    public void Match()
    {
        if (CandidateClickInfo.instance.currentlySelectedCharacter == null) return;

        AudioManager.instance.PlayAudio(AudioManager.instance.gameAudios.buttonClickAudio);


        StartCoroutine(MatchLoop());
    }

    public IEnumerator MatchLoop()
    {
        float val = TargetInfo.instance.character.CheckCompability(CandidateClickInfo.instance.currentlySelectedCharacter);
        int starValue = GetStarValue(val);

        AudioManager.instance.PlayAudio(AudioManager.instance.gameAudios.matchAudio);
        matchAnimationSequence.PlayMatchAnimation(TargetInfo.instance.character.data, CandidateClickInfo.instance.currentlySelectedCharacter.data);

        Destroy(CandidateClickInfo.instance.currentlySelectedCharacter.gameObject);

        yield return new WaitForSeconds(4f);

        if (val > 0.5f)
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.gameAudios.goodMatchAudio);

        }
        else
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.gameAudios.badMatchAudio);

        }
        CreatePrefabObject("helo", starValue);
        ChangeRating(starValue);

        CandidateClickInfo.instance.ClearInfo();

        yield return new WaitForSeconds(2f);

        OnTargetFinished();
    }

    private float currentDisplayedAverage = 0f;

    public void ChangeRating(int starValue)
    {
        ratingList.Add(starValue);

        float newAverage = 0;
        foreach (int rating in ratingList)
        {
            newAverage += rating;
        }
        newAverage /= ratingList.Count;

        DOTween.To(() => currentDisplayedAverage, x =>
        {
            currentDisplayedAverage = x;
            ratingText.text = "Rating: " + currentDisplayedAverage.ToString("0.0") + " / 5";
        }, newAverage, 0.5f).SetEase(Ease.OutCubic);

        ratingText.transform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 5, 1).SetUpdate(true);
    }

    public void CreatePrefabObject(string feedbackText, int starCount)
    {
        FeedbackObject newFeedback = Instantiate(feedbackPrefab, canvas);
        newFeedback.SetupFeedback(feedbackText, starCount);
    }

    public int GetStarValue(float val)
    {
        if (val >= 0.8f) return 5;
        if (val >= 0.6f) return 4;
        if (val >= 0.4f) return 3;
        if (val >= 0.2f) return 2;
        return 1;
    }
}

[System.Serializable]
public class DayInfo
{
    public List<CharacterData> targetDatas = new List<CharacterData>();
    public List<CharacterData> fixedCandidates = new List<CharacterData>();
    public int candidateCount;
}