using UnityEngine;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists. 
    
public class GamerObject : MonoBehaviour
{

    public static GamerObject instance = null;              //Static instance of GamerObject which allows it to be accessed by any other script.
    public  string activeGamerId = "";
    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)           
            //if not, set instance to this
            instance = this;
            
        //If instance already exists and it's not this:
        else if (instance != this)              
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GamerObject.
            Destroy(gameObject);    
            
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);    
    }
}