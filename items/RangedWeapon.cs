using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HungerGames.items
{
    class RangedWeapon : WeaponItem
    {
        public string[] AmmoType;

        public override Item Clone()
        {
            RangedWeapon weap = (RangedWeapon)base.Clone();
            weap.AmmoType = this.AmmoType;
            return weap;
        }
    }
}
