"use strict";
var cursorPath = "clientside/images/cursor/cursor.png";
var handPath = "clientside/images/cursor/hand.png";
var cursorEnabled = false;
var handEnabled = false;
API.onUpdate.connect(() => {
    drawCursor();
});
function toggleCursor() {
    if (!cursorEnabled) {
        cursorEnabled = true;
    }
    else {
        cursorEnabled = false;
    }
}
function setCursor(value) {
    cursorEnabled = value;
}
function isCursorEnables() {
    return cursorEnabled;
}
function setHand(value) {
    handEnabled = value;
}
function drawCursor() {
    if (!cursorEnabled) {
        return;
    }
    API.disableAllControlsThisFrame();
    var mouse = API.getCursorPositionMaintainRatio();
    if (handEnabled) {
        API.dxDrawTexture(handPath, new Point(Math.round(mouse.X), Math.round(mouse.Y)), new Size(12, 19), 0);
    }
    else {
        API.dxDrawTexture(cursorPath, new Point(Math.round(mouse.X), Math.round(mouse.Y)), new Size(12, 19), 0);
    }
}
