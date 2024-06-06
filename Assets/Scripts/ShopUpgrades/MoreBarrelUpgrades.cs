using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreBarrelUpgrades : ShopUpgrade // INHERITANCE
{
    public BarrelGeneratorController generator;

    public override void Upgrade() // POLYMORPHISM
    {
        generator.MoreBarrel();
    }
}
