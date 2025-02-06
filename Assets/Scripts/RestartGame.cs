using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //call RestartScene
            RestartScene();
        }
    }

    public void RestartScene()
    {
        //get current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        //reload scene
        SceneManager.LoadScene(currentSceneIndex);
    }
}
