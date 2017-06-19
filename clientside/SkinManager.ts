API.onResourceStart.connect(function () {
    var players = API.getStreamedPlayers();

    for (var i = players.Length - 1; i >= 0; i--) {
        setPedCharacter(players[i]);
    }
});

API.onServerEventTrigger.connect(function (name, args) {
    if (name === "ESS_SKIN_UPDATE") {
        var entity = args[0];
        setPedCharacter(entity);
    }
});

API.onEntityStreamIn.connect(function (ent, entType) {
    if (entType == 6 || entType == 8) {
        setPedCharacter(ent);
    }
});

function setPedCharacter(ent) {
    if (!API.isPed(ent) && (API.getEntityModel(ent) != 1885233650 || API.getEntityModel(ent) != -1667301416)) {
        return;
    }

    var Mother = API.getEntitySyncedData(ent, "ESS_Mother");
    var Father = API.getEntitySyncedData(ent, "ESS_Father");
    var MotherSkin = API.getEntitySyncedData(ent, "ESS_MotherSkin");
    var FatherSkin = API.getEntitySyncedData(ent, "ESS_FatherSkin");
    var FaceBlend = API.getEntitySyncedData(ent, "ESS_FaceBlend");
    var SkinBlend = API.getEntitySyncedData(ent, "ESS_SkinBlend");
    var Hair = API.getEntitySyncedData(ent, "ESS_Hair");
    var HairColor = API.getEntitySyncedData(ent, "ESS_HairColor");
    var HairHighlight = API.getEntitySyncedData(ent, "ESS_HairHighlight");
    var Blemishes = API.getEntitySyncedData(ent, "ESS_Blemishes");
    var FacialHair = API.getEntitySyncedData(ent, "ESS_FacialHair");
    var Eyebrows = API.getEntitySyncedData(ent, "ESS_Eyebrows");
    var Ageing = API.getEntitySyncedData(ent, "ESS_Ageing");
    var Makeup = API.getEntitySyncedData(ent, "ESS_Makeup");
    var Blush = API.getEntitySyncedData(ent, "ESS_Blush");
    var Complexion = API.getEntitySyncedData(ent, "ESS_Complexion");
    var SunDamage = API.getEntitySyncedData(ent, "ESS_SunDamage");
    var Lipstick = API.getEntitySyncedData(ent, "ESS_Lipstick");
    var Moles = API.getEntitySyncedData(ent, "ESS_Moles");
    var ChestHair = API.getEntitySyncedData(ent, "ESS_ChestHair");
    var BodyBlemishes = API.getEntitySyncedData(ent, "ESS_BodyBlemishes");
    var EyeColor = API.getEntitySyncedData(ent, "ESS_EyeColor");
    var LipstickColor = API.getEntitySyncedData(ent, "ESS_LipstickColor");
    var LipstickColor2 = API.getEntitySyncedData(ent, "ESS_LipstickColor2");
    var MakeupColor = API.getEntitySyncedData(ent, "ESS_MakeupColor");
    var MakeupColor2 = API.getEntitySyncedData(ent, "ESS_MakeupColor2");
    var FaceList = API.getEntitySyncedData(ent, "ESS_FaceList");

    // Face
    API.callNative("SET_PED_HEAD_BLEND_DATA", ent, Mother, Father, 0, MotherSkin, FatherSkin, 0, parseFloat(FaceBlend), parseFloat(SkinBlend), 0, false);
    // Blend Data
    API.callNative("UPDATE_PED_HEAD_BLEND_DATA", ent, parseFloat(FaceBlend), parseFloat(SkinBlend), 0);
    // Eye Color
    API.callNative("_SET_PED_EYE_COLOR", ent, EyeColor);
    // Eyebrows
    API.callNative("SET_PED_HEAD_OVERLAY", ent, 2, Eyebrows, 0.9);
    API.callNative("_SET_PED_HEAD_OVERLAY_COLOR", ent, 2, 1, HairColor, HairHighlight);
    // Lipstick
    API.callNative("SET_PED_HEAD_OVERLAY", ent, 8, Lipstick, 0.9);
    API.callNative("_SET_PED_HEAD_OVERLAY_COLOR", ent, 8, 2, LipstickColor, LipstickColor2);
    // Makeup
    API.callNative("SET_PED_HEAD_OVERLAY", ent, 4, Makeup, 0.9);
    API.callNative("_SET_PED_HEAD_OVERLAY_COLOR", ent, 4, 0, MakeupColor, MakeupColor2);
    // Blemishes
    API.callNative("SET_PED_HEAD_OVERLAY", ent, 0, Blemishes, 0.9);
    // FacialHair
    API.callNative("SET_PED_HEAD_OVERLAY", ent, 1, FacialHair, 0.9);
    API.callNative("_SET_PED_HEAD_OVERLAY_COLOR", ent, 1, 1, HairColor, HairHighlight);
    // Ageing
    API.callNative("SET_PED_HEAD_OVERLAY", ent, 3, Ageing, 0.9);
    // Complexion
    API.callNative("SET_PED_HEAD_OVERLAY", ent, 6, Complexion, 0.9);
    // Moles
    API.callNative("SET_PED_HEAD_OVERLAY", ent, 9, Moles, 0.9);
    // SunDamage
    API.callNative("SET_PED_HEAD_OVERLAY", ent, 7, SunDamage, 0.9);
    // ChestHair
    API.callNative("SET_PED_HEAD_OVERLAY", ent, 10, ChestHair, 0.9);
    // BodyBlemishes
    API.callNative("SET_PED_HEAD_OVERLAY", ent, 7, BodyBlemishes, 0.9);
    // FaceList (e.g. nose length, chin shape, etc)
    for (var i = 0; i < 21; i++) {
        API.callNative("_SET_PED_FACE_FEATURE", ent, i, parseFloat(FaceList[i]));
    }
    // Hair Color
    API.callNative("_SET_PED_HAIR_COLOR", ent, HairColor, HairHighlight);
}