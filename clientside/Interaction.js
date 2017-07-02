"use strict";
var screenRes = API.getScreenResolutionMantainRatio();
var widthHalf = Math.round(screenRes.Width / 2);
var heightHalf = Math.round(screenRes.Height / 2);
var holdCounter = 0;
var vehicleMenuButtons = new Array();
var playerMenuButtons = new Array();
var playerAnimationMenuButtons = new Array();
// All of our interaction options go in here.
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
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf - 325, heightHalf - 100), new Size(200, 50), "Vehicle_Door_0", "Driver Door"));
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf - 325, heightHalf - 25), new Size(200, 50), "Vehicle_Door_2", "Driver Rear Door"));
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf - 325, heightHalf + 50), new Size(200, 50), "Vehicle_Windows_Down", "Windows Down"));
}
function vehicleRightMenus() {
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf + 125, heightHalf - 100), new Size(200, 50), "Vehicle_Door_1", "Passenger Door"));
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf + 125, heightHalf - 25), new Size(200, 50), "Vehicle_Door_3", "Passenger Rear Door"));
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf + 125, heightHalf + 50), new Size(200, 50), "Vehicle_Windows_Up", "Windows Up"));
}
function animationCenterMenus() {
    playerAnimationMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf - 400), new Size(200, 50), "ANIM_GESTURE_COME_HERE", "Come Here"));
    playerAnimationMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf - 325), new Size(200, 50), "ANIM_GESTURE_HELLO", "Hello"));
    playerAnimationMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf - 250), new Size(200, 50), "ANIM_NOD_YES", "Yes"));
    playerAnimationMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf - 175), new Size(200, 50), "ANIM_NOD_NO", "No"));
    playerAnimationMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf - 100), new Size(200, 50), "ANIM_GESTURE_SHRUG_HARD", "Shrug"));
    var interact = new InteractionButton(new Point(widthHalf - 100, heightHalf - 25), new Size(200, 50), "ANIM_STOP", "Stop Animation");
    interact.setRGB(255, 0, 0);
    playerAnimationMenuButtons.push(interact); // Middle
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
    // Check if the player is currently logged in.
    if (!API.hasEntitySyncedData(API.getLocalPlayer(), "ESS_LoggedIn")) {
        return;
    }
    // This will toggle the player's inventory on or off.'
    if (e.KeyCode == Keys.I) {
        resource.Inventory.toggleInventory();
    }
});
API.onUpdate.connect(() => {
    // Make sure the chat isn't open. This can be replaced with a true or false for 'drawing' a menu.
    if (API.isChatOpen()) {
        return;
    }
    // Check if the player is currently logged in.
    if (!API.hasEntitySyncedData(API.getLocalPlayer(), "ESS_LoggedIn")) {
        return;
    }
    API.dxDrawTexture("clientside/images/crosshair/crosshair.png", new Point(widthHalf - 5, heightHalf - 5), new Size(10, 10), 0, 255, 255, 255, 255);
    // We're going to use disabled controls here. So I'm disabling the vehicle horn since it's a control for E.
    API.disableControlThisFrame(86 /* VehicleHorn */);
    API.disableControlThisFrame(51 /* Context */);
    // When you hold E it adds to the counter. Once it's over 200, it turns on isInteracting();
    if (API.isDisabledControlPressed(51 /* Context */)) {
        holdCounter += 5;
        if (holdCounter > 200) {
            isInteracting();
        }
        else {
            rayCastForItems();
        }
    }
    // When E is released it will put the counter back down to 0.
    if (API.isDisabledControlJustReleased(51 /* Context */)) {
        holdCounter = 0;
        API.showCursor(false);
    }
    // This just ensures the cursor goes away if anything goes wrong.
    if (holdCounter <= 100) {
        if (API.isCursorShown()) {
            API.showCursor(false);
        }
    }
});
function rayCastForItems() {
    var playerPos = API.getEntityPosition(API.getLocalPlayer());
    var aimPos = API.getPlayerAimCoords(API.getLocalPlayer());
    var rayCast = API.createRaycast(playerPos, aimPos, 16 /* Objects */, null);
    // Check if our raycast hits anything.
    if (!rayCast.didHitAnything) {
        return;
    }
    if (!rayCast.didHitEntity) {
        return;
    }
    if (!API.doesEntityExist(rayCast.hitEntity)) {
        return;
    }
    // Check if it's a dropped object.
    if (!API.hasEntitySyncedData(rayCast.hitEntity, "DROPPED_OBJECT")) {
        return;
    }
    // Check if they're close enough.
    if (playerPos.DistanceTo(API.getEntityPosition(rayCast.hitEntity)) >= 5) {
        return;
    }
    // Ensure it's a prop and then trigger a server event.
    switch (API.getEntityType(rayCast.hitEntity)) {
        case 2 /* Prop */:
            API.triggerServerEvent("PICKUP_ITEM", rayCast.hitEntity);
            return;
    }
}
// This handles which menu our player is going to see.
function isInteracting() {
    // If the cursor isn't shown you, show it.
    if (!API.isCursorShown()) {
        API.showCursor(true);
    }
    if (!API.isPlayerInAnyVehicle(API.getLocalPlayer())) {
        if (API.isControlPressed(21 /* Sprint */)) {
            showAnimationMenu();
            return;
        }
    }
    // If the player is in a vehicle, show the vehicle menu.
    if (API.isPlayerInAnyVehicle(API.getLocalPlayer())) {
        showVehicleMenu();
        return;
    }
}
// This is our main vehicle draw function.
function showVehicleMenu() {
    // We loop through each 'InteractionButton' class item in our list and we call the drawFunction. Because it's in onUpdate it's fast enough to do this.
    for (var i = 0; i < vehicleMenuButtons.length; i++) {
        vehicleMenuButtons[i].draw();
    }
}
// Animation Menu
function showAnimationMenu() {
    // We loop through each 'InteractionButton' class item in our list and we call the drawFunction. Because it's in onUpdate it's fast enough to do this.
    for (var i = 0; i < playerAnimationMenuButtons.length; i++) {
        playerAnimationMenuButtons[i].draw();
    }
}
// This is our InteractionButton class.
class InteractionButton {
    // We need a new Point(x, y) which is the starting point of our button. We need a new Size(width, height) of 
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
    collision() {
        var mouse = API.getCursorPositionMantainRatio();
        API.drawRectangle(mouse.X, mouse.Y, 5, 5, 255, 255, 255, 255);
        if (mouse.X > this.position.X && mouse.X < this.position.X + this.size.Width && mouse.Y > this.position.Y && mouse.Y < this.position.Y + this.size.Height) {
            return true;
        }
        return false;
    }
    clicked() {
        if (API.isControlJustPressed(237 /* CursorAccept */)) {
            API.playSoundFrontEnd("CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET");
            API.triggerServerEvent(this.clientEvent);
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
