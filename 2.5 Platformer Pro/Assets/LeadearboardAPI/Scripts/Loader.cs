using UnityEngine;
    public class Loader : MonoBehaviour 
    {
        public GameObject gamerObject;          //GamerObject prefab to instantiate.
        
        
        void Awake ()
        {
            //Check if a GamerObject has already been assigned to static variable GamerObject.instance or if it's still null
            if (GamerObject.instance == null)
                
                //Instantiate GamerObject prefab
                Instantiate(gamerObject);
        }
    }