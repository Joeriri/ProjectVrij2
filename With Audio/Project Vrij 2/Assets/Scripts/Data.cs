using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Source: https://answers.unity.com/questions/1224993/how-to-save-variables-between-scenes.html

public class Data
{
    // keep constructor private
    private Data()
    {
    }

    static private Data _instance;
    static public Data instance
    {
        get
        {
            if (_instance == null)
                _instance = new Data();
            return _instance;
        }
    }

    // This stack keeps track of all the scenes the player has been in, so we can go back to previous scenes.
    // Source: https://answers.unity.com/questions/1617291/how-i-can-open-a-previous-scene-with-button-back.html
    public Stack<string> loadedScenes = new Stack<string>();
}
