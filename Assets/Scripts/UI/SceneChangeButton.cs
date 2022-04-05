
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    public void ChangeScene( string scene )
    {
        SceneManager.SetActiveScene( SceneManager.GetSceneByName( scene ) );
    }

    public void ResetScene()
    {
        SceneManager.SetActiveScene( SceneManager.GetActiveScene() );
    }
}
