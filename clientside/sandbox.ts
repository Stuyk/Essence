API.onResourceStart.connect(() => {
    mainMenu();
});

function mainMenu() {
    resource.SimpleUI.clearContent();
    // Base Menu Holder
    var contentHolder: ContentHolder = resource.SimpleUI.setupContent();
    var header: ContentHeader = contentHolder.createHeader();
    header.setGameTexture("shopui_title_lowendfashion2", "shopui_title_lowendfashion2");
    // Generate an item with hover / select function
    var item: ContentItem = contentHolder.addItem("Clothing ->");
    item.Description = "This will go to a sub menu.";
    var holder: FunctionHolder = item.getSelectFunction();
    holder.Function = subClothing;
    var item: ContentItem = contentHolder.addItem("Item");
    // This will tell the client that the menu is ready.
    contentHolder.IsReady = true;
}

function subClothing() {
    resource.SimpleUI.clearContent();
    // Base Menu Holder
    var contentHolder: ContentHolder = resource.SimpleUI.setupContent();
    var header: ContentHeader = contentHolder.createHeader();
    header.setGameTexture("shopui_title_lowendfashion2", "shopui_title_lowendfashion2");
    // Generate an item with hover / select function
    var item: ContentItem = contentHolder.addItem("<- Back");
    item.Description = "This will go to a sub menu.";
    var holder: FunctionHolder = item.getSelectFunction();
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