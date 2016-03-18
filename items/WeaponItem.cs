using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HungerGames.items
{
    public class WeaponItem : EquipableItem
    {
        public WeaponItem()
        {
            EquipSlot = EquipableItemSlot.InHands;
        }
        public InjuryType Injury;

        public virtual void Swing()
        {

        }
        public override Item Clone()
        {
            WeaponItem weap = (WeaponItem) base.Clone();
            weap.Injury = this.Injury;
            return weap;
        }
    }
}
