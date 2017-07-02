var grid: Grid = null;
var items: Array<InventoryItem> = new Array<InventoryItem>();
var selected: InventoryItem = null;
var opened = false;

class Grid {
    private grids: Array<GridBlock>;
    private defaultBlockWidth: number;
    private defaultBlockHeight: number;
    private gridLength: number;
    constructor(rows: number, columns: number, blockSize: number, startPoint: Point) {
        this.grids = new Array<GridBlock>();
        for (var r = 0; r < rows; r++) {
            for (var i = 0; i < columns; i++) {
                if (r === 0) {
                    var gridBlock = new GridBlock(blockSize, new Point((200 * i) + startPoint.X, startPoint.Y));
                    this.grids.push(gridBlock);
                } else {
                    var gridBlock = new GridBlock(blockSize, new Point((200 * i) + startPoint.X, (200 * r) + startPoint.Y));
                    this.grids.push(gridBlock);
                }
                
            }
        }
        // This is for performance.
        this.gridLength = this.grids.length;
    }

    get GridBlocks(): Array<GridBlock> {
        return this.grids;
    }
}

class GridBlock {
    private size: number;
    private point: Point;
    private gridBlockTaken: boolean;
    constructor(size: number, point: Point) {
        this.size = size;
        this.point = point;
        this.gridBlockTaken = false;
    }

    get BlockSize(): number {
        return this.size;
    }

    get BlockPoint(): Point {
        return this.point;
    }

    set Taken(value: boolean) {
        this.gridBlockTaken = value;
    }

    get Taken(): boolean {
        return this.gridBlockTaken;
    }
}


class InventoryItem {
    private x: number;
    private y: number;
    private width: number;
    private height: number;
    private attached: boolean;
    private snapped: boolean;
    private type: string;
    private quantity: number;
    constructor(type: string, quantity: number) {
        this.x = Math.round(API.getScreenResolutionMantainRatio().Width / 2);
        this.y = Math.round(API.getScreenResolutionMantainRatio().Height / 2);
        this.width = 190;
        this.height = 190;
        this.snapped = true;
        this.type = type;
        this.quantity = quantity;
        this.rePosition();
    }

    public draw() {
        if (!API.isCursorShown()) {
            API.showCursor(true);
        }

        var mouse = API.getCursorPositionMantainRatio();
        API.drawRectangle(mouse.X, mouse.Y, 5, 5, 150, 150, 150, 255);

        if (this.snapped) {
            API.drawText("" + this.type, Math.round(this.x + (this.width / 2)), Math.round(this.y + (this.height / 2)) - 10, 0.5, 255, 255, 255, 255, 4, 1, false, true, 500);
            API.drawText("x" + this.quantity.toString(), Math.round(this.x + (this.width / 2)), Math.round(this.y + (this.height / 2)) + 30, 0.3, 255, 255, 255, 255, 4, 1, false, true, 500);
            //API.drawRectangle(this.x, this.y, this.width, this.height, 225, 225, 225, 200);

            if (this.attached) {
                this.move();
            } else {
                this.getSelection();
                //this.gravity();
            }
        }
    }

    private rePosition() {
        var grids = grid.GridBlocks;
        if (grids.length <= 0) {
            return;
        }

        for (var i = 0; i < grids.length; i++) {
            var gridSize = grids[i].BlockSize;
            var gridX = grids[i].BlockPoint.X;
            var gridY = grids[i].BlockPoint.Y;

            var blockX = Math.round(this.x + (this.width / 2));
            var blockY = Math.round(this.y + (this.height / 2))

            if (!grids[i].Taken) {
                this.x = Math.round(gridX + (gridSize / 2) - (this.width / 2))
                this.y = Math.round(gridY + (gridSize / 2) - (this.height / 2))
                this.snapped = true;
                grids[i].Taken = true;
                return;
            }
        }

        this.snapped = false;
        this.dropItem();
    }

    private move() {
        if (selected != this) {
            this.attached = false;
            return;
        }

        if (!API.isControlPressed(Enums.Controls.CursorAccept)) {
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
                    var blockY = Math.round(this.y + (this.height / 2))


                    if (blockX <= grids[i].BlockPoint.X + gridSize && blockX >= grids[i].BlockPoint.X && blockY <= grids[i].BlockPoint.Y + gridSize && blockY >= grids[i].BlockPoint.Y) {
                        // Check if our grid point is available.
                        if (!grids[i].Taken) {
                            this.x = Math.round(gridX + (gridSize / 2) - (this.width / 2))
                            this.y = Math.round(gridY + (gridSize / 2) - (this.height / 2))
                            this.snapped = true;
                            grids[i].Taken = true;
                            return;
                        // If not put it in our next available.
                        } else {
                            this.rePosition();
                            return;
                        }
                    }
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

    private dropItem() {
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
        API.triggerServerEvent("DROP_ITEM", this.type);
    }

    private getSelection() {
        if (selected !== null) {
            return;
        }

        if (!API.isControlPressed(Enums.Controls.CursorAccept)) {
            return;
        }

        if (!this.mouseCheck()) {
            return;
        }

        selected = this;
        this.attached = true;
        this.removeTaken();
    }

    private mouseCheck() {
        var mouse = API.getCursorPositionMantainRatio();
        if (mouse.X > this.x && mouse.X < this.x + this.width && mouse.Y > this.y && mouse.Y < this.y + this.height) {
            return true;
        }
        return false;
    }

    private removeTaken() {
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
})

function toggleInventory() {
    if (opened) {
        opened = false;
        API.showCursor(false);
        items = [];
        var gridBlocks = grid.GridBlocks;
        for (var i = 0; i < gridBlocks.length; i++) {
            gridBlocks[i].Taken = false;
        }
    } else {
        opened = true;
        API.triggerServerEvent("GET_ITEMS");
        API.showCursor(true);
    }
}

function addInventoryItem(type: string, quantity: number) {
    items.push(new InventoryItem(type, quantity));
}

API.onUpdate.connect(() => {
    if (!opened) {
        return;
    }

    if (items.length <= 0) {
        return;
    }

    for (var i = 0; i < items.length; i++) {
        items[i].draw();
    }
});