using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager
{
    #region Not-used Singleton Pattern
    //public static MusicManager instance;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }

    //    DontDestroyOnLoad(gameObject);
    //}
    #endregion

    // keep constructor private
    private MusicManager()
    {
    }

    static private MusicManager _instance;
    static public MusicManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new MusicManager();
            return _instance;
        }
    }

    public FMOD.Studio.EventInstance Music;

    public void CreateMusicEventInstance()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
        Debug.Log("Creating Music Event Instance");
    }
}
