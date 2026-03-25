using UnityEngine;

public class CandidateClickInfo : MonoBehaviour
{
  
    public static CandidateClickInfo instance;

    public Character currentlySelectedCharacter;
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

    public void ChangeCurrentData(Character character)
    {
        currentlySelectedCharacter = character;
    }
}
