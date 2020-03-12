using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Monster{

    public int ID = 0;
    //
    public string Type = "";
    //이름
    public string Name = "";

    public string Prefab = "Monter/Slime";

    public int Damage = 1;

    public int Defense = 1;

    public int Health = 1;

    public int Exp = 1;

}
