using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PressKeyToStart : MonoBehaviour
{

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        SceneManager.LoadSceneAsync("GameplayScene");
        Debug.Log("Start triggered.");
    }
}
