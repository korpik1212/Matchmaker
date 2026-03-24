using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    public CharacterData data;

    public float CheckCompability(Character character)
    {
        int totalMaxPoints = 0;
        int totalPoints = 0;

        CalculateScore(this.data, character.data, ref totalPoints, ref totalMaxPoints);
        CalculateScore(character.data, this.data, ref totalPoints, ref totalMaxPoints);

        return totalMaxPoints > 0 ? (float)totalPoints / totalMaxPoints : 0f;
    }

    private void CalculateScore(CharacterData evaluator, CharacterData subject, ref int points, ref int maxPoints)
    {
        foreach (string interest in evaluator.interests)
        {
            maxPoints += 10;
            if (subject.tags.Contains(interest))
            {
                points += 10;
            }
        }

        foreach (Flag flag in evaluator.flags)
        {
            int scoreValue = (flag.type == Flag.FlagType.SoftGreen || flag.type == Flag.FlagType.SoftRed) ? 20 : 40;
            maxPoints += scoreValue;

            bool hasTag = subject.tags.Contains(flag.tag);
            bool isTriggered = (flag.searchType == Flag.SearchType.SearchFor && hasTag) ||
                               (flag.searchType == Flag.SearchType.SearchAgainst && !hasTag);

            if (isTriggered)
            {
                if (flag.type == Flag.FlagType.SoftRed || flag.type == Flag.FlagType.hardRed)
                {
                    points -= scoreValue;
                }
                else
                {
                    points += scoreValue;
                }
            }
        }
    }
}

public class Prompt
{
    public string promptText;
    public string answerText;
}

[System.Serializable]
public class Flag
{
    public enum FlagType
    {
        SoftGreen,
        HardGreen,
        SoftRed,
        hardRed
    }

    public enum SearchType
    {
        SearchFor,
        SearchAgainst
    }

    public FlagType type;
    public SearchType searchType;
    public string tag;
}