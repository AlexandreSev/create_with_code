using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterBarrelUpgrades : ShopUpgrade // INHERITANCE
{

    public BarrelGeneratorController controler;
    // Start is called before the first frame update
    public override void Upgrade() // POLYMORPHISM
    {
        controler.SpeedUp();
    }
}
