"use strict";
var list = new Array();
var keyboardPath = "clientside/images/keyboard_e.png";
var actionCooldown = Date.now() + 3000;
class BlipTextPoint {
    constructor(position, type, color, text, draw, id, interactable) {
        this.position = position;
        this.type = type;
        this.color = color;
        this.text = text;
        this.draw = draw;
        this.id = id;
        this.blip = API.createBlip(this.position);
        this.interactable = interactable;
        API.setBlipSprite(this.blip, this.type);
        API.setBlipColor(this.blip, this.color);
        API.setBlipShortRange(this.blip, true);
        API.setBlipName(this.blip, this.text);
    }
    run() {
        if (!this.draw) {
            return;
        }
        var point = Point.Round(API.worldToScreenMantainRatio(this.position.Add(new Vector3(0, 0, 2))));
        API.drawText(this.text, point.X, point.Y - 20, 0.5, 255, 255, 255, 255, 4, 1, true, true, 600);
        if (this.interactable) {
            API.dxDrawTexture(keyboardPath, new Point(point.X - 25, point.Y + 40), new Size(50, 50), 0, 255, 255, 255, 255);
        }
    }
    get Position() {
        return this.position;
    }
    get Blip() {
        return this.blip;
    }
    get ID() {
        return this.id;
    }
    get Interactable() {
        return this.interactable;
    }
    // Used to interact with pretty much everything ever.
    triggerServerEvent() {
        API.triggerServerEvent(this.id);
    }
}
API.onUpdate.connect(() => {
    if (list.length <= 0) {
        return;
    }
    var playerPos = API.getEntityPosition(API.getLocalPlayer());
    drawText();
});
// Draws the text if the player is near it.
function drawText() {
    var playerPos = API.getEntityPosition(API.getLocalPlayer());
    for (var i = 0; i < list.length; i++) {
        if (list[i].Position.DistanceTo(playerPos) <= 10) {
            list[i].run();
        }
    }
}
// Adds a new point to the list.
function addNewPoint(position, type, color, text, draw, id, interactable) {
    var newPoint = new BlipTextPoint(position, type, color, text, draw, id, interactable);
    list.push(newPoint);
}
function checkIfNearPointOnce() {
    var playerPos = API.getEntityPosition(API.getLocalPlayer());
    if (Date.now() > actionCooldown) {
        actionCooldown = Date.now() + 3000;
        for (var i = 0; i < list.length; i++) {
            if (!list[i].Interactable) {
                continue;
            }
            if (list[i].Position.DistanceTo(playerPos) <= 5) {
                list[i].triggerServerEvent();
                return;
            }
        }
    }
}
