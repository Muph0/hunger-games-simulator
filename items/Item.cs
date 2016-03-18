using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HungerGames.items
{
    public class Item
    {
        public string Name, ID;
        public int MassInGrams, Damage;

        public static Dictionary<string, Item> ItemsByName { get; private set; }
        public static void LoadItems()
        {
            ItemsByName = new Dictionary<string, Item>();

            ItemsByName.Add(AppleRed.ID, AppleRed);
            ItemsByName.Add(AppleGreen.ID, AppleGreen);
            ItemsByName.Add(Plum.ID, Plum);
        }

        public static Item
            // FOOD
            Plum = new FruitConsumable() { Name = "Švestka", ID = "food.plum", Damage = 15, MassInGrams = 120 },
            AppleRed = new FruitConsumable() { Name = "Červené jablko", ID = "food.apple_red", Damage = 20, MassInGrams = 200 },
            AppleGreen = new FruitConsumable() { Name = "Zelené jablko", ID = "food.apple_green", Damage = 20, MassInGrams = 200 },
            
            // AMMO
            ArrowWood = new Item() { Name = "Dřevěný šíp", ID = "ammo.arrow_wood", Damage = 60, MassInGrams = 100},

            // MELEE
            TreeBranch = new WeaponItem() { Name = "Větev", ID = "weap.treebranch", Damage = 40, MassInGrams = 2300, Injury = InjuryType.BluntHeavy },
            TreeStick = new WeaponItem() { Name = "Klacek", ID = "weap.treestick", Damage = 10, MassInGrams = 1000, Injury = InjuryType.BluntLight | InjuryType.Stab },
            
            // RANGED
            BowWood = new RangedWeapon() { Name = "Dřevěný luk", ID = "weap.bow_wood", Damage = 80, MassInGrams = 1100, AmmoType = new string[] { ArrowWood.ID }, Injury = InjuryType.StabRanged, EquipLayer = 1 };

        public virtual string getMass()
        {
            return MassInGrams / 1000 + "." + (MassInGrams % 1000) / 10 + " kg";
        }
        public virtual string getFullName()
        {
            return "item." + Name;
        }
        public virtual Item Clone()
        {
            return new Item() { Name = this.Name, Damage = this.Damage, MassInGrams = this.MassInGrams };
        }
    }
}
