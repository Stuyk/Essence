"use strict";
var height = 50;
var width = 500;
var headerHeight = 100;
var contentHolder = null;
API.onKeyDown.connect((sender, e) => {
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
API.onUpdate.connect(() => {
    if (contentHolder != null) {
        API.disableAllControlsThisFrame();
        contentHolder.draw();
    }
});
function setupContent() {
    contentHolder = new ContentHolder();
    return contentHolder;
}
function getContentHolder() {
    return contentHolder;
}
function clearContent() {
    contentHolder = null;
}
class ContentHolder {
    constructor(point = new Point(0, 0)) {
        this.point = point;
        this.contentHeader = null;
        this.contentItems = new Array();
        this.currentSelection = 0;
        this.isReady = false;
    }
    draw() {
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
    }
    set IsReady(value) {
        this.isReady = value;
    }
    drawAll() {
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
    }
    drawToMaximum() {
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
    }
    drawToAmount(amount) {
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
    }
    createHeader() {
        this.contentHeader = new ContentHeader(this);
        return this.contentHeader;
    }
    addItem(itemName) {
        var contentItem = new ContentItem(this, itemName);
        this.contentItems.push(contentItem);
        return contentItem;
    }
    scrollDown() {
        if (this.currentSelection <= 0) {
            this.currentSelection = this.contentItems.length - 1;
        }
        else {
            this.currentSelection -= 1;
        }
        this.contentItems[this.currentSelection].runHoverFunction();
    }
    scrollUp() {
        if (this.currentSelection + 1 >= this.contentItems.length) {
            this.currentSelection = 0;
        }
        else {
            this.currentSelection += 1;
        }
        this.contentItems[this.currentSelection].runHoverFunction();
    }
    get CurrentSelection() {
        return this.currentSelection;
    }
    get Point() {
        return this.point;
    }
    get ContentItems() {
        return this.contentItems;
    }
}
class ContentHeader {
    constructor(contentHolder) {
        this.contentHolder = contentHolder;
        this.path = null;
        this.dict = null;
        this.texture = null;
    }
    setLocalImage(path) {
        this.path = path;
    }
    setGameTexture(dict, texture) {
        this.dict = dict;
        this.texture = texture;
    }
    draw() {
        if (this.dict != null && this.texture != null) {
            API.drawGameTexture(this.dict, this.texture, this.contentHolder.Point.X, this.contentHolder.Point.Y, width, headerHeight, 0, 255, 255, 255, 255);
        }
        if (this.path != null) {
            API.dxDrawTexture(this.path, this.contentHolder.Point, new Size(width, headerHeight), 0);
        }
    }
}
class ContentItem {
    constructor(contentHolder, itemName) {
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
    getID() {
        this.id = this.contentHolder.ContentItems.length + 1;
    }
    addVariedContentItem(contentHolder, itemName) {
        if (!this.isVariedContent) {
            this.itemName = `< ${this.itemName} >`;
        }
        var item = new ContentItem(contentHolder, itemName);
        this.isVariedContent = true;
        this.variedContents.push(item);
        return item;
    }
    nextContent() {
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
    }
    previousContent() {
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
    }
    runVariedHoverFunction() {
        this.variedContents[this.currentSelection].runHoverFunction();
    }
    setTextToCurrentContent() {
        this.itemName = `< ${this.variedContents[this.currentSelection].Text} >`;
    }
    set IsSelected(value) {
        this.isSelected = value;
    }
    set ItemPrice(value) {
        this.itemPrice = value;
    }
    draw(currentOffset) {
        this.drawText(currentOffset);
        this.drawBackground(currentOffset);
    }
    runHoverFunction() {
        if (this.hoverFunction != null) {
            this.hoverFunction.run();
        }
    }
    runSelectFunction() {
        if (this.selectFunction != null) {
            this.selectFunction.run();
        }
        if (this.isVariedContent) {
            if (this.variedContents[this.currentSelection].selectFunction != null) {
                this.variedContents[this.currentSelection].runSelectFunction();
            }
        }
    }
    set CurrentSelection(value) {
        this.currentSelection = value;
    }
    setTextColor(r, g, b) {
        this.color = [r, g, b];
    }
    set HoverFunction(value) {
        this.hoverFunction = value;
    }
    set Description(value) {
        this.description = value;
    }
    set SelectFunction(value) {
        this.selectFunction = value;
    }
    get Text() {
        return this.itemName;
    }
    get Price() {
        return this.itemPrice;
    }
    drawBackground(currentOffset) {
        if (this.isSelected) {
            API.drawRectangle(this.contentHolder.Point.X, headerHeight + (height * currentOffset) - height, width, height, 255, 255, 255, 150);
        }
        else {
            API.drawRectangle(this.contentHolder.Point.X, headerHeight + (height * currentOffset) - height, width, height, 0, 0, 0, 150);
        }
    }
    drawText(currentOffset) {
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
    }
    drawDescription() {
        if (!this.isSelected) {
            return;
        }
        if (this.description != null) {
            API.drawText(this.description, this.contentHolder.Point.X + 10, headerHeight + Math.round(height * 10) + 12, 0.4, this.color[0], this.color[1], this.color[2], 255, 4, 0, false, false, 500);
            API.drawRectangle(this.contentHolder.Point.X, headerHeight + Math.round(height * 10), width, height, 0, 0, 0, 200);
        }
    }
    getHoverFunction() {
        this.hoverFunction = new FunctionHolder();
        return this.hoverFunction;
    }
    getSelectFunction() {
        this.selectFunction = new FunctionHolder();
        return this.selectFunction;
    }
}
class FunctionHolder {
    constructor() {
        this.localFunction = null;
        this.localArgs = null;
    }
    set Function(value) {
        this.localFunction = value;
    }
    set Args(value) {
        this.localArgs = value;
    }
    run() {
        if (this.localFunction != null) {
            if (this.localArgs != null) {
                this.localFunction(this.localArgs);
            }
            else {
                this.localFunction();
            }
        }
    }
}
