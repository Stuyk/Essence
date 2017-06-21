using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Essence.classes
{
    public class Skin : Script
    {
        Database db = new Database();

        private Client client;
        private Player player;
        private int father;
        private int mother;
        private int fatherSkin;
        private int motherSkin;
        private float faceBlend;
        private float skinBlend;
        private int hair;
        private int hairColor;
        private int hairHighlight;
        private int blemishes;
        private int facialHair;
        private int eyebrows;
        private int ageing;
        private int makeup;
        private int blush;
        private int complexion;
        private int sunDamage;
        private int lipstick;
        private int moles;
        private int chestHair;
        private int bodyBlemishes;
        private int eyeColor;
        private int lipstickColor;
        private int lipstickColor2;
        private int makeupColor;
        private int makeupColor2;
        private float[] faceList;

        public int Father
        {
            set
            {
                father = value;
                API.setEntitySyncedData(client, "ESS_Father", value);
            }
            get
            {
                return father;
                
            }
        }
        public int Mother
        {
            get
            {
                return mother;
            }
            set
            {
                mother = value;
                API.setEntitySyncedData(client, "ESS_Mother", value);
            }
        }
        public int FatherSkin
        {
            get
            {
                return fatherSkin;
            }
            set
            {
                fatherSkin = value;
                API.setEntitySyncedData(client, "ESS_FatherSkin", value);
            }
        }
        public int MotherSkin
        {
            get
            {
                return motherSkin;
            }
            set
            {
                motherSkin = value;
                API.setEntitySyncedData(client, "ESS_MotherSkin", value);
            }
        }
        public float FaceBlend
        {
            get
            {
                return faceBlend;
            }
            set
            {
                faceBlend = value;
                API.setEntitySyncedData(client, "ESS_FaceBlend", value);
            }
        }
        public float SkinBlend
        {
            get
            {
                return skinBlend;
            }
            set
            {
                skinBlend = value;
                API.setEntitySyncedData(client, "ESS_SkinBlend", value);
            }
        }
        public int Hair
        {
            get
            {
                return hair;
            }
            set
            {
                hair = value;
                API.setEntitySyncedData(client, "ESS_Hair", value);
            }
        }
        public int HairColor
        {
            get
            {
                return hairColor;
            }
            set
            {
                hairColor = value;
                API.setEntitySyncedData(client, "ESS_HairColor", value);
            }
        }
        public int HairHighlight
        {
            get
            {
                return hairHighlight;
            }
            set
            {
                hairHighlight = value;
                API.setEntitySyncedData(client, "ESS_HairHighlight", value);
            }
        }
        public int Blemishes
        {
            get
            {
                return blemishes;
            }
            set
            {
                blemishes = value;
                API.setEntitySyncedData(client, "ESS_Blemishes", value);
            }
        }
        public int FacialHair
        {
            get
            {
                return facialHair;
            }
            set
            {
                facialHair = value;
                API.setEntitySyncedData(client, "ESS_FacialHair", value);
            }
        }
        public int Eyebrows
        {
            get
            {
                return eyebrows;
            }
            set
            {
                eyebrows = value;
                API.setEntitySyncedData(client, "ESS_Eyebrows", value);
            }
        }
        public int Ageing
        {
            get
            {
                return ageing;
            }
            set
            {
                ageing = value;
                API.setEntitySyncedData(client, "ESS_Ageing", value);
            }
        }
        public int Makeup
        {
            get
            {
                return makeup;
            }
            set
            {
                makeup = value;
                API.setEntitySyncedData(client, "ESS_Makeup", value);
            }
        }
        public int Blush
        {
            get
            {
                return blush;
            }
            set
            {
                blush = value;
                API.setEntitySyncedData(client, "ESS_Blush", value);
            }
        }
        public int Complexion
        {
            get
            {
                return complexion;
            }
            set
            {
                complexion = value;
                API.setEntitySyncedData(client, "ESS_Complexion", value);
            }
        }
        public int SunDamage
        {
            get
            {
                return sunDamage;
            }
            set
            {
                sunDamage = value;
                API.setEntitySyncedData(client, "ESS_SunDamage", value);
            }
        }
        public int Lipstick
        {
            get
            {
                return lipstick;
            }
            set
            {
                lipstick = value;
                API.setEntitySyncedData(client, "ESS_Lipstick", value);
            }
        }
        public int Moles
        {
            get
            {
                return moles;
            }
            set
            {
                moles = value;
                API.setEntitySyncedData(client, "ESS_Moles", value);
            }
        }
        public int ChestHair
        {
            get
            {
                return chestHair;
            }
            set
            {
                chestHair = value;
                API.setEntitySyncedData(client, "ESS_ChestHair", value);
            }
        }
        public int BodyBlemishes
        {
            get
            {
                return bodyBlemishes;
            }
            set
            {
                bodyBlemishes = value;
                API.setEntitySyncedData(client, "ESS_BodyBlemishes", value);
            }
        }
        public int EyeColor
        {
            get
            {
                return eyeColor;
            }
            set
            {
                eyeColor = value;
                API.setEntitySyncedData(client, "ESS_EyeColor", value);
            }
        }
        public int LipstickColor
        {
            get
            {
                return lipstickColor;
            }
            set
            {
                lipstickColor = value;
                API.setEntitySyncedData(client, "ESS_LipstickColor", value);
            }
        }
        public int LipstickColor2
        {
            get
            {
                return lipstickColor2;
            }
            set
            {
                lipstickColor2 = value;
                API.setEntitySyncedData(client, "ESS_LipstickColor2", value);
            }
        }
        public int MakeupColor
        {
            get
            {
                return lipstickColor;
            }
            set
            {
                lipstickColor = value;
                API.setEntitySyncedData(client, "ESS_MakeupColor", value);
            }
        }
        public int MakeupColor2
        {
            get
            {
                return lipstickColor2;
            }
            set
            {
                lipstickColor2 = value;
                API.setEntitySyncedData(client, "ESS_MakeupColor2", lipstickColor2);
            }
        }
        public float[] FaceList
        {
            get
            {
                return faceList;
            }
            set
            {
                faceList = value;
                API.setEntitySyncedData(client, "ESS_FaceList", faceList);
            }
        }

        public Skin() { }

        public Skin(Client p, Player pClass)
        {
            client = p;
            player = pClass;
            Mother = 0;
            father = 0;
            motherSkin = 0;
            fatherSkin = 0;
            faceBlend = 0;
            skinBlend = 0;
            hair = 0;
            hairColor = 0;
            hairHighlight = 0;
            blemishes = 0;
            facialHair = 0;
            eyebrows = 0;
            ageing = 0;
            makeup = 0;
            blush = 0;
            complexion = 0;
            sunDamage = 0;
            lipstick = 0;
            moles = 0;
            chestHair = 0;
            bodyBlemishes = 0;
            eyeColor = 0;
            lipstickColor = 0;
            lipstickColor2 = 0;
            makeupColor = 0;
            makeupColor2 = 0;
            faceList = new float[21];
        }

        public void savePlayerFace()
        {
            string before = "UPDATE Skin SET";
            string[] varNames = { "Mother", "Father", "MotherSkin", "FatherSkin", "FaceBlend", "SkinBlend", "Hair", "HairColor", "HairHighlight", "Blemishes", "FacialHair", "Eyebrows", "Ageing", "Makeup", "Blush", "Complexion", "SunDamage", "Lipstick", "Moles", "ChestHair", "BodyBlemishes", "EyeColor", "LipstickColor", "LipstickColor2", "MakeupColor", "MakeupColor2", "Face0", "Face1", "Face2", "Face3", "Face4", "Face5", "Face6", "Face7", "Face8", "Face9", "Face10", "Face11", "Face12", "Face13", "Face14", "Face15", "Face16", "Face17", "Face18", "Face19", "Face20" };
            string after = string.Format("WHERE Owner='{0}'", player.ID);
            object[] args = { mother, father, motherSkin, fatherSkin, faceBlend, skinBlend, hair, hairColor, hairHighlight, blemishes, facialHair, eyebrows, ageing, makeup, blush, complexion, sunDamage, lipstick, moles, chestHair, bodyBlemishes, eyeColor, lipstickColor, lipstickColor2, makeupColor, makeupColor2, faceList[0], faceList[1], faceList[2], faceList[3], faceList[4], faceList[5], faceList[6], faceList[7], faceList[8], faceList[9], faceList[10], faceList[11], faceList[12], faceList[13], faceList[14], faceList[15], faceList[16], faceList[17], faceList[18], faceList[19], faceList[20] };
            db.compileQuery(before, after, varNames, args);
        }

        public void loadPlayerFace()
        {
            /** Pull from the database. */
            string[] varNames = { "Owner" };
            string before = "SELECT * FROM Skin WHERE";
            object[] data = { player.ID };
            DataTable result = db.compileSelectQuery(before, varNames, data);
            DataRow face = result.Rows[0];

            Father = Convert.ToInt32(face["Father"]);
            Mother = Convert.ToInt32(face["Mother"]);
            MotherSkin = Convert.ToInt32(face["MotherSkin"]);
            FatherSkin = Convert.ToInt32(face["FatherSkin"]);
            FaceBlend = Convert.ToSingle(face["FaceBlend"]);
            SkinBlend = Convert.ToSingle(face["SkinBlend"]);
            Hair = Convert.ToInt32(face["Hair"]);
            HairColor = Convert.ToInt32(face["HairColor"]);
            HairHighlight = Convert.ToInt32(face["HairHighlight"]);
            Blemishes = Convert.ToInt32(face["Blemishes"]);
            FacialHair = Convert.ToInt32(face["FacialHair"]);
            Eyebrows = Convert.ToInt32(face["Eyebrows"]);
            Ageing = Convert.ToInt32(face["Ageing"]);
            Makeup = Convert.ToInt32(face["Makeup"]);
            Blush = Convert.ToInt32(face["Blush"]);
            Complexion = Convert.ToInt32(face["Complexion"]);
            SunDamage = Convert.ToInt32(face["SunDamage"]);
            Lipstick = Convert.ToInt32(face["Lipstick"]);
            Moles = Convert.ToInt32(face["Moles"]);
            ChestHair = Convert.ToInt32(face["ChestHair"]);
            BodyBlemishes = Convert.ToInt32(face["BodyBlemishes"]);
            EyeColor = Convert.ToInt32(face["EyeColor"]);
            LipstickColor = Convert.ToInt32(face["LipstickColor"]);
            LipstickColor2 = Convert.ToInt32(face["LipstickColor2"]);
            MakeupColor = Convert.ToInt32(face["MakeupColor"]);
            MakeupColor2 = Convert.ToInt32(face["MakeupColor2"]);
            faceList[0] = Convert.ToSingle(face["Face0"]);
            faceList[1] = Convert.ToSingle(face["Face1"]);
            faceList[2] = Convert.ToSingle(face["Face2"]);
            faceList[3] = Convert.ToSingle(face["Face3"]);
            faceList[4] = Convert.ToSingle(face["Face4"]);
            faceList[5] = Convert.ToSingle(face["Face5"]);
            faceList[6] = Convert.ToSingle(face["Face6"]);
            faceList[7] = Convert.ToSingle(face["Face7"]);
            faceList[8] = Convert.ToSingle(face["Face8"]);
            faceList[9] = Convert.ToSingle(face["Face9"]);
            faceList[10] = Convert.ToSingle(face["Face10"]);
            faceList[11] = Convert.ToSingle(face["Face11"]);
            faceList[12] = Convert.ToSingle(face["Face12"]);
            faceList[13] = Convert.ToSingle(face["Face13"]);
            faceList[14] = Convert.ToSingle(face["Face14"]);
            faceList[15] = Convert.ToSingle(face["Face15"]);
            faceList[16] = Convert.ToSingle(face["Face16"]);
            faceList[17] = Convert.ToSingle(face["Face17"]);
            faceList[18] = Convert.ToSingle(face["Face18"]);
            faceList[19] = Convert.ToSingle(face["Face19"]);
            faceList[20] = Convert.ToSingle(face["Face20"]);

            API.setEntitySyncedData(client, "ESS_FaceList", FaceList);

            int gender = Convert.ToInt32(face["Model"]);

            if (gender == 0)
            {
                API.setPlayerSkin(client, PedHash.FreemodeMale01);
            }
            else
            {
                API.setPlayerSkin(client, PedHash.FreemodeFemale01);
            }
        }

        public void updatePlayerFace()
        {
            if (Hair == 23 || Hair == 24)
            {
                Hair = 0;
            }

            //API.setPlayerClothes(client, 2, Hair, 0);
            API.triggerClientEventForAll("ESS_SKIN_UPDATE", client);
        }
    }

    public class Clothing : Script
    {
        Database db = new Database();

        private Client client;
        private Player player;
        private int mask;
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
            string[] varNames = { "Mask", "Torso", "TorsoVariant", "Legs", "LegsVariant", "Bag", "Feet", "FeetVariant", "Accessories", "Undershirt", "UndershirtVariant", "BodyArmor", "BodyArmorVariant", "Top", "TopVariant" };
            string after = string.Format("WHERE Owner='{0}'", player.ID);
            object[] args = { mask, torso, torsoVariant, legs, legsVariant, bags, feet, feetVariant, accessories, undershirt, undershirtVariant, bodyArmor, bodyArmorVariant, top, topVariant };
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
            API.setPlayerClothes(client, 1, mask, 0);
            API.setEntitySyncedData(client, "ESS_Mask", mask);
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

    public class Player : Script
    {
        Database db = new Database();

        private int id;
        private int money;
        private int bank;
        private Vector3 lastPosition;
        private Client player;
        private Clothing playerClothing;
        private Skin playerSkin;

        private List<Vehicle> vehicles;

        // Return Player Vehicles
        public List<Vehicle> PlayerVehicles
        {
            get
            {
                return vehicles;
            }
        }

        // Active player client.
        public Client PlayerClient
        {
            set
            {
                player = value;
            }
            get
            {
                return player;
            }
        }

        //Player ID Number from Database
        public int ID
        {
            set
            {
                id = value;
            }
            get
            {
                return id;
            }
        }

        //Player Money Number from Database
        public int Money
        {
            set
            {
                money = value;
                API.setEntitySyncedData(PlayerClient, "ESS_Money", money);
                updatePlayerMoney();
            }
            get
            {
                return money;
            }
        }

        //Player Bank Number from Database
        public int Bank
        {
            set
            {
                bank = value;
            }
            get
            {
                return bank;
            }
        }

        //Player Last Position from Database
        public Vector3 LastPosition
        {
            set
            {
                lastPosition = value;
            }
            get
            {
                return lastPosition;
            }
        }

        public Clothing PlayerClothing
        {
            get
            {
                return playerClothing;
            }
        }

        public Player() { }

        public Player(Client player, DataRow db)
        {
            // Pass the login to the console.
            API.consoleOutput(string.Format("{0} has successfully logged in.", player.name));
            // Used for drawing hud items -->
            API.setEntitySyncedData(player, "ESS_LoggedIn", true);
            // Setup Class Data.
            API.setEntityData(player, "Instance", this);
            PlayerClient = player;
            ID = Convert.ToInt32(db["ID"]);
            Bank = Convert.ToInt32(db["Bank"]);
            Money = Convert.ToInt32(db["Money"]);
            LastPosition = new Vector3(Convert.ToSingle(db["X"]), Convert.ToSingle(db["Y"]), Convert.ToSingle(db["Z"]));
            // Don't move on until the players dimension is ready.

            // Make our player controllable again.
            API.freezePlayer(player, false);
            API.setEntityTransparency(player, 255);
            API.setEntityPosition(player, LastPosition);

            // Setup Player Vehicles
            createPlayerVehicles();

            // Setup Player Skin
            playerSkin = new Skin(player, this);
            playerSkin.loadPlayerFace();
            playerSkin.updatePlayerFace();

            // Setup Player Clothing
            playerClothing = new Clothing(player, this);
            playerClothing.loadPlayerClothes();
            playerClothing.updatePlayerClothes();

            // Set our entity dimension.
            API.setEntityDimension(player, 0);
        }

        /** Spawn Player Vehicles */
        public void createPlayerVehicles()
        {
            vehicles = new List<Vehicle>();

            string[] varNames = { "Owner" };
            string before = "SELECT * FROM Vehicles WHERE";
            object[] data = { ID };
            DataTable result = db.compileSelectQuery(before, varNames, data);

            if (result.Rows.Count < 1)
            {
                return;
            }

            foreach (DataRow row in result.Rows)
            {
                Vehicle vehInfo = new Vehicle(row);
                vehicles.Add(vehInfo);
            }
        }

        /** Remove Player Vehicles **/
        public void removePlayerVehicles()
        {
            if (vehicles.Count <= 0)
            {
                return;
            }

            foreach (Vehicle vehInfo in vehicles)
            {
                // Update vehicle position before deleting.
                vehInfo.LastPosition = API.getEntityPosition(vehInfo.Handle);
                // Delete the entity.
                API.deleteEntity(vehInfo.Handle);
            }
        }

        /** Deposit money into the player's bank. */
        public void depositMoney(int amount)
        {
            Bank += amount;
            Money -= amount;

            updatePlayerMoney();
        }

        /** Withdraw money from the player's bank. */
        public void withdrawMoney(int amount)
        {
            Bank -= amount;
            Money += amount;

            updatePlayerMoney();
        }

        private void updatePlayerMoney()
        {
            string before = "UPDATE Players SET";
            string[] varNames = { "Money", "Bank" };
            string after = string.Format("WHERE Id='{0}'", ID);
            object[] args = { Money, Bank };
            db.compileQuery(before, after, varNames, args);
        }

    }
}
