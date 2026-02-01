using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IfVictory : MonoBehaviour
{
    public int enemyKillMax = 20;
    private bool victory;
    //public Animator transition;
    //public Animator childTran;
    //public float transitionTime = 1f;

    private void Awake()
    {
        //transition = GetComponent<Animator>();
        //childTran = GetComponentInChildren<Animator>();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyManager.EnemyKilled == enemyKillMax)
        {
            Debug.Log("Victory!");
            SceneManager.LoadScene("VictoryScene");
            //StartCoroutine(LoadSceneCO());
            //GetComponentInChildren<Animator>().SetTrigger("Skip");
        }
    }


    //IEnumerator LoadSceneCO()
    //{
    //    //childTran.SetTrigger("Start");
    //    transition.SetTrigger("Skip");

    //    yield return new WaitForSeconds(transitionTime);

    //    //SceneManager.LoadScene();
    //}
}
