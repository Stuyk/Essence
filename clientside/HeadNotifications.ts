var lastNotification: HeadNotification = null;
var chatMessages: HeadChat[] = new Array<HeadChat>();

class HeadChat {
    private target: LocalHandle;
    private text: string;
    private alpha: number;
    private modY: number;

    constructor(target: LocalHandle, text: string) {
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

    private removeSelf() {
        for (var i = chatMessages.length; i > 0; i--) {
            if (chatMessages[i] === this) {
                chatMessages.splice(i, 1);
                break;
            }
        }
    }
}

class HeadNotification {
    private text: string;
    private alpha: number;
    private modY: number;

    constructor(whatToSay: string) {
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
})

function createHeadNotification(text: string) {
    lastNotification = new HeadNotification(text);
    API.playSoundFrontEnd("FocusIn", "HintCamSounds");
}

function createHeadNoteForTarget(target: LocalHandle, text: string) {
    if (!API.doesEntityExist(target)) {
        return;
    }

    var newChat = new HeadChat(target, text);
    chatMessages.push(newChat);
}