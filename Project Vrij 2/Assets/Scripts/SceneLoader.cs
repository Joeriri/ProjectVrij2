using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMainTitle()
    {
        SceneManager.LoadScene("MainTitle");
    }

    public void GoToPinBoard()
    {
        SceneManager.LoadScene("PinBoard");
    }

    public void GoToFoyer()
    {
        SceneManager.LoadScene("Foyer");
    }
}
