using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.datahandles
{
    public class Clothing : Script
    {
        Database db = new Database();

        private Client client;
        private Player player;
        private int mask;
        private int maskVariant;
        private int torso;
        private int torsoVariant;
        private int legs;
        private int legsVariant;
        private int bags;
        private int feet;
        private int feetVariant;
        private int accessories;
        private int undershirt;
        private int undershirtVariant;
        private int bodyArmor;
        private int bodyArmorVariant;
        private int top;
        private int topVariant;

        public Clothing() { }

        public Clothing(Client p, Player pClass)
        {
            client = p;
            player = pClass;
            mask = 0;
            maskVariant = 0;
            torso = 0;
            torsoVariant = 0;
            legs = 0;
            legsVariant = 0;
            bags = 0;
            feet = 0;
            feetVariant = 0;
            accessories = 0;
            undershirt = 0;
            undershirtVariant = 0;
            bodyArmor = 0;
            bodyArmorVariant = 0;
            top = 0;
            topVariant = 0;
        }

        public void savePlayerClothes()
        {
            string before = "UPDATE Clothing SET";
            string[] varNames = { "Mask", "MaskVariant", "Torso", "TorsoVariant", "Legs", "LegsVariant", "Bag", "Feet", "FeetVariant", "Accessories", "Undershirt", "UndershirtVariant", "BodyArmor", "BodyArmorVariant", "Top", "TopVariant" };
            string after = string.Format("WHERE Owner='{0}'", player.ID);
            object[] args = { mask, maskVariant, torso, torsoVariant, legs, legsVariant, bags, feet, feetVariant, accessories, undershirt, undershirtVariant, bodyArmor, bodyArmorVariant, top, topVariant };
            db.compileQuery(before, after, varNames, args);
        }

        public void loadPlayerClothes()
        {
            /** Pull from the database. */
            string[] varNames = { "Owner" };
            string before = "SELECT * FROM Clothing WHERE";
            object[] data = { player.ID };
            DataTable result = db.compileSelectQuery(before, varNames, data);
            DataRow clothing = result.Rows[0];

            Mask = Convert.ToInt32(clothing["Mask"]);
            MaskVariant = Convert.ToInt32(clothing["MaskVariant"]);
            Torso = Convert.ToInt32(clothing["Torso"]);
            TorsoVariant = Convert.ToInt32(clothing["TorsoVariant"]);
            Legs = Convert.ToInt32(clothing["Legs"]);
            LegsVariant = Convert.ToInt32(clothing["LegsVariant"]);
            Bag = Convert.ToInt32(clothing["Bag"]);
            Feet = Convert.ToInt32(clothing["Feet"]);
            FeetVariant = Convert.ToInt32(clothing["FeetVariant"]);
            Accessories = Convert.ToInt32(clothing["Accessories"]);
            Undershirt = Convert.ToInt32(clothing["Undershirt"]);
            UndershirtVariant = Convert.ToInt32(clothing["UndershirtVariant"]);
            BodyArmor = Convert.ToInt32(clothing["BodyArmor"]);
            BodyArmorVariant = Convert.ToInt32(clothing["BodyArmorVariant"]);
            Top = Convert.ToInt32(clothing["Top"]);
            TopVariant = Convert.ToInt32(clothing["TopVariant"]);
        }

        public void updatePlayerClothes()
        {
            // Mask
            API.setPlayerClothes(client, 1, mask, maskVariant);
            API.setEntitySyncedData(client, "ESS_Mask", mask);
            API.setEntitySyncedData(client, "ESS_MaskVariant", maskVariant);
            // Torso
            API.setPlayerClothes(client, 3, torso, torsoVariant);
            API.setEntitySyncedData(client, "ESS_Torso", torso);
            API.setEntitySyncedData(client, "ESS_TorsoVariant", torsoVariant);
            // Legs
            API.setPlayerClothes(client, 4, legs, legsVariant);
            API.setEntitySyncedData(client, "ESS_Legs", legs);
            API.setEntitySyncedData(client, "ESS_LegsVariant", legsVariant);
            // Bag
            API.setPlayerClothes(client, 5, bags, 0);
            API.setEntitySyncedData(client, "ESS_Bag", bags);
            // Feet
            API.setPlayerClothes(client, 6, feet, feetVariant);
            API.setEntitySyncedData(client, "ESS_Feet", feet);
            API.setEntitySyncedData(client, "ESS_FeetVariant", feetVariant);
            // Accessories
            API.setPlayerClothes(client, 7, accessories, 0);
            API.setEntitySyncedData(client, "ESS_Accessories", accessories);
            // Undershirt
            API.setPlayerClothes(client, 8, undershirt, undershirtVariant);
            API.setEntitySyncedData(client, "ESS_Undershirt", undershirt);
            API.setEntitySyncedData(client, "ESS_UndershirtVariant", undershirtVariant);
            // BodyArmor
            API.setPlayerClothes(client, 9, bodyArmor, bodyArmorVariant);
            API.setEntitySyncedData(client, "ESS_BodyArmor", bodyArmor);
            API.setEntitySyncedData(client, "ESS_BodyArmorVariant", bodyArmorVariant);
            // Top
            API.setPlayerClothes(client, 11, top, topVariant);
            API.setEntitySyncedData(client, "ESS_Top", top);
            API.setEntitySyncedData(client, "ESS_TopVariant", topVariant);
        }

        public int Mask
        {
            set
            {
                mask = value;
                updatePlayerClothes();
            }
            get
            {
                return mask;
            }
        }
        public int MaskVariant
        {
            set
            {
                maskVariant = value;
                updatePlayerClothes();
            }
            get
            {
                return maskVariant;
            }
        }
        public int Torso
        {
            set
            {
                torso = value;
                updatePlayerClothes();
            }
            get
            {
                return torso;
            }
        }
        public int TorsoVariant
        {
            set
            {
                torsoVariant = value;
                updatePlayerClothes();
            }
            get
            {
                return torsoVariant;
            }
        }
        public int Legs
        {
            set
            {
                legs = value;
                updatePlayerClothes();
            }
            get
            {
                return legs;
            }
        }
        public int LegsVariant
        {
            set
            {
                legsVariant = value;
                updatePlayerClothes();
            }
            get
            {
                return legsVariant;
            }
        }
        public int Bag
        {
            set
            {
                bags = value;
                updatePlayerClothes();
            }
            get
            {
                return bags;
            }
        }
        public int Feet
        {
            set
            {
                feet = value;
                updatePlayerClothes();
            }
            get
            {
                return feet;
            }
        }
        public int FeetVariant
        {
            set
            {
                feetVariant = value;
                updatePlayerClothes();
            }
            get
            {
                return feetVariant;
            }
        }
        public int Accessories
        {
            set
            {
                accessories = value;
                updatePlayerClothes();
            }
            get
            {
                return accessories;
            }
        }
        public int Undershirt
        {
            set
            {
                undershirt = value;
                updatePlayerClothes();
            }
            get
            {
                return undershirt;
            }
        }
        public int UndershirtVariant
        {
            set
            {
                undershirtVariant = value;
                updatePlayerClothes();
            }
            get
            {
                return undershirtVariant;
            }
        }
        public int BodyArmor
        {
            set
            {
                bodyArmor = value;
                updatePlayerClothes();
            }
            get
            {
                return bodyArmor;
            }
        }
        public int BodyArmorVariant
        {
            set
            {
                bodyArmorVariant = value;
                updatePlayerClothes();
            }
            get
            {
                return bodyArmorVariant;
            }
        }
        public int Top
        {
            set
            {
                top = value;
                updatePlayerClothes();
            }
            get
            {
                return top;
            }
        }
        public int TopVariant
        {
            set
            {
                topVariant = value;
                updatePlayerClothes();
            }
            get
            {
                return top;
            }
        }
    }
}
