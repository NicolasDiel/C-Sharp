using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{

    [SerializeField] private Image progressBar;

    // Start is called before the first frame update
    public void LoadScene()
    {
        //start async operation
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        //create an async operation
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(1);
        
        while (gameLevel.progress < 1)
        {
            // take the progress bar fill = async operation progress
            progressBar.fillAmount = gameLevel.progress;

            yield return new WaitForEndOfFrame();
        }
    }
}
