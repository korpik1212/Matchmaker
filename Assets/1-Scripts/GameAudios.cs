using UnityEngine;


[CreateAssetMenu(fileName = "GameAudios", menuName = "ScriptableObjects/GameAudios", order = 1)]
public class GameAudios : ScriptableObject
{
    /// Audio
    public AudioClip matchAudio; 
    public AudioClip goodMatchAudio;
    public AudioClip badMatchAudio;
    public AudioClip badMatchAudio2;
    public AudioClip nextTargetAudio;
    public AudioClip buttonClickAudio;
    public AudioClip endOfDayAudio;
    public AudioClip mouseClickAudio;
    public AudioClip startGameClickAudio;
    public AudioClip startGameClickAudio2;
}
