using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private float _sceneToLoad;

    private void OnEnable()
    {
        DialogueController.onDialogueEnd += LoadNextScene;
    }

    private void OnDisable()
    {
        DialogueController.onDialogueEnd -= LoadNextScene;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
