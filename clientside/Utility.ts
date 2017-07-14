var cursorPath = "clientside/images/cursor/cursor.png";
var handPath = "clientside/images/cursor/hand.png";
var cursorEnabled = false;
var handEnabled = false;

API.onUpdate.connect(() => {
    drawCursor();
});
// #######################
//  CURSOR FUNCTIONS
// #######################
function toggleCursor() {
    if (!cursorEnabled) {
        cursorEnabled = true;
    } else {
        cursorEnabled = false;
    }
}
function setCursor(value: boolean) {
    cursorEnabled = value;
}
function isCursorEnables() {
    return cursorEnabled;
}
function setHand(value: boolean) {
    handEnabled = value;
}
function drawCursor() {
    if (!cursorEnabled) {
        return;
    }
    API.disableAllControlsThisFrame();
    var mouse = API.getCursorPositionMantainRatio();
    if (handEnabled) {
        API.dxDrawTexture(handPath, Point.Round(mouse), new Size(12, 19), 0, 255, 255, 255, 255);
    } else {
        API.dxDrawTexture(cursorPath, Point.Round(mouse), new Size(12, 19), 0, 255, 255, 255, 255);
    }
    
}