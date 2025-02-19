using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public int specificSceneToLoad;


    public void SceneLoader(int sceneToLoad)
    {
        //Esta acción carga la escena del espacio igual al entero que indiquemos
        SceneManager.LoadScene(sceneToLoad);
    
    
    }

    public void SceneLoaderOnPlay()
    {

        SceneManager.LoadScene(specificSceneToLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    
    }




}
