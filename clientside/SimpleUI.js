// Default sizes.
var height = 50;
var width = 500;
var headerHeight = 100;
// Current Menu Draw
var contentHolder = null;
API.onKeyDown.connect(function (sender, e) {
    if (contentHolder === null) {
        return;
    }
    if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) {
        contentHolder.scrollDown();
        API.playSoundFrontEnd("NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET");
    }
    if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) {
        contentHolder.scrollUp();
        API.playSoundFrontEnd("NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET");
    }
    if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F) {
        contentHolder.ContentItems[contentHolder.CurrentSelection].runSelectFunction();
        API.playSoundFrontEnd("SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET");
    }
    if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) {
        contentHolder.ContentItems[contentHolder.CurrentSelection].previousContent();
    }
    if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) {
        contentHolder.ContentItems[contentHolder.CurrentSelection].nextContent();
    }
});
API.onUpdate.connect(function () {
    if (contentHolder != null) {
        API.disableAllControlsThisFrame();
        contentHolder.draw();
    }
});
// Return a ContentHolder for a menu.
function setupContent() {
    contentHolder = new ContentHolder();
    return contentHolder;
}
// Get the current content holder as a return.
function getContentHolder() {
    return contentHolder;
}
// Clear Menu
function clearContent() {
    contentHolder = null;
}
var ContentHolder = (function () {
    function ContentHolder(point) {
        if (point === void 0) { point = new Point(0, 0); }
        this.point = point;
        this.contentHeader = null;
        this.contentItems = new Array();
        this.currentSelection = 0;
        this.isReady = false;
    }
    ContentHolder.prototype.draw = function () {
        if (!this.isReady) {
            return;
        }
        if (this.contentHeader != null) {
            this.contentHeader.draw();
        }
        if (this.contentItems.length <= 0) {
            return;
        }
        if (this.contentItems.length >= 10) {
            if (this.currentSelection + 10 >= this.contentItems.length) {
                this.drawToMaximum();
            }
            else {
                this.drawToAmount(10);
            }
        }
        else {
            this.drawAll();
        }
    };
    Object.defineProperty(ContentHolder.prototype, "IsReady", {
        set: function (value) {
            this.isReady = value;
        },
        enumerable: true,
        configurable: true
    });
    ContentHolder.prototype.drawAll = function () {
        for (var i = 0; i < this.contentItems.length; i++) {
            if (i === this.currentSelection) {
                this.contentItems[i].draw(i + 1);
                this.contentItems[i].IsSelected = true;
            }
            else {
                this.contentItems[i].draw(i + 1);
                this.contentItems[i].IsSelected = false;
            }
        }
    };
    ContentHolder.prototype.drawToMaximum = function () {
        for (var i = this.contentItems.length - 10; i < this.contentItems.length; i++) {
            if (i === this.currentSelection) {
                this.contentItems[i].draw(i - this.contentItems.length + 11);
                this.contentItems[i].IsSelected = true;
            }
            else {
                this.contentItems[i].draw(i - this.contentItems.length + 11);
                this.contentItems[i].IsSelected = false;
            }
        }
    };
    ContentHolder.prototype.drawToAmount = function (amount) {
        for (var i = this.currentSelection; i < this.currentSelection + 10; i++) {
            if (i === this.currentSelection) {
                this.contentItems[i].draw(1);
                this.contentItems[i].IsSelected = true;
            }
            else {
                this.contentItems[i].draw(i - this.currentSelection + 1);
                this.contentItems[i].IsSelected = false;
            }
        }
    };
    ContentHolder.prototype.createHeader = function () {
        this.contentHeader = new ContentHeader(this);
        return this.contentHeader;
    };
    ContentHolder.prototype.addItem = function (itemName) {
        var contentItem = new ContentItem(this, itemName);
        this.contentItems.push(contentItem);
        return contentItem;
    };
    ContentHolder.prototype.scrollDown = function () {
        if (this.currentSelection <= 0) {
            this.currentSelection = this.contentItems.length - 1;
        }
        else {
            this.currentSelection -= 1;
        }
        this.contentItems[this.currentSelection].runHoverFunction();
    };
    ContentHolder.prototype.scrollUp = function () {
        if (this.currentSelection + 1 >= this.contentItems.length) {
            this.currentSelection = 0;
        }
        else {
            this.currentSelection += 1;
        }
        this.contentItems[this.currentSelection].runHoverFunction();
    };
    Object.defineProperty(ContentHolder.prototype, "CurrentSelection", {
        get: function () {
            return this.currentSelection;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ContentHolder.prototype, "Point", {
        get: function () {
            return this.point;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ContentHolder.prototype, "ContentItems", {
        get: function () {
            return this.contentItems;
        },
        enumerable: true,
        configurable: true
    });
    return ContentHolder;
}());
var ContentHeader = (function () {
    function ContentHeader(contentHolder) {
        this.contentHolder = contentHolder;
        this.path = null;
        this.dict = null;
        this.texture = null;
    }
    // Set the header a png or jpg.
    ContentHeader.prototype.setLocalImage = function (path) {
        this.path = path;
    };
    // Set the header to a game texture.
    ContentHeader.prototype.setGameTexture = function (dict, texture) {
        this.dict = dict;
        this.texture = texture;
    };
    ContentHeader.prototype.draw = function () {
        if (this.dict != null && this.texture != null) {
            API.drawGameTexture(this.dict, this.texture, this.contentHolder.Point.X, this.contentHolder.Point.Y, width, headerHeight, 0, 255, 255, 255, 255);
        }
        if (this.path != null) {
            API.dxDrawTexture(this.path, this.contentHolder.Point, new Size(width, headerHeight), 0);
        }
    };
    return ContentHeader;
}());
var ContentItem = (function () {
    function ContentItem(contentHolder, itemName) {
        this.contentHolder = contentHolder;
        this.itemName = itemName;
        this.itemPrice = "-1";
        this.color = [255, 255, 255];
        this.id = null;
        this.isSelected = false;
        this.getID();
        this.hoverFunction = null;
        this.selectFunction = null;
        this.description = null;
        this.isVariedContent = false;
        this.variedContents = new Array();
        this.currentSelection = 0;
    }
    // Get the ID of this item.
    ContentItem.prototype.getID = function () {
        this.id = this.contentHolder.ContentItems.length + 1;
    };
    // Used to turn on left and right scrolling, and add new items. Returns the item you just added.
    ContentItem.prototype.addVariedContentItem = function (contentHolder, itemName) {
        if (!this.isVariedContent) {
            this.itemName = "< " + this.itemName + " >";
        }
        var item = new ContentItem(contentHolder, itemName);
        this.isVariedContent = true;
        this.variedContents.push(item);
        return item;
    };
    // Next content display.
    ContentItem.prototype.nextContent = function () {
        if (!this.isVariedContent) {
            return;
        }
        API.playSoundFrontEnd("NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET");
        if (this.currentSelection + 1 > this.variedContents.length) {
            this.currentSelection = 0;
        }
        else {
            this.currentSelection += 1;
        }
        this.setTextToCurrentContent();
        this.runVariedHoverFunction();
    };
    // Previous content display.
    ContentItem.prototype.previousContent = function () {
        if (!this.isVariedContent) {
            return;
        }
        API.playSoundFrontEnd("NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET");
        if (this.currentSelection - 1 <= -1) {
            this.currentSelection = this.variedContents.length;
        }
        else {
            this.currentSelection -= 1;
        }
        this.setTextToCurrentContent();
        this.runVariedHoverFunction();
    };
    //
    ContentItem.prototype.runVariedHoverFunction = function () {
        this.variedContents[this.currentSelection].runHoverFunction();
    };
    // Set the text from other content.
    ContentItem.prototype.setTextToCurrentContent = function () {
        this.itemName = "< " + this.variedContents[this.currentSelection].Text + " >";
    };
    Object.defineProperty(ContentItem.prototype, "IsSelected", {
        // Is this item currently selected?
        set: function (value) {
            this.isSelected = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ContentItem.prototype, "ItemPrice", {
        // Set the item price of this item.
        set: function (value) {
            this.itemPrice = value;
        },
        enumerable: true,
        configurable: true
    });
    // Used to draw the text, images, etc.
    ContentItem.prototype.draw = function (currentOffset) {
        this.drawText(currentOffset);
        this.drawBackground(currentOffset);
    };
    // Used for hovering.
    ContentItem.prototype.runHoverFunction = function () {
        if (this.hoverFunction != null) {
            this.hoverFunction.run();
        }
    };
    // Used when the F or Enter key is pressed.
    ContentItem.prototype.runSelectFunction = function () {
        if (this.selectFunction != null) {
            this.selectFunction.run();
        }
        if (this.isVariedContent) {
            if (this.variedContents[this.currentSelection].selectFunction != null) {
                this.variedContents[this.currentSelection].runSelectFunction();
            }
        }
    };
    Object.defineProperty(ContentItem.prototype, "CurrentSelection", {
        // Current Selection
        set: function (value) {
            this.currentSelection = value;
        },
        enumerable: true,
        configurable: true
    });
    // Used to set the text color.
    ContentItem.prototype.setTextColor = function (r, g, b) {
        this.color = [r, g, b];
    };
    Object.defineProperty(ContentItem.prototype, "HoverFunction", {
        // Attach a FunctionHolder to this Item. Fires when it gets hovered.
        set: function (value) {
            this.hoverFunction = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ContentItem.prototype, "Description", {
        // Attach a description to this Item
        set: function (value) {
            this.description = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ContentItem.prototype, "SelectFunction", {
        // Attach a selective FunctionHolder to this item. Fires when the action key is pressed.
        set: function (value) {
            this.selectFunction = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ContentItem.prototype, "Text", {
        // Get Text
        get: function () {
            return this.itemName;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ContentItem.prototype, "Price", {
        // Get Price
        get: function () {
            return this.itemPrice;
        },
        enumerable: true,
        configurable: true
    });
    // Draw the background.
    ContentItem.prototype.drawBackground = function (currentOffset) {
        if (this.isSelected) {
            API.drawRectangle(this.contentHolder.Point.X, headerHeight + (height * currentOffset) - height, width, height, 255, 255, 255, 150);
        }
        else {
            API.drawRectangle(this.contentHolder.Point.X, headerHeight + (height * currentOffset) - height, width, height, 0, 0, 0, 150);
        }
    };
    // DRaw the text.
    ContentItem.prototype.drawText = function (currentOffset) {
        if (this.isSelected) {
            API.drawText(this.itemName, this.contentHolder.Point.X + 10, headerHeight + Math.round(height * currentOffset) - (Math.round(height / 2)) - 12, 0.4, 0, 0, 0, 255, 4, 0, false, false, 500);
        }
        else {
            API.drawText(this.itemName, this.contentHolder.Point.X + 10, headerHeight + Math.round(height * currentOffset) - (Math.round(height / 2)) - 12, 0.4, this.color[0], this.color[1], this.color[2], 255, 4, 0, false, false, 500);
        }
        if (this.itemPrice != "-1") {
            API.drawText("$" + this.itemPrice, width - 30, headerHeight + Math.round(height * currentOffset) - (Math.round(height / 2)) - 12, 0.4, this.color[0], this.color[1], this.color[2], 255, 4, 1, false, false, 500);
        }
        this.drawDescription();
    };
    // Draw the description text.
    ContentItem.prototype.drawDescription = function () {
        if (!this.isSelected) {
            return;
        }
        if (this.description != null) {
            API.drawText(this.description, this.contentHolder.Point.X + 10, headerHeight + Math.round(height * 10) + 12, 0.4, this.color[0], this.color[1], this.color[2], 255, 4, 0, false, false, 500);
            API.drawRectangle(this.contentHolder.Point.X, headerHeight + Math.round(height * 10), width, height, 0, 0, 0, 200);
        }
    };
    ContentItem.prototype.getHoverFunction = function () {
        this.hoverFunction = new FunctionHolder();
        return this.hoverFunction;
    };
    ContentItem.prototype.getSelectFunction = function () {
        this.selectFunction = new FunctionHolder();
        return this.selectFunction;
    };
    return ContentItem;
}());
var FunctionHolder = (function () {
    function FunctionHolder() {
        this.localFunction = null;
        this.localArgs = null;
    }
    Object.defineProperty(FunctionHolder.prototype, "Function", {
        set: function (value) {
            this.localFunction = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(FunctionHolder.prototype, "Args", {
        set: function (value) {
            this.localArgs = value;
        },
        enumerable: true,
        configurable: true
    });
    FunctionHolder.prototype.run = function () {
        if (this.localFunction != null) {
            if (this.localArgs != null) {
                this.localFunction(this.localArgs);
            }
            else {
                this.localFunction();
            }
        }
    };
    return FunctionHolder;
}());
//# sourceMappingURL=SimpleUI.js.map