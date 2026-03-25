using UnityEngine;

public class TargetInfo : MonoBehaviour
{
    public static TargetInfo targetInfo;
    [HideInInspector]
    public Character character;

    private void Awake()
    {
        if (targetInfo == null)
        {
            targetInfo = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        character = GetComponent<Character>();
    }


    public void AssignCharacterData(CharacterData data)
    {
        character.AssignData(data);
        SetupCharacterInfo();
    }


    public void SetupCharacterInfo()
    {
        //create the infos here now 
    }
}
