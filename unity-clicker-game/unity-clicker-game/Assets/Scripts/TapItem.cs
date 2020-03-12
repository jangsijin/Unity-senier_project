using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapItem : MonoBehaviour {

    public string StatType = "";

	public void onClickButton()
    {
        if(StatType == "CollectGold")
        {
            GameController.Instance.UpgradeCollectGold();
        }else if(StatType == "StatDamage")
        {
            GameController.Instance.UgradeDamage();
        }
        else if (StatType == "StatHealth")
        {
            GameController.Instance.UgradeHealth();
        }
    }
}
