using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Character Data", menuName = "Character Data", order = 1)]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite characterIcon;
    [TextArea(15, 20)]
    public string characterBio;
    public List<string> interests = new List<string>();
    public List<Prompt> promptList;
    public List<string> tags = new List<string>();
    public List<Flag> flags = new List<Flag>();
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