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
    public class Skin : Script
    {
        Database db = new Database();

        //surgeonshit
        private Client client;
        private Player player;
        private int father;
        private int mother;
        private int fatherSkin;
        private int motherSkin;
        private float faceBlend;
        private float skinBlend;
        private int blemishes;
        private int ageing;
        private int complexion;
        private int sunDamage;
        private int moles;
        private int bodyBlemishes;
        //end surgeon

        //barber shit
        private int hair;
        private int hairColor;
        private int hairHighlight;
        private int facialHair;
        private int facialHairColor;
        private float facialHairOpacity;
        private int eyebrows;
        private int eyebrowsColor;
        private float eyebrowsOpacity;
        private int makeup;
        private float makeupOpacity;
        private int facepaint;
        private float facepaintOpacity;
        private int lipstick;
        private int lipstickColor;
        private float lipstickOpacity;
        private int chestHair;
        private int chestHairColor;
        private float chestHairOpacity;
        private int eyeColor;
        //end barber

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


        //barber shit
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
        public int FacialHair
        {
            get
            {
                return facialHair;
            }
            set
            {
                facialHair = value;
                API.setEntitySyncedData(client, "ESS_Facial_Hair", value);
            }
        }
        public int FacialHairColor
        {
            get
            {
                return facialHairColor;
            }
            set
            {
                facialHairColor = value;
                API.setEntitySyncedData(client, "ESS_Facial_Hair_Color", value);
            }
        }
        public float FacialHairOpacity
        {
            get
            {
                return facialHairOpacity;
            }
            set
            {
                facialHairOpacity = value;
                API.setEntitySyncedData(client, "ESS_Facial_Hair_Opacity", value);
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
        public int EyebrowsColor
        {
            get
            {
                return eyebrowsColor;
            }
            set
            {
                eyebrowsColor = value;
                API.setEntitySyncedData(client, "ESS_Eyebrows_Color", value);
            }
        }
        public float EyebrowsOpacity
        {
            get
            {
                return eyebrowsOpacity;
            }
            set
            {
                eyebrowsOpacity = value;
                API.setEntitySyncedData(client, "ESS_Eyebrows_Opacity", value);
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
        public int LipstickColor
        {
            get
            {
                return lipstickColor;
            }
            set
            {
                lipstickColor = value;
                API.setEntitySyncedData(client, "ESS_Lipstick_Color", value);
            }
        }
        public float LipstickOpacity
        {
            get
            {
                return lipstickOpacity;
            }
            set
            {
                lipstickOpacity = value;
                API.setEntitySyncedData(client, "ESS_Lipstick_Opacity", value);
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
                API.setEntitySyncedData(client, "ESS_Chest_Hair", value);
            }
        }
        public int ChestHairColor
        {
            get
            {
                return chestHairColor;
            }
            set
            {
                chestHairColor = value;
                API.setEntitySyncedData(client, "ESS_Chest_Hair_Color", value);
            }
        }
        public float ChestHairOpacity
        {
            get
            {
                return chestHairOpacity;
            }
            set
            {
                chestHairOpacity = value;
                API.setEntitySyncedData(client, "ESS_Chest_Hair_Opacity", value);
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
                API.setEntitySyncedData(client, "ESS_Eye_Color", value);
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
        public float MakeupOpacity
        {
            get
            {
                return makeupOpacity;
            }
            set
            {
                makeupOpacity = value;
                API.setEntitySyncedData(client, "ESS_Makeup_Opacity", value);
            }
        }
        public int Facepaint
        {
            get
            {
                return facepaint;
            }
            set
            {
                facepaint = value;
                API.setEntitySyncedData(client, "ESS_Facepaint", value);
            }
        }
        public float FacepaintOpacity
        {
            get
            {
                return facepaintOpacity;
            }
            set
            {
                facepaintOpacity = value;
                API.setEntitySyncedData(client, "ESS_Facepaint_Opacity", value);
            }
        }


        //end barber shit


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
            client = pClass.PlayerClient;
            player = pClass;
            Mother = 0;
            father = 0;
            motherSkin = 0;
            fatherSkin = 0;
            faceBlend = 0;
            skinBlend = 0;
            moles = 0;
            blemishes = 0;
            complexion = 0;
            sunDamage = 0;
            bodyBlemishes = 0;
            ageing = 0;

            //barber shit
            hair = 0;
            hairColor = 0;
            hairHighlight = 0;
            facialHair = 0;
            facialHairColor = 0;
            facialHairOpacity = 0;
            eyebrows = 0;
            eyebrowsColor = 0;
            eyebrowsOpacity = 0;           
            lipstick = 0;
            lipstickColor = 0;
            lipstickOpacity = 0;
            chestHair = 0;
            chestHairColor = 0;
            chestHairOpacity = 0;
            eyeColor = 0;
            makeup = 0;
            makeupOpacity = 0;
            //end barber

            faceList = new float[21];
        }

        public void savePlayerFace()
        {
            string before = "UPDATE Skin SET";
            string[] varNames = { "Mother", "Father", "MotherSkin", "FatherSkin", "FaceBlend", "SkinBlend", "Hair", "HairColor", "HairHighlight", "Blemishes", "FacialHair", "FacialHairColor", "FacialHairOpacity", "Eyebrows", "EyebrowsColor", "EyebrowsOpacity", "Ageing", "Makeup", "MakeupOpacity", "Complexion", "SunDamage", "Lipstick", "Moles", "ChestHair", "ChestHairColor", "ChestHairOpacity", "BodyBlemishes", "EyeColor", "LipstickColor", "LipstickOpacity", "Makeup", "MakeupOpacity", "Facepaint", "FacepaintOpacity", "Face0", "Face1", "Face2", "Face3", "Face4", "Face5", "Face6", "Face7", "Face8", "Face9", "Face10", "Face11", "Face12", "Face13", "Face14", "Face15", "Face16", "Face17", "Face18", "Face19", "Face20" };
            string after = string.Format("WHERE Owner='{0}'", player.ID);
            object[] args = { mother, father, motherSkin, fatherSkin, faceBlend, skinBlend, hair, hairColor, hairHighlight, blemishes, facialHair, facialHairColor, facialHairOpacity, eyebrows, eyebrowsColor, eyebrowsOpacity, ageing, makeup, makeupOpacity, complexion, sunDamage, lipstick, moles, chestHair, chestHairColor, chestHairOpacity, bodyBlemishes, eyeColor, lipstickColor, lipstickOpacity, makeup, makeupOpacity, facepaint, facepaintOpacity, faceList[0], faceList[1], faceList[2], faceList[3], faceList[4], faceList[5], faceList[6], faceList[7], faceList[8], faceList[9], faceList[10], faceList[11], faceList[12], faceList[13], faceList[14], faceList[15], faceList[16], faceList[17], faceList[18], faceList[19], faceList[20] };
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

            int gender = Convert.ToInt32(face["Model"]);

            if (gender == 0)
            {
                API.setPlayerSkin(client, (PedHash)1885233650);
                //API.setPlayerSkin(client, PedHash.FreemodeMale01);
            }
            else
            {
                API.setPlayerSkin(client, (PedHash)(-1667301416));
                //API.setPlayerSkin(client, PedHash.FreemodeFemale01);
            }


            Father = Convert.ToInt32(face["Father"]);
            Mother = Convert.ToInt32(face["Mother"]);
            MotherSkin = Convert.ToInt32(face["MotherSkin"]);
            FatherSkin = Convert.ToInt32(face["FatherSkin"]);
            FaceBlend = Convert.ToSingle(face["FaceBlend"]);
            SkinBlend = Convert.ToSingle(face["SkinBlend"]);
            Ageing = Convert.ToInt32(face["Ageing"]);
            Blemishes = Convert.ToInt32(face["Blemishes"]);
            BodyBlemishes = Convert.ToInt32(face["BodyBlemishes"]);
            Complexion = Convert.ToInt32(face["Complexion"]);
            SunDamage = Convert.ToInt32(face["SunDamage"]);
            Moles = Convert.ToInt32(face["Moles"]);

            //barber shit
            Hair = Convert.ToInt32(face["Hair"]);
            HairColor = Convert.ToInt32(face["HairColor"]);
            HairHighlight = Convert.ToInt32(face["HairHighlight"]);

            FacialHair = Convert.ToInt32(face["FacialHair"]);
            FacialHairColor = Convert.ToInt32(face["FacialHairColor"]);
            FacialHairOpacity = Convert.ToInt32(face["FacialHairOpacity"]);

            Eyebrows = Convert.ToInt32(face["Eyebrows"]);
            EyebrowsColor = Convert.ToInt32(face["EyebrowsColor"]);
            EyebrowsOpacity = Convert.ToInt32(face["EyebrowsOpacity"]);

            Makeup = Convert.ToInt32(face["Makeup"]);
            MakeupOpacity = Convert.ToInt32(face["MakeupOpacity"]);
          
            ChestHair = Convert.ToInt32(face["ChestHair"]);
            ChestHairColor = Convert.ToInt32(face["ChestHairColor"]);
            ChestHairOpacity = Convert.ToInt32(face["ChestHairOpacity"]);

            EyeColor = Convert.ToInt32(face["EyeColor"]);

            Lipstick = Convert.ToInt32(face["Lipstick"]);
            LipstickColor = Convert.ToInt32(face["LipstickColor"]);
            LipstickOpacity = Convert.ToInt32(face["LipstickOpacity"]);

            Facepaint = Convert.ToInt32(face["Facepaint"]);
            FacepaintOpacity = Convert.ToInt32(face["FacepaintOpacity"]);
            //end barber

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

            if (Hair == 23 || Hair == 24)
            {
                Hair = 0;
            }
            API.setPlayerClothes(client, 2, Hair, 0);

            API.triggerClientEventForAll("ESS_SKIN_UPDATE", client);
            //sendPlayerFaceToClient(client);
        }

        public void sendPlayerFaceToClient(Client requester)
        {
            // Native Hell Hole
            API.sendNativeToPlayer(requester, (ulong)Hash.SET_PED_HEAD_BLEND_DATA, client.handle, Mother, Father, 0, MotherSkin, FatherSkin, 0, FaceBlend, SkinBlend, 0, false);
            API.sendNativeToPlayer(requester, (ulong)Hash.UPDATE_PED_HEAD_BLEND_DATA, client.handle, FaceBlend, SkinBlend, 0);
            // Eye Color
            API.sendNativeToPlayer(requester, (ulong)Hash._SET_PED_EYE_COLOR, client.handle, EyeColor);
            // Eyebrows
            API.sendNativeToPlayer(requester, (ulong)Hash.SET_PED_HEAD_OVERLAY, client.handle, 2, Eyebrows, EyebrowsOpacity);
            API.sendNativeToPlayer(requester, (ulong)Hash._SET_PED_HEAD_OVERLAY_COLOR, client.handle, 2, 1, EyebrowsColor, EyebrowsOpacity);
            // Lipstick
            API.sendNativeToPlayer(requester, (ulong)Hash.SET_PED_HEAD_OVERLAY, client.handle, 8, Lipstick, LipstickOpacity);
            API.sendNativeToPlayer(requester, (ulong)Hash._SET_PED_HEAD_OVERLAY_COLOR, client.handle, 8, 2, LipstickColor, LipstickOpacity);
            // Makeup
            API.sendNativeToPlayer(requester, (ulong)Hash.SET_PED_HEAD_OVERLAY, client.handle, 4, Makeup, MakeupOpacity);
            // Blemishes
            API.sendNativeToPlayer(requester, (ulong)Hash.SET_PED_HEAD_OVERLAY, client.handle, 0, Blemishes, 0.9);
            // FacialHair
            API.sendNativeToPlayer(requester, (ulong)Hash.SET_PED_HEAD_OVERLAY, client.handle, 1, FacialHair, FacialHairOpacity);
            API.sendNativeToPlayer(requester, (ulong)Hash._SET_PED_HEAD_OVERLAY_COLOR, client.handle, 1, 1, FacialHairColor, FacialHairOpacity);
            // Face Paint
            API.sendNativeToPlayer(requester, (ulong)Hash.SET_PED_HEAD_OVERLAY, client.handle, 1, Facepaint, FacepaintOpacity);
            // Ageing
            API.sendNativeToPlayer(requester, (ulong)Hash.SET_PED_HEAD_OVERLAY, client.handle, 3, Ageing, 0.9);
            // Complexion
            API.sendNativeToPlayer(requester, (ulong)Hash.SET_PED_HEAD_OVERLAY, client.handle, 6, Complexion, 0.9);
            // Moles
            API.sendNativeToPlayer(requester, (ulong)Hash.SET_PED_HEAD_OVERLAY, client.handle, 9, Moles, 0.9);
            // SunDamage
            API.sendNativeToPlayer(requester, (ulong)Hash.SET_PED_HEAD_OVERLAY, client.handle, 7, SunDamage, 0.9);
            // ChestHair
            API.sendNativeToPlayer(requester, (ulong)Hash.SET_PED_HEAD_OVERLAY, client.handle, 10, ChestHair, ChestHairOpacity);
            API.sendNativeToPlayer(requester, (ulong)Hash._SET_PED_HEAD_OVERLAY_COLOR, client.handle, 10, 1, ChestHairColor, ChestHairOpacity);
            // BodyBlemishes
            API.sendNativeToPlayer(requester, (ulong)Hash.SET_PED_HEAD_OVERLAY, client.handle, 7, BodyBlemishes, 0.9);
            // Hair Color
            API.sendNativeToPlayer(requester, (ulong)Hash._SET_PED_HAIR_COLOR, client.handle, HairColor, HairHighlight);
            // FaceList (e.g. nose length, chin shape, etc)
            for (var i = 0; i < 21; i++)
            {
                API.sendNativeToPlayer(requester, (ulong)Hash._SET_PED_FACE_FEATURE, client.handle, i, FaceList[i]);
            }
            
        }
    }
}
