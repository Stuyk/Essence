"use strict";
var grid = null;
var debugMode = false;
var opened = false;
var items = new Array();
var currentSelection = null;
var itemSize = 0;
class Grid {
    constructor(rows, columns, boxSize, startingXPos, startingYPos, gutter) {
        var currentYGutter = gutter;
        var currentXGutter = gutter;
        var gutterValue = gutter;
        this.size = boxSize;
        this.boxes = new Array();
        for (var r = 0; r < rows; r++) {
            for (var i = 0; i < columns; i++) {
                if (r === 0) {
                    var box = new Box(((this.size * i) + startingXPos + currentXGutter), (startingYPos + currentYGutter), this.size);
                    this.boxes.push(box);
                }
                else {
                    var box = new Box(((this.size * i) + startingXPos + currentXGutter), ((this.size * r) + startingYPos + currentYGutter), this.size);
                    this.boxes.push(box);
                }
                currentXGutter += gutterValue;
            }
            currentXGutter = gutter;
            currentYGutter += gutterValue;
        }
    }
    get GetBoxes() {
        return this.boxes;
    }
}
class Box {
    constructor(x, y, size) {
        this.size = size;
        this.x = x;
        this.y = y;
        this.item = null;
        itemSize = this.size;
    }
    get Size() {
        return this.size;
    }
    get Position() {
        return new Point(this.x, this.y);
    }
    set BoxItem(value) {
        this.item = value;
    }
    get BoxItem() {
        return this.item;
    }
    mouseCheck() {
        var mouse = API.getCursorPositionMaintainRatio();
        if (mouse.X > this.x && mouse.X < this.x + this.size && mouse.Y > this.y && mouse.Y < this.y + this.size) {
            return true;
        }
        return false;
    }
    drawBox() {
        API.drawRectangle(this.x, this.y, this.size, this.size, 255, 255, 255, 100);
    }
}
class Item {
    constructor(id, type, quantity, consumeable, data) {
        this.x = Math.round(API.getScreenResolutionMaintainRatio().Width / 2);
        this.y = Math.round(API.getScreenResolutionMaintainRatio().Height / 2);
        this.id = id;
        this.type = type;
        this.quantity = quantity;
        this.data = data;
        this.consumeable = consumeable;
        this.box = null;
        this.selected = false;
        this.splitTimer = Date.now() + 3000;
        this.findOpenPosition();
    }
    draw() {
        let itemName = this.type.toLowerCase();
        itemName = itemName.charAt(0).toUpperCase() + itemName.slice(1);
        itemName = itemName.replace('_', ' ');
        API.drawText("" + itemName, this.centerX, this.centerY - 30, 0.5, 255, 255, 255, 255, 4, 1, false, true, 500);
        API.drawText("x" + this.quantity.toString(), this.centerX, this.centerY + 20, 0.3, 255, 255, 255, 255, 4, 1, false, true, 500);
        if (this.data.length > 0) {
            API.drawText(this.data, this.centerX, this.centerY, 0.3, 255, 255, 255, 255, 4, 1, false, true, 500);
        }
        this.mouseCheck();
        if (this.selected) {
            this.move();
        }
        else {
            if (API.isControlPressed(21)) {
                this.splitSelection();
            }
            else {
                this.getSelection();
                this.consumeSelection();
            }
        }
    }
    findOpenPosition() {
        var boxes = grid.GetBoxes;
        if (boxes.length <= 0) {
            return;
        }
        for (var i = 0; i < boxes.length; i++) {
            if (boxes[i].BoxItem === null) {
                boxes[i].BoxItem = this;
                this.box = boxes[i];
                this.x = boxes[i].Position.X;
                this.y = boxes[i].Position.Y;
                this.calculateCenterPoints();
                break;
            }
        }
    }
    calculateCenterPoints() {
        this.centerX = Math.round(this.x + (itemSize / 2));
        this.centerY = Math.round(this.y + (itemSize / 2));
    }
    consumeSelection() {
        if (!API.isControlJustReleased(238)) {
            return;
        }
        if (!this.mouseCheck()) {
            return;
        }
        if (!this.consumeable) {
            return;
        }
        this.quantity -= 1;
        if (this.quantity <= 0) {
            this.removeItem();
        }
        API.triggerServerEvent("USE_ITEM", this.id);
        API.playSoundFrontEnd("Load_Scene", "DLC_Dmod_Prop_Editor_Sounds");
    }
    getSelection() {
        if (currentSelection !== null) {
            return;
        }
        if (!API.isControlPressed(237)) {
            return;
        }
        if (!this.mouseCheck()) {
            return;
        }
        currentSelection = this;
        this.selected = true;
        this.removeBinding();
        API.playSoundFrontEnd("Select_Placed_Prop", "DLC_Dmod_Prop_Editor_Sounds");
        if (debugMode) {
            API.sendChatMessage("Selected");
        }
    }
    splitSelection() {
        if (currentSelection !== null) {
            return;
        }
        if (!API.isControlJustPressed(237)) {
            return;
        }
        if (!this.mouseCheck()) {
            return;
        }
        if (Date.now() < this.splitTimer) {
            return;
        }
        this.splitTimer = Date.now() + 5000;
        var splitValue = Math.floor(this.quantity / 2);
        var possibleNewValue = this.quantity - splitValue;
        if (splitValue <= 0) {
            return;
        }
        API.playSoundFrontEnd("Reset_Prop_Position", "DLC_Dmod_Prop_Editor_Sounds");
        this.quantity = possibleNewValue;
        addInventoryItem(this.id, this.type, splitValue, this.consumeable, this.data);
    }
    removeBinding() {
        this.box.BoxItem = null;
        this.box = null;
    }
    mouseCheck() {
        var mouse = API.getCursorPositionMaintainRatio();
        if (mouse.X > this.x && mouse.X < this.x + itemSize && mouse.Y > this.y && mouse.Y < this.y + itemSize) {
            return true;
        }
        return false;
    }
    findClosestBoxToPointer() {
        var boxes = grid.GetBoxes;
        for (var i = 0; i < boxes.length; i++) {
            if (boxes[i].mouseCheck()) {
                return boxes[i];
            }
        }
        return null;
    }
    move() {
        if (currentSelection !== this) {
            this.selected = false;
            return;
        }
        if (API.isControlPressed(237)) {
            var mouse = API.getCursorPositionMaintainRatio();
            this.x = mouse.X - Math.round(itemSize / 2);
            this.y = mouse.Y - Math.round(itemSize / 2);
            this.calculateCenterPoints();
            resource.Utility.setHand(true);
        }
        else {
            resource.Utility.setHand(false);
            this.selected = false;
            currentSelection = null;
            var closestBox = this.findClosestBoxToPointer();
            if (closestBox === null) {
                if (debugMode) {
                    API.sendChatMessage("Spot does not exist, dropping item.");
                }
                this.dropItem();
                return;
            }
            if (closestBox.BoxItem === null) {
                if (debugMode) {
                    API.sendChatMessage("Found open Box spot. Attempting placement.");
                }
                closestBox.BoxItem = this;
                this.box = closestBox;
                this.x = closestBox.Position.X;
                this.y = closestBox.Position.Y;
                this.calculateCenterPoints();
                API.playSoundFrontEnd("Place_Prop_Success", "DLC_Dmod_Prop_Editor_Sounds");
                if (debugMode) {
                    API.sendChatMessage("Placement succeeded.");
                }
            }
            else {
                this.findOpenPosition();
            }
        }
    }
    removeItem() {
        var index = 0;
        for (index = 0; index < items.length; index++) {
            if (items[index] === this) {
                break;
            }
        }
        if (index > items.length) {
            return;
        }
        items.splice(index, 1);
    }
    dropItem() {
        this.removeItem();
        var playerPos = API.getEntityPosition(API.getLocalPlayer());
        var aimCoords = API.getPlayerAimCoords(API.getLocalPlayer());
        API.playSoundFrontEnd("Place_Prop_Fail", "DLC_Dmod_Prop_Editor_Sounds");
        if (playerPos.DistanceTo(aimCoords) <= 6) {
            API.triggerServerEvent("DROP_ITEM", this.id, this.type, aimCoords, this.quantity);
        }
        else {
            var groundHeight = API.getGroundHeight(playerPos);
            var newPos = new Vector3(playerPos.X, playerPos.Y, groundHeight);
            API.triggerServerEvent("DROP_ITEM", this.id, this.type, newPos, this.quantity);
        }
    }
}
API.onResourceStart.connect(() => {
    grid = new Grid(5, 8, 150, 300, 50, 10);
});
function toggleInventory() {
    if (opened) {
        opened = false;
        items = [];
        resource.Utility.toggleCursor();
        var gridBoxes = grid.GetBoxes;
        for (var i = 0; i < gridBoxes.length; i++) {
            gridBoxes[i].BoxItem = null;
        }
    }
    else {
        resource.Utility.toggleCursor();
        opened = true;
        API.triggerServerEvent("GET_ITEMS");
    }
}
function addInventoryItem(id, type, quantity, consumeable, data) {
    items.push(new Item(id, type, quantity, consumeable, data));
}
API.onUpdate.connect(() => {
    if (!opened) {
        return;
    }
    for (var i = 0; i < items.length; i++) {
        items[i].draw();
    }
    var gridBoxes = grid.GetBoxes;
    for (var i = 0; i < gridBoxes.length; i++) {
        gridBoxes[i].drawBox();
    }
});
