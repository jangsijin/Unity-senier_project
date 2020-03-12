using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat{

    //
    public string Type = "";
    //이름
    public string Name = "";
    //가격
    public int Cost = 1;

    public int CostUpgradeAmount = 2;

    public string CostUpgradeAmountOp = "*";

    public int UpgradeAmount = 2;

    public string UpgradeAmountOp = "*";

}
