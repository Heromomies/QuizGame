using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    #region Singleton
    public static Database instance;
    
    void Awake(){

        if (instance == null){

            instance = this;
            DontDestroyOnLoad(this.gameObject);
    
            //Rest of your Awake code
    
        } else {
            Destroy(this);
        }
    }
    #endregion

    public List<QuestionDatabase> questionDatabase;
}
