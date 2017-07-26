var grid = null;
var debugMode = false;
var opened = false;
var items = new Array();
// Used by our items.
var currentSelection = null;
var itemSize = 0;
var Grid = (function () {
    function Grid(rows, columns, boxSize, startingXPos, startingYPos, gutter) {
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
    Object.defineProperty(Grid.prototype, "GetBoxes", {
        // Returns a list of our box elements when we defined our grid.
        get: function () {
            return this.boxes;
        },
        enumerable: true,
        configurable: true
    });
    return Grid;
}());
// A box element inside of our grid, determines whether or not it holds an 'item';
var Box = (function () {
    function Box(x, y, size) {
        this.size = size;
        this.x = x;
        this.y = y;
        this.item = null;
        itemSize = this.size;
    }
    Object.defineProperty(Box.prototype, "Size", {
        // Return the size of our block element. Inherited by the grid usually.
        get: function () {
            return this.size;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(Box.prototype, "Position", {
        // Return the position of our box element in a Pointer format.
        get: function () {
            return new Point(this.x, this.y);
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(Box.prototype, "BoxItem", {
        get: function () {
            return this.item;
        },
        // Set / Get the Item that is currently attached to this object.
        set: function (value) {
            this.item = value;
        },
        enumerable: true,
        configurable: true
    });
    // Check if our mouse is within this box.
    Box.prototype.mouseCheck = function () {
        var mouse = API.getCursorPositionMaintainRatio();
        // Check if your mouse is greater than X but less than X + Size, same thing with Y values.
        if (mouse.X > this.x && mouse.X < this.x + this.size && mouse.Y > this.y && mouse.Y < this.y + this.size) {
            return true;
        }
        return false;
    };
    // Draw our grids as a debug option.
    Box.prototype.drawBox = function () {
        API.drawRectangle(this.x, this.y, this.size, this.size, 255, 255, 255, 100);
    };
    return Box;
}());
// An item element will represent a 'phsyical item' that can be split up or dropped.
var Item = (function () {
    function Item(id, type, quantity, consumeable, data) {
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
    // This is used to draw our items.
    Item.prototype.draw = function () {
        var itemName = this.type.toLowerCase();
        itemName = itemName.charAt(0).toUpperCase() + itemName.slice(1);
        itemName = itemName.replace('_', ' ');
        API.drawText("" + itemName, this.centerX, this.centerY - 30, 0.5, 255, 255, 255, 255, 4, 1, false, true, 500);
        API.drawText("x" + this.quantity.toString(), this.centerX, this.centerY + 20, 0.3, 255, 255, 255, 255, 4, 1, false, true, 500);
        if (this.data.length > 0) {
            API.drawText(this.data, this.centerX, this.centerY, 0.3, 255, 255, 255, 255, 4, 1, false, true, 500);
        }
        this.mouseCheck();
        // If this is selected. Move it, else let's get the selection.
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
    };
    // When the item is first created, it will look for an open spot.
    Item.prototype.findOpenPosition = function () {
        var boxes = grid.GetBoxes;
        if (boxes.length <= 0) {
            return;
        }
        // Loop through our boxes and find a non-null one.
        for (var i = 0; i < boxes.length; i++) {
            if (boxes[i].BoxItem === null) {
                // Assign our box to our item and our item to our box.
                boxes[i].BoxItem = this;
                this.box = boxes[i];
                // Also setup positions.
                this.x = boxes[i].Position.X;
                this.y = boxes[i].Position.Y;
                this.calculateCenterPoints();
                break;
            }
        }
        // Add the code to automatically drop items on full-inventory.
    };
    // Used to re-calculate center points.
    Item.prototype.calculateCenterPoints = function () {
        this.centerX = Math.round(this.x + (itemSize / 2));
        this.centerY = Math.round(this.y + (itemSize / 2));
    };
    // Consume Selection
    Item.prototype.consumeSelection = function () {
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
        // Consumeable shit -->
        API.triggerServerEvent("USE_ITEM", this.id);
        API.playSoundFrontEnd("Load_Scene", "DLC_Dmod_Prop_Editor_Sounds");
    };
    // Get our current selection.
    Item.prototype.getSelection = function () {
        // If our current selection is not open, don't bother.'
        if (currentSelection !== null) {
            return;
        }
        // Check if left click is pressed.
        if (!API.isControlPressed(237)) {
            return;
        }
        // Check if the mouse is within position of this box.
        if (!this.mouseCheck()) {
            return;
        }
        // If all goes well let's make this our new selection.
        currentSelection = this;
        this.selected = true;
        this.removeBinding();
        API.playSoundFrontEnd("Select_Placed_Prop", "DLC_Dmod_Prop_Editor_Sounds");
        if (debugMode) {
            API.sendChatMessage("Selected");
        }
    };
    // Split your selection into two.
    Item.prototype.splitSelection = function () {
        if (currentSelection !== null) {
            return;
        }
        // Check if the mouse down is applied.
        if (!API.isControlJustPressed(237)) {
            return;
        }
        // Check if the mouse is within position of this box.
        if (!this.mouseCheck()) {
            return;
        }
        // Check our split timer.
        if (Date.now() < this.splitTimer) {
            return;
        }
        // Re-Assign the split timer.
        this.splitTimer = Date.now() + 5000;
        // If all goes well let's make this our target.
        var splitValue = Math.floor(this.quantity / 2);
        var possibleNewValue = this.quantity - splitValue;
        if (splitValue <= 0) {
            return;
        }
        API.playSoundFrontEnd("Reset_Prop_Position", "DLC_Dmod_Prop_Editor_Sounds");
        this.quantity = possibleNewValue;
        addInventoryItem(this.id, this.type, splitValue, this.consumeable, this.data);
    };
    // Remove item bind from box.
    Item.prototype.removeBinding = function () {
        this.box.BoxItem = null;
        this.box = null;
    };
    // This function will check if the mouse is inside of this specific item.
    Item.prototype.mouseCheck = function () {
        var mouse = API.getCursorPositionMaintainRatio();
        // Check if your mouse is greater than X but less than X + Size, same thing with Y values.
        if (mouse.X > this.x && mouse.X < this.x + itemSize && mouse.Y > this.y && mouse.Y < this.y + itemSize) {
            return true;
        }
        return false;
    };
    // Find closest grid to position.
    Item.prototype.findClosestBoxToPointer = function () {
        var boxes = grid.GetBoxes;
        for (var i = 0; i < boxes.length; i++) {
            if (boxes[i].mouseCheck()) {
                return boxes[i];
            }
        }
        return null;
    };
    // Used to move our item around.
    Item.prototype.move = function () {
        // Check if our current selection is this, and our object is selected.
        if (currentSelection !== this) {
            this.selected = false;
            return;
        }
        // Ensure our mouse is pressed down.
        if (API.isControlPressed(237)) {
            var mouse = API.getCursorPositionMaintainRatio();
            this.x = mouse.X - Math.round(itemSize / 2);
            this.y = mouse.Y - Math.round(itemSize / 2);
            this.calculateCenterPoints();
            resource.Utility.setHand(true);
        }
        else {
            resource.Utility.setHand(false);
            // If our mouse isn't pressed down drop our item.'
            this.selected = false;
            currentSelection = null;
            var closestBox = this.findClosestBoxToPointer();
            // Check if a box exists where we dropped it.
            if (closestBox === null) {
                if (debugMode) {
                    API.sendChatMessage("Spot does not exist, dropping item.");
                }
                this.dropItem();
                return;
            }
            // If it does exist, let's place our object down or reposition it.
            if (closestBox.BoxItem === null) {
                if (debugMode) {
                    API.sendChatMessage("Found open Box spot. Attempting placement.");
                }
                // Setup our new found spot.
                closestBox.BoxItem = this;
                this.box = closestBox;
                // Get the coords and reposition for center point.
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
    };
    // Remove Item
    Item.prototype.removeItem = function () {
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
    };
    // Used to drop the item. General idea of how it works. We loop through our array, find the index that matches this. Then we take that index splice it out of our array, and then we drop our item.
    Item.prototype.dropItem = function () {
        this.removeItem();
        var playerPos = API.getEntityPosition(API.getLocalPlayer());
        var aimCoords = API.getPlayerAimCoords(API.getLocalPlayer());
        API.playSoundFrontEnd("Place_Prop_Fail", "DLC_Dmod_Prop_Editor_Sounds");
        // As a bonus we check if the aim coordinate is a viable position. If not we'll just drop it at ground height.'
        if (playerPos.DistanceTo(aimCoords) <= 6) {
            API.triggerServerEvent("DROP_ITEM", this.id, this.type, aimCoords, this.quantity);
        }
        else {
            var groundHeight = API.getGroundHeight(playerPos);
            var newPos = new Vector3(playerPos.X, playerPos.Y, groundHeight);
            API.triggerServerEvent("DROP_ITEM", this.id, this.type, newPos, this.quantity);
        }
    };
    return Item;
}());
API.onResourceStart.connect(function () {
    grid = new Grid(5, 8, 150, 300, 50, 10);
    //grid = new Grid(5, 5, 50, new Point(Math.round(API.getScreenResolutionMaintainRatio().Width / 2) - 125, 25));
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
API.onUpdate.connect(function () {
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
//# sourceMappingURL=Inventory.js.map