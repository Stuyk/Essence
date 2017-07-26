"use strict";
var screenRes = API.getScreenResolutionMaintainRatio();
var widthHalf = Math.round(screenRes.Width / 2);
var heightHalf = Math.round(screenRes.Height / 2);
var holdCounter = 0;
var vehicleMenuButtons = new Array();
var playerMenuButtons = new Array();
var playerAnimationMenuButtons = new Array();
API.onResourceStart.connect(() => {
    vehicleLeftMenus();
    vehicleCenterMenus();
    vehicleRightMenus();
    animationCenterMenus();
});
function vehicleCenterMenus() {
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf - 100), new Size(200, 50), "Vehicle_Hood", "Hood"));
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf - 25), new Size(200, 50), "Vehicle_Engine", "Engine"));
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf + 50), new Size(200, 50), "Vehicle_Trunk", "Trunk"));
}
function vehicleLeftMenus() {
    var DriverDoor = new InteractionButton(new Point(widthHalf - 325, heightHalf - 100), new Size(200, 50), "Vehicle_Door", "Driver Door");
    DriverDoor.Argument = 0;
    vehicleMenuButtons.push(DriverDoor);
    var DriverRearDoor = new InteractionButton(new Point(widthHalf - 325, heightHalf - 25), new Size(200, 50), "Vehicle_Door", "Driver Rear Door");
    DriverRearDoor.Argument = 2;
    vehicleMenuButtons.push(DriverRearDoor);
    var WindowsDown = new InteractionButton(new Point(widthHalf - 325, heightHalf + 50), new Size(200, 50), "Vehicle_Windows_Down", "Windows Down");
    WindowsDown.Argument = true;
    vehicleMenuButtons.push(WindowsDown);
}
function vehicleRightMenus() {
    var PassengerDoor = new InteractionButton(new Point(widthHalf + 125, heightHalf - 100), new Size(200, 50), "Vehicle_Door", "Passenger Door");
    PassengerDoor.Argument = 1;
    vehicleMenuButtons.push(PassengerDoor);
    var PassengerRearDoor = new InteractionButton(new Point(widthHalf + 125, heightHalf - 25), new Size(200, 50), "Vehicle_Door", "Passenger Rear Door");
    PassengerRearDoor.Argument = 3;
    vehicleMenuButtons.push(PassengerRearDoor);
    var WindowsUp = new InteractionButton(new Point(widthHalf + 125, heightHalf + 50), new Size(200, 50), "Vehicle_Windows_Up", "Windows Up");
    WindowsUp.Argument = false;
    vehicleMenuButtons.push(WindowsUp);
}
function animationCenterMenus() {
    playerAnimationMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf - 400), new Size(200, 50), "ANIM_GESTURE_COME_HERE", "Come Here"));
    playerAnimationMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf - 325), new Size(200, 50), "ANIM_GESTURE_HELLO", "Hello"));
    playerAnimationMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf - 250), new Size(200, 50), "ANIM_NOD_YES", "Yes"));
    playerAnimationMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf - 175), new Size(200, 50), "ANIM_NOD_NO", "No"));
    playerAnimationMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf - 100), new Size(200, 50), "ANIM_GESTURE_SHRUG_HARD", "Shrug"));
    var interact = new InteractionButton(new Point(widthHalf - 100, heightHalf - 25), new Size(200, 50), "ANIM_STOP", "Stop Animation");
    interact.setRGB(255, 0, 0);
    playerAnimationMenuButtons.push(interact);
    playerAnimationMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf + 50), new Size(200, 50), "ANIM_CROUCH", "Crouch"));
    playerAnimationMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf + 125), new Size(200, 50), "ANIM_GESTURE_POINT", "Point"));
    playerAnimationMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf + 200), new Size(200, 50), "ANIM_GESTURE_DAMN", "Damn"));
    playerAnimationMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf + 275), new Size(200, 50), "ANIM_HANDS_UP", "Hands Up"));
    playerAnimationMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf + 350), new Size(200, 50), "ANIM_SURRENDER", "Surrender"));
}
API.onKeyDown.connect((sender, e) => {
    if (API.isChatOpen()) {
        return;
    }
    if (!API.hasEntitySyncedData(API.getLocalPlayer(), "ESS_LoggedIn")) {
        return;
    }
    if (e.KeyCode == Keys.I) {
        resource.Inventory.toggleInventory();
    }
});
API.onUpdate.connect(() => {
    if (API.isChatOpen()) {
        return;
    }
    if (!API.hasEntitySyncedData(API.getLocalPlayer(), "ESS_LoggedIn")) {
        return;
    }
    API.dxDrawTexture("clientside/images/crosshair/crosshair.png", new Point(widthHalf - 5, heightHalf - 5), new Size(10, 10), 0);
    API.disableControlThisFrame(86);
    API.disableControlThisFrame(51);
    if (API.isDisabledControlPressed(51)) {
        holdCounter += 5;
        if (holdCounter > 200) {
            isInteracting();
        }
        else {
            rayCastForItems();
        }
    }
    if (API.isDisabledControlJustReleased(51)) {
        holdCounter = 0;
        API.showCursor(false);
    }
    if (holdCounter <= 100) {
        if (API.isCursorShown()) {
            API.showCursor(false);
        }
    }
});
function rayCastForItems() {
    var playerPos = API.getEntityPosition(API.getLocalPlayer());
    var aimPos = API.getPlayerAimCoords(API.getLocalPlayer());
    var rayCast = API.createRaycast(playerPos, aimPos, 16, null);
    if (!rayCast.didHitAnything) {
        radiusForItems(aimPos);
        return;
    }
    if (!rayCast.didHitEntity) {
        radiusForItems(aimPos);
        return;
    }
    if (!API.doesEntityExist(rayCast.hitEntity)) {
        radiusForItems(aimPos);
        return;
    }
    if (!API.hasEntitySyncedData(rayCast.hitEntity, "DROPPED_OBJECT")) {
        radiusForItems(aimPos);
        return;
    }
    if (playerPos.DistanceTo(API.getEntityPosition(rayCast.hitEntity)) >= 5) {
        return;
    }
    API.triggerServerEvent("PICKUP_ITEM", rayCast.hitEntity);
}
function radiusForItems(aimpos) {
    var objects = API.getAllObjects();
    for (var i = 0; i < objects.Length; i++) {
        if (API.getEntityPosition(objects[i]).DistanceTo2D(aimpos) <= 1) {
            if (API.hasEntitySyncedData(objects[i], "DROPPED_OBJECT")) {
                API.triggerServerEvent("PICKUP_ITEM", objects[i]);
                return;
            }
        }
    }
}
function isInteracting() {
    if (!API.isCursorShown()) {
        API.showCursor(true);
    }
    if (!API.isPlayerInAnyVehicle(API.getLocalPlayer())) {
        if (API.isControlPressed(21)) {
            showAnimationMenu();
            return;
        }
    }
    if (API.isPlayerInAnyVehicle(API.getLocalPlayer())) {
        showVehicleMenu();
        return;
    }
}
function showVehicleMenu() {
    for (var i = 0; i < vehicleMenuButtons.length; i++) {
        vehicleMenuButtons[i].draw();
    }
}
function showAnimationMenu() {
    for (var i = 0; i < playerAnimationMenuButtons.length; i++) {
        playerAnimationMenuButtons[i].draw();
    }
}
class InteractionButton {
    constructor(pos, size, clientEvent, action) {
        this.position = pos;
        this.size = size;
        this.clientEvent = clientEvent;
        this.action = action;
        this.r = 0;
        this.g = 0;
        this.b = 0;
    }
    draw() {
        if (this.collision()) {
            API.drawRectangle(this.position.X, this.position.Y, this.size.Width, this.size.Height, 255, 255, 255, 100);
            this.drawtext();
            this.clicked();
        }
        else {
            API.drawRectangle(this.position.X, this.position.Y, this.size.Width, this.size.Height, this.r, this.g, this.b, 100);
            this.drawtext();
        }
    }
    set Argument(value) {
        this.argument = value;
    }
    collision() {
        var mouse = API.getCursorPositionMaintainRatio();
        API.drawRectangle(mouse.X, mouse.Y, 5, 5, 255, 255, 255, 255);
        if (mouse.X > this.position.X && mouse.X < this.position.X + this.size.Width && mouse.Y > this.position.Y && mouse.Y < this.position.Y + this.size.Height) {
            return true;
        }
        return false;
    }
    clicked() {
        if (API.isControlJustPressed(237)) {
            API.playSoundFrontEnd("CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET");
            if (this.argument != null) {
                API.triggerServerEvent(this.clientEvent, this.argument);
            }
            else {
                API.triggerServerEvent(this.clientEvent);
            }
        }
    }
    drawtext() {
        API.drawText(this.action, Math.round(this.position.X + (this.size.Width / 2)), Math.round(this.position.Y + (this.size.Height / 2)) - 18, 0.5, 255, 255, 255, 255, 4, 1, false, false, 600);
    }
    setRGB(r, g, b) {
        this.r = r;
        this.g = g;
        this.b = b;
    }
}
