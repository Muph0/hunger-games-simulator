using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HungerGames.items
{
    public class EquipableItem : Item
    {
        public int EquipLayer;
        public EquipableItemSlot EquipSlot;

        public override Item Clone()
        {
            EquipableItem result = (EquipableItem)base.Clone();
            result.EquipLayer = this.EquipLayer;
            result.EquipSlot = this.EquipSlot;
            return result;
        }
    }

    public enum EquipableItemSlot
    {
        Head = 0,
        Body,
        Fists,
        InHands,
        Arms,
        Legs,
        Feet
    }

    public enum InjuryType
    {
        Stab = 1,
        Firegun = 2,
        BluntLight = 4,
        BluntHeavy = 8,
        SharpLight = 16,
        SharpHeavy = 32,
        StabRanged = 64,
    }
}
