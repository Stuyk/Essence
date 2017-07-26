"use strict";
API.onResourceStart.connect(function () {
    var players = API.getStreamedPlayers();
    for (var i = players.Length - 1; i >= 0; i--) {
        setPedCharacter(players[i]);
    }
});
API.onServerEventTrigger.connect(function (name, args) {
    if (name === "ESS_SKIN_UPDATE") {
        if (args[0] !== API.getLocalPlayer()) {
            setPedCharacter(API.getLocalPlayer());
        }
        else {
            setPedCharacter(args[0]);
        }
    }
});
API.onEntityStreamIn.connect(function (ent, entType) {
    if (entType == 6 || entType == 8) {
        setPedCharacter(ent);
    }
});
function setPedCharacter(ent) {
    if (API.getEntityModel(ent) === 1885233650 || API.getEntityModel(ent) === -1667301416) {
        var Mother = API.getEntitySyncedData(ent, "ESS_Mother");
        var Father = API.getEntitySyncedData(ent, "ESS_Father");
        var MotherSkin = API.getEntitySyncedData(ent, "ESS_MotherSkin");
        var FatherSkin = API.getEntitySyncedData(ent, "ESS_FatherSkin");
        var FaceBlend = API.getEntitySyncedData(ent, "ESS_FaceBlend");
        var SkinBlend = API.getEntitySyncedData(ent, "ESS_SkinBlend");
        var Blemishes = API.getEntitySyncedData(ent, "ESS_Blemishes");
        var Ageing = API.getEntitySyncedData(ent, "ESS_Ageing");
        var Complexion = API.getEntitySyncedData(ent, "ESS_Complexion");
        var SunDamage = API.getEntitySyncedData(ent, "ESS_SunDamage");
        var Moles = API.getEntitySyncedData(ent, "ESS_Moles");
        var BodyBlemishes = API.getEntitySyncedData(ent, "ESS_BodyBlemishes");
        var Hair = API.getEntitySyncedData(ent, "ESS_Hair");
        var HairColor = API.getEntitySyncedData(ent, "ESS_HairColor");
        var HairHighlight = API.getEntitySyncedData(ent, "ESS_HairHighlight");
        var ScalpCollection = API.getEntitySyncedData(ent, "ESS_Scalp_Collection");
        var ScalpOverlay = API.getEntitySyncedData(ent, "ESS_Scalp_Overlay");
        var FacialHair = API.getEntitySyncedData(ent, "ESS_Facial_Hair");
        var FacialHairColor = API.getEntitySyncedData(ent, "ESS_Facial_Hair_Color");
        var FacialHairOpacity = API.getEntitySyncedData(ent, "ESS_Facial_Hair_Opacity");
        var Eyebrows = API.getEntitySyncedData(ent, "ESS_Eyebrows");
        var EyebrowsColor = API.getEntitySyncedData(ent, "ESS_Eyebrows_Color");
        var EyebrowsOpacity = API.getEntitySyncedData(ent, "ESS_Eyebrows_Opacity");
        var ChestHair = API.getEntitySyncedData(ent, "ESS_Chest_Hair");
        var ChestHairColor = API.getEntitySyncedData(ent, "ESS_Chest_Hair_Color");
        var ChestHairOpacity = API.getEntitySyncedData(ent, "ESS_Chest_Hair_Opacity");
        var Facepaint = API.getEntitySyncedData(ent, "ESS_Facepaint");
        var FacepaintOpacity = API.getEntitySyncedData(ent, "ESS_Facepaint_Opacity");
        var Makeup = API.getEntitySyncedData(ent, "ESS_Makeup");
        var MakeupOpacity = API.getEntitySyncedData(ent, "ESS_MakeupOpacity");
        var Lipstick = API.getEntitySyncedData(ent, "ESS_Lipstick");
        var LipstickColor = API.getEntitySyncedData(ent, "ESS_Lipstick_Color");
        var LipstickOpacity = API.getEntitySyncedData(ent, "ESS_Lipstick_Opacity");
        var EyeColor = API.getEntitySyncedData(ent, "ESS_EyeColor");
        var FaceList = API.getEntitySyncedData(ent, "ESS_FaceList");
        API.callNative("SET_PED_HEAD_BLEND_DATA", ent, Mother, Father, 0, MotherSkin, FatherSkin, 0, parseFloat(FaceBlend), parseFloat(SkinBlend), 0, false);
        API.callNative("UPDATE_PED_HEAD_BLEND_DATA", ent, parseFloat(FaceBlend), parseFloat(SkinBlend), 0);
        API.callNative("_SET_PED_EYE_COLOR", ent, EyeColor);
        API.callNative("SET_PED_HEAD_OVERLAY", ent, 2, Eyebrows, 0.9);
        API.callNative("_SET_PED_HEAD_OVERLAY_COLOR", ent, 2, 1, EyebrowsColor, EyebrowsOpacity);
        API.callNative("SET_PED_HEAD_OVERLAY", ent, 8, Lipstick, 0.9);
        API.callNative("_SET_PED_HEAD_OVERLAY_COLOR", ent, 8, 2, LipstickColor, LipstickOpacity);
        API.callNative("SET_PED_HEAD_OVERLAY", ent, 4, Makeup, 0.9);
        API.callNative("_SET_PED_HEAD_OVERLAY_COLOR", ent, 4, 0, Makeup, MakeupOpacity);
        API.callNative("SET_PED_HEAD_OVERLAY", ent, 0, Blemishes, 0.9);
        API.callNative("SET_PED_HEAD_OVERLAY", ent, 1, FacialHair, 0.9);
        API.callNative("_SET_PED_HEAD_OVERLAY_COLOR", ent, 1, 1, FacialHairColor, FacialHairOpacity);
        API.callNative("SET_PED_HEAD_OVERLAY", ent, 3, Ageing, 0.9);
        API.callNative("SET_PED_HEAD_OVERLAY", ent, 6, Complexion, 0.9);
        API.callNative("SET_PED_HEAD_OVERLAY", ent, 9, Moles, 0.9);
        API.callNative("SET_PED_HEAD_OVERLAY", ent, 7, SunDamage, 0.9);
        API.callNative("SET_PED_HEAD_OVERLAY", ent, 10, ChestHair, 0.9);
        API.callNative("_SET_PED_HEAD_OVERLAY_COLOR", ent, 10, 1, ChestHairColor, ChestHairOpacity);
        API.callNative("SET_PED_HEAD_OVERLAY", ent, 7, BodyBlemishes, 0.9);
        API.setPlayerClothes(ent, 2, Hair, 0);
        API.callNative("_SET_PED_HAIR_COLOR", ent, HairColor, HairHighlight);
        API.callNative("SET_PED_HEAD_OVERLAY", ent, 4, Facepaint, FacepaintOpacity);
        API.callNative("_SET_PED_FACIAL_DECORATION", ent, API.getHashKey(ScalpCollection), API.getHashKey(ScalpOverlay));
        for (var i = 0; i < 21; i++) {
            API.callNative("_SET_PED_FACE_FEATURE", ent, i, parseFloat(FaceList[i]));
        }
    }
}
