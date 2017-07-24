"use strict";
var lastNotification = null;
var chatMessages = new Array();
class HeadChat {
    constructor(target, text) {
        this.text = text;
        this.alpha = 255;
        this.modY = 0;
        this.target = target;
    }
    run() {
        if (this.alpha <= 0) {
            this.removeSelf();
            return;
        }
        if (!API.doesEntityExist(this.target)) {
            this.removeSelf();
            return;
        }
        var loc = API.getEntityPosition(this.target).Add(new Vector3(0, 0, 1));
        var pointer = API.worldToScreenMaintainRatio(loc);
        API.drawText(this.text, pointer.X, pointer.Y - this.modY, 0.6, 255, 255, 255, this.alpha, 4, 1, false, true, 600);
        this.alpha -= 1;
        this.modY += 1;
    }
    removeSelf() {
        for (var i = chatMessages.length; i > 0; i--) {
            if (chatMessages[i] === this) {
                chatMessages.splice(i, 1);
                break;
            }
        }
    }
}
class HeadNotification {
    constructor(whatToSay) {
        this.text = whatToSay;
        this.alpha = 255;
        this.modY = 0;
    }
    run() {
        if (this.alpha <= 0) {
            lastNotification = null;
            return;
        }
        var loc = API.getEntityPosition(API.getLocalPlayer()).Add(new Vector3(0, 0, 1));
        var pointer = API.worldToScreenMaintainRatio(loc);
        API.drawText(this.text, pointer.X, pointer.Y - this.modY, 0.6, 255, 255, 255, this.alpha, 4, 1, false, true, 600);
        this.alpha -= 2;
        this.modY += 1;
    }
}
API.onUpdate.connect(() => {
    if (lastNotification !== null) {
        lastNotification.run();
    }
});
function createHeadNotification(text) {
    lastNotification = new HeadNotification(text);
    API.playSoundFrontEnd("FocusIn", "HintCamSounds");
}
function createHeadNoteForTarget(target, text) {
    if (!API.doesEntityExist(target)) {
        return;
    }
    var newChat = new HeadChat(target, text);
    chatMessages.push(newChat);
}
