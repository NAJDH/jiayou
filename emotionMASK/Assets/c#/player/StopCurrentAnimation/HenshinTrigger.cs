using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenshinTrigger : MonoBehaviour
{
    //player player => GetComponentInParent<player>();

    public void EffectDisappared()
    {
        //player.MakeHenshinTriggerDisActive();
    }

    private void Start()
    {
        this.GetComponent<Animator>().SetTrigger("henshin");
        
        Destroy(gameObject, 1f);
    }

}
