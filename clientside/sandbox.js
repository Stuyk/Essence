"use strict";
API.onChatMessage.connect((msg) => {
    if (msg == "help") {
        mainMenu();
    }
});
var father = 0;
var mother = 0;
var faceBlend = "0.5";
var fatherSkin = 0;
var motherSkin = 0;
var skinBlend = "0.5";
function mainMenu() {
    resource.SimpleUI.clearContent();
    // Base Menu Holder
    var contentHolder = resource.SimpleUI.setupContent();
    var header = contentHolder.createHeader();
    header.setLocalImage("clientside/images/banners/fmsu.jpg");
    // Items
    addFather(contentHolder);
    addMother(contentHolder);
    //addMother(contentHolder);
    // This will tell the client that the menu is ready.
    contentHolder.IsReady = true;
}
// ===============================
// FATHER CODE
// ===============================
function addFather(contentHolder) {
    var item = contentHolder.addItem("Father");
    item.Description = "Which 'Father' face type would you like to blend.";
    for (var i = 0; i < 46; i++) {
        var subitem = item.addVariedContentItem(contentHolder, i.toString());
        setupFatherFaceFunction(subitem, i);
    }
    getCurrentFatherValue("ESS_Father", item);
}
function getCurrentFatherValue(syncString, item) {
    if (API.hasEntitySyncedData(API.getLocalPlayer(), syncString)) {
        item.CurrentSelection = API.getEntitySyncedData(API.getLocalPlayer(), syncString);
        father = item.CurrentSelection;
    }
    else {
        item.CurrentSelection = 0;
    }
}
function setupFatherFaceFunction(subitem, id) {
    // Select Function
    var selectHolder = subitem.getSelectFunction();
    selectHolder.Function = setFatherFace;
    selectHolder.Args = [id];
    subitem.SelectFunction = selectHolder;
    // Hover Function
    var hoverHolder = subitem.getHoverFunction();
    hoverHolder.Function = updateFatherFace;
    hoverHolder.Args = [id];
    subitem.HoverFunction = hoverHolder;
}
function updateFatherFace(args) {
    API.callNative("SET_PED_HEAD_BLEND_DATA", API.getLocalPlayer(), mother, Number(args[0]), 0, motherSkin, fatherSkin, 0, faceBlend, skinBlend, 0);
}
function setFatherFace(args) {
    updateFatherFace(args);
    father = args[0];
}
// ===============================
// Mother CODE
// ===============================
function addMother(contentHolder) {
    var item = contentHolder.addItem("Mother");
    item.Description = "Which 'Mother' face type would you like to blend.";
    for (var i = 0; i < 46; i++) {
        var subitem = item.addVariedContentItem(contentHolder, i.toString());
        setupMotherFaceFunction(subitem, i);
    }
    getCurrentMotherValue("ESS_Mother", item);
}
function getCurrentMotherValue(syncString, item) {
    if (API.hasEntitySyncedData(API.getLocalPlayer(), syncString)) {
        item.CurrentSelection = API.getEntitySyncedData(API.getLocalPlayer(), syncString);
        mother = item.CurrentSelection;
    }
    else {
        item.CurrentSelection = 0;
    }
}
function setupMotherFaceFunction(subitem, id) {
    // Select Function
    var selectHolder = subitem.getSelectFunction();
    selectHolder.Function = resource.sandbox.setMotherFace;
    selectHolder.Args = [id];
    subitem.SelectFunction = selectHolder;
    // Hover Function
    var hoverHolder = subitem.getHoverFunction();
    hoverHolder.Function = resource.sandbox.updateMotherFace;
    hoverHolder.Args = [id];
    subitem.HoverFunction = hoverHolder;
}
function updateMotherFace(args) {
    API.callNative("SET_PED_HEAD_BLEND_DATA", API.getLocalPlayer(), args[0], father, 0, motherSkin, fatherSkin, 0, parseFloat(faceBlend), parseFloat(skinBlend), 0);
}
function setMotherFace(args) {
    updateMotherFace(args);
    father = args[0];
}
