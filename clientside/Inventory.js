"use strict";
var grid = null;
var items = new Array();
var selected = null;
var opened = false;
class Grid {
    constructor(rows, columns, blockSize, startPoint) {
        this.grids = new Array();
        for (var r = 0; r < rows; r++) {
            for (var i = 0; i < columns; i++) {
                if (r === 0) {
                    var gridBlock = new GridBlock(blockSize, new Point((200 * i) + startPoint.X, startPoint.Y));
                    this.grids.push(gridBlock);
                }
                else {
                    var gridBlock = new GridBlock(blockSize, new Point((200 * i) + startPoint.X, (200 * r) + startPoint.Y));
                    this.grids.push(gridBlock);
                }
            }
        }
        // This is for performance.
        this.gridLength = this.grids.length;
    }
    get GridBlocks() {
        return this.grids;
    }
}
class GridBlock {
    constructor(size, point) {
        this.size = size;
        this.point = point;
        this.gridBlockTaken = false;
    }
    get BlockSize() {
        return this.size;
    }
    get BlockPoint() {
        return this.point;
    }
    set Taken(value) {
        this.gridBlockTaken = value;
    }
    get Taken() {
        return this.gridBlockTaken;
    }
}
class InventoryItem {
    constructor(type, quantity) {
        this.x = Math.round(API.getScreenResolutionMantainRatio().Width / 2);
        this.y = Math.round(API.getScreenResolutionMantainRatio().Height / 2);
        this.width = 190;
        this.height = 190;
        this.snapped = true;
        this.type = type;
        this.quantity = quantity;
        this.rePosition();
    }
    draw() {
        if (!API.isCursorShown()) {
            API.showCursor(true);
        }
        var mouse = API.getCursorPositionMantainRatio();
        API.drawRectangle(mouse.X, mouse.Y, 5, 5, 150, 150, 150, 255);
        if (this.snapped) {
            API.drawText("" + this.type, Math.round(this.x + (this.width / 2)), Math.round(this.y + (this.height / 2)) - 10, 0.5, 255, 255, 255, 255, 4, 1, false, true, 500);
            API.drawText("x" + this.quantity.toString(), Math.round(this.x + (this.width / 2)), Math.round(this.y + (this.height / 2)) + 30, 0.3, 255, 255, 255, 255, 4, 1, false, true, 500);
            if (this.attached) {
                this.move();
            }
            else {
                this.getSelection();
            }
        }
    }
    rePosition() {
        var grids = grid.GridBlocks;
        if (grids.length <= 0) {
            return;
        }
        for (var i = 0; i < grids.length; i++) {
            var gridSize = grids[i].BlockSize;
            var gridX = grids[i].BlockPoint.X;
            var gridY = grids[i].BlockPoint.Y;
            var blockX = Math.round(this.x + (this.width / 2));
            var blockY = Math.round(this.y + (this.height / 2));
            if (!grids[i].Taken) {
                this.x = Math.round(gridX + (gridSize / 2) - (this.width / 2));
                this.y = Math.round(gridY + (gridSize / 2) - (this.height / 2));
                this.snapped = true;
                grids[i].Taken = true;
                return;
            }
        }
        if (!this.isAimPointValid()) {
            return;
        }
        this.snapped = false;
        this.dropItem();
    }
    move() {
        if (selected != this) {
            this.attached = false;
            return;
        }
        if (!API.isControlPressed(237 /* CursorAccept */)) {
            this.attached = false;
            selected = null;
            if (grid != null) {
                var grids = grid.GridBlocks;
                if (grids.length <= 0) {
                    return;
                }
                for (var i = 0; i < grids.length; i++) {
                    var gridSize = grids[i].BlockSize;
                    var gridX = grids[i].BlockPoint.X;
                    var gridY = grids[i].BlockPoint.Y;
                    var blockX = Math.round(this.x + (this.width / 2));
                    var blockY = Math.round(this.y + (this.height / 2));
                    if (blockX <= grids[i].BlockPoint.X + gridSize && blockX >= grids[i].BlockPoint.X && blockY <= grids[i].BlockPoint.Y + gridSize && blockY >= grids[i].BlockPoint.Y) {
                        // Check if our grid point is available.
                        if (!grids[i].Taken) {
                            this.x = Math.round(gridX + (gridSize / 2) - (this.width / 2));
                            this.y = Math.round(gridY + (gridSize / 2) - (this.height / 2));
                            this.snapped = true;
                            grids[i].Taken = true;
                            return;
                            // If not put it in our next available.
                        }
                        else {
                            this.rePosition();
                            return;
                        }
                    }
                }
                // Check if the player's distance is proper for dropping the item.
                if (!this.isAimPointValid()) {
                    return;
                }
                this.snapped = false;
                this.dropItem();
                return;
            }
            return;
        }
        var mouse = API.getCursorPositionMantainRatio();
        this.x = mouse.X - Math.round(this.width / 2);
        this.y = mouse.Y - Math.round(this.height / 2);
    }
    dropItem() {
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
        var aimCoords = API.getPlayerAimCoords(API.getLocalPlayer());
        API.triggerServerEvent("DROP_ITEM", this.type, aimCoords);
    }
    getSelection() {
        if (selected !== null) {
            return;
        }
        if (!API.isControlPressed(237 /* CursorAccept */)) {
            return;
        }
        if (!this.mouseCheck()) {
            return;
        }
        selected = this;
        this.attached = true;
        this.removeTaken();
    }
    mouseCheck() {
        var mouse = API.getCursorPositionMantainRatio();
        if (mouse.X > this.x && mouse.X < this.x + this.width && mouse.Y > this.y && mouse.Y < this.y + this.height) {
            return true;
        }
        return false;
    }
    isAimPointValid() {
        var aimPos = API.getPlayerAimCoords(API.getLocalPlayer());
        var playerPOS = API.getEntityPosition(API.getLocalPlayer());
        if (playerPOS.DistanceTo(aimPos) <= 5) {
            return true;
        }
        return false;
    }
    removeTaken() {
        var mouse = API.getCursorPositionMantainRatio();
        var grids = grid.GridBlocks;
        if (grids.length <= 0) {
            return;
        }
        for (var i = 0; i < grids.length; i++) {
            var gridSize = grids[i].BlockSize;
            var gridX = grids[i].BlockPoint.X;
            var gridY = grids[i].BlockPoint.Y;
            if (mouse.X > gridX && mouse.X < gridX + gridSize && mouse.Y > gridY && mouse.Y < gridY + gridSize) {
                grids[i].Taken = false;
                return;
            }
        }
    }
}
API.onResourceStart.connect(() => {
    grid = new Grid(5, 5, 200, new Point(Math.round(API.getScreenResolutionMantainRatio().Width / 2) - 500, 25));
});
function toggleInventory() {
    if (opened) {
        opened = false;
        API.showCursor(false);
        items = [];
        var gridBlocks = grid.GridBlocks;
        for (var i = 0; i < gridBlocks.length; i++) {
            gridBlocks[i].Taken = false;
        }
    }
    else {
        opened = true;
        API.triggerServerEvent("GET_ITEMS");
        API.showCursor(true);
    }
}
function addInventoryItem(type, quantity) {
    items.push(new InventoryItem(type, quantity));
}
API.onUpdate.connect(() => {
    if (!opened) {
        return;
    }
    for (var i = 0; i < items.length; i++) {
        items[i].draw();
    }
});
