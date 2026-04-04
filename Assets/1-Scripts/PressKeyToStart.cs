using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PressKeyToStart : MonoBehaviour
{


    public List<GameObject> beginningObjects=new List<GameObject>();
    int currentPage = 0;
    private void Start()
    {
    }
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            BeginGame();
        }
    }

    public void BeginGame()
    {
        Progress();
    }

    public void Progress()
    {

        beginningObjects[currentPage].SetActive(false);
        currentPage++;
        if (currentPage >= beginningObjects.Count)
        {
            Invoke("StartGame", 0.1f);
            StartGame();
        }
        else
        {
            beginningObjects[currentPage].SetActive(true);
        }
    }
    private void StartGame()
    {
        SceneManager.LoadSceneAsync("GameplayScene");
        Debug.Log("Start triggered.");
    }
}
