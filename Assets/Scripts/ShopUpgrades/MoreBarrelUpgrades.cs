using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreBarrelUpgrades : ShopUpgrade
{
    public BarrelGeneratorController generator;

    public override void Upgrade()
    {
        generator.MoreBarrel();
    }
}
