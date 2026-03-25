using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    public CharacterData data;

    public void AssignData(CharacterData newData)
    {
        data = newData;
    }
    public float CheckCompability(Character character)
    {
        int totalMaxPoints = 0;
        int totalPoints = 0;

        CalculateScore(this.data, character.data, ref totalPoints, ref totalMaxPoints);
        Debug.Log("total points of the target"+ totalPoints+" max points of the target"+ totalMaxPoints);
        CalculateScore(character.data, this.data, ref totalPoints, ref totalMaxPoints);
        Debug.Log("total points of the target2 " + totalPoints + " max points of the target2 " + totalMaxPoints);




        return totalMaxPoints > 0 ? (float)totalPoints / totalMaxPoints : 0f;
    }

    private void CalculateScore(CharacterData evaluator, CharacterData subject, ref int points, ref int maxPoints)
    {
        evaluator.AddInterestsToTags();
        subject.AddInterestsToTags();
        foreach (string interest in evaluator.interests)
        {
            maxPoints += 10;
            if (subject.tags.Contains(interest))
            {
                points += 10;
                Debug.Log("Shared interest: " + interest + " (+" + 10 + " points)");
            }
        }

        foreach (Flag flag in evaluator.flags)
        {
            int scoreValue = (flag.type == Flag.FlagType.SoftGreen || flag.type == Flag.FlagType.SoftRed) ? 20 : 40;
            Debug.Log(flag.tag + " is a " + flag.type + " with search type " + flag.searchType + " and score value " + scoreValue);
            bool hasTag = subject.tags.Contains(flag.tag);
            bool isTriggered = (flag.searchType == Flag.SearchType.SearchFor && hasTag) ||
                               (flag.searchType == Flag.SearchType.SearchAgainst && !hasTag);

            if (isTriggered)
            {
                if (flag.type == Flag.FlagType.SoftRed || flag.type == Flag.FlagType.hardRed)
                {
                    points -= scoreValue;
                    Debug.Log("Flag triggered: " + flag.tag + " (-" + scoreValue + " points)");
                }
                else
                {
                    points += scoreValue;
                    maxPoints += scoreValue;
                    Debug.Log("Flag triggered: " + flag.tag + " (+" + scoreValue + " points)");


                }
            }
        }
    }
}

