using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.factions
{
    public static class Modules
    {
        private static WeaponHash[] meleeGradeArms = {
            WeaponHash.Hammer,
            WeaponHash.Bat,
            WeaponHash.Crowbar,
            WeaponHash.Dagger,
            WeaponHash.Hatchet,
            WeaponHash.KnuckleDuster,
            WeaponHash.Machete,
            WeaponHash.SwitchBlade,
            WeaponHash.Wrench,
            WeaponHash.Poolcue,
            WeaponHash.Bottle,
            WeaponHash.Battleaxe,
            WeaponHash.Ball
        };

        private static WeaponHash[] lowGradeArms = {
            WeaponHash.Pistol,
            WeaponHash.CombatPistol,
            WeaponHash.Pistol50,
            WeaponHash.SNSPistol,
            WeaponHash.HeavyPistol,
            WeaponHash.APPistol,
            WeaponHash.MicroSMG,
            WeaponHash.MachinePistol,
            WeaponHash.SMG,
            WeaponHash.DoubleBarrelShotgun
        };

        private static WeaponHash[] mediumGradeArms =
        {
            WeaponHash.Revolver,
            WeaponHash.CombatPDW,
            WeaponHash.MiniSMG,
            WeaponHash.AssaultRifle,
            WeaponHash.CompactRifle,
            WeaponHash.CarbineRifle,
            WeaponHash.SawnoffShotgun,
            WeaponHash.PumpShotgun
        };

        private static WeaponHash[] highGradeArms =
        {
            WeaponHash.BullpupShotgun,
            WeaponHash.AssaultShotgun,
            WeaponHash.HeavyShotgun,
            WeaponHash.Autoshotgun,
            WeaponHash.AdvancedRifle,
            WeaponHash.SpecialCarbine,
            WeaponHash.BullpupShotgun,
            WeaponHash.Gusenberg,
            WeaponHash.CombatMG,
            WeaponHash.MG,
            WeaponHash.AssaultSMG
        };

        private static WeaponHash[] explosiveGradeArms =
        {
            WeaponHash.Grenade,
            WeaponHash.RPG,
            WeaponHash.GrenadeLauncher,
            WeaponHash.GrenadeLauncherSmoke,
            WeaponHash.SmokeGrenade,
            WeaponHash.CompactLauncher,
            WeaponHash.Firework,
            WeaponHash.StickyBomb,
            WeaponHash.Molotov,
            WeaponHash.Pipebomb
        };

        public static bool isValidArmsForFaction(Client player, FactionInfo info)
        {
            switch (info.ArmsModule)
            {
                case FactionInfo.WeaponModule.Melee:
                    if (meleeGradeArms.Contains(player.currentWeapon))
                    {
                        return true;
                    }
                    return false;
                case FactionInfo.WeaponModule.LowGradeArms:
                    if (meleeGradeArms.Contains(player.currentWeapon) 
                        || lowGradeArms.Contains(player.currentWeapon))
                    {
                        return true;
                    }
                    return false;
                case FactionInfo.WeaponModule.MediumGradeArms:
                    if (meleeGradeArms.Contains(player.currentWeapon) 
                        || lowGradeArms.Contains(player.currentWeapon) 
                        || mediumGradeArms.Contains(player.currentWeapon))
                    {
                        return true;
                    }
                    return false;
                case FactionInfo.WeaponModule.HighGradeArms:
                    if (meleeGradeArms.Contains(player.currentWeapon) 
                        || lowGradeArms.Contains(player.currentWeapon) 
                        || mediumGradeArms.Contains(player.currentWeapon) 
                        || highGradeArms.Contains(player.currentWeapon))
                    {
                        return true;
                    }
                    return false;
                case FactionInfo.WeaponModule.ExplosiveArms:
                    if (meleeGradeArms.Contains(player.currentWeapon) 
                        || lowGradeArms.Contains(player.currentWeapon) 
                        || mediumGradeArms.Contains(player.currentWeapon) 
                        || highGradeArms.Contains(player.currentWeapon) 
                        || explosiveGradeArms.Contains(player.currentWeapon))
                    {
                        return true;
                    }
                    return false;
            }
            return false;
        }
    }
}
