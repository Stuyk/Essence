"use strict";
API.onResourceStart.connect(() => {
    mainMenu();
});
function mainMenu() {
    resource.SimpleUI.clearContent();
    // Base Menu Holder
    var contentHolder = resource.SimpleUI.setupContent();
    var header = contentHolder.createHeader();
    header.setGameTexture("shopui_title_lowendfashion2", "shopui_title_lowendfashion2");
    // Generate an item with hover / select function
    var item = contentHolder.addItem("Clothing ->");
    item.Description = "This will go to a sub menu.";
    var holder = item.getSelectFunction();
    holder.Function = subClothing;
    var item = contentHolder.addItem("Item");
    // This will tell the client that the menu is ready.
    contentHolder.IsReady = true;
}
function subClothing() {
    resource.SimpleUI.clearContent();
    // Base Menu Holder
    var contentHolder = resource.SimpleUI.setupContent();
    var header = contentHolder.createHeader();
    header.setGameTexture("shopui_title_lowendfashion2", "shopui_title_lowendfashion2");
    // Generate an item with hover / select function
    var item = contentHolder.addItem("<- Back");
    item.Description = "This will go to a sub menu.";
    var holder = item.getSelectFunction();
    holder.Function = mainMenu;
    // This will tell the client that the menu is ready.
    contentHolder.IsReady = true;
}
function myNotSoFancyAssFunction() {
    API.sendChatMessage("You pressed enter.");
}
function myFancyAssFunction(args) {
    API.sendChatMessage(`${args[0]} ${args[1]} ${args[2]} ${args[3]}`);
}
