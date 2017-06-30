var screenX = API.getScreenResolutionMantainRatio().Width;
var screenY = API.getScreenResolutionMantainRatio().Height;
var halfScreenWidth = Math.round(screenX / 2);
var halfScreenHeight = Math.round(screenY / 2);

var currentLockPick = null;

class LockPick {
    private inside;
    private outside;
    private direction;
    private checkRate;
    private score;

    constructor() {
        this.inside = 0;
        this.outside = 0;
        this.score = 0;
        this.checkRate = Date.now() + 2000;
    }

    drawLockPick() {
        if (this.score >= 100) {
            API.showCursor(false);
            currentLockPick = null;
            return;
        }

        this.getOutside();
        this.getScore();
        this.controls();
        this.check();

        API.dxDrawTexture("clientside/images/lockpick/lockpick_0.png", new Point(halfScreenWidth - 290, halfScreenHeight - 290), new Size(580, 580), this.outside, 255, 255, 255, 255);
        API.dxDrawTexture("clientside/images/lockpick/lockpick_1.png", new Point(halfScreenWidth - 237, halfScreenHeight - 237), new Size(474, 474), this.inside, 255, 255, 255, 255);
        API.dxDrawTexture("clientside/images/lockpick/lockpick_2.png", new Point(halfScreenWidth - 143, halfScreenHeight - 143), new Size(286, 286), this.score, 255, 255, 255, 255);

        //API.drawText("Outside: " + this.outside, 50, 50, 1.0, 255, 255, 255, 255, 4, 0, false, false, 600);
        //API.drawText("Inside: " + this.inside, 50, 100, 1.0, 255, 255, 255, 255, 4, 0, false, false, 600);
        //API.drawText("Score: " + this.score, 50, 150, 1.0, 255, 255, 255, 255, 4, 0, false, false, 600);
    }

    private getOutside() {
        if (!API.hasEntitySyncedData(API.getLocalPlayer(), "Lockpick_Value")) {
            return;
        }

        this.outside = API.getEntitySyncedData(API.getLocalPlayer(), "Lockpick_Value")
    }

    private getScore() {
        if (!API.hasEntitySyncedData(API.getLocalPlayer(), "Lockpick_Score")) {
            return;
        }

        this.score = API.getEntitySyncedData(API.getLocalPlayer(), "Lockpick_Score")
    }

    private check() {
        if (this.checkRate > Date.now) {
            return;
        }
        this.checkRate = Date.now() + 2000;
        API.triggerServerEvent("Check_Lockpick_Score", this.inside)
    }

    private controls() {
        //API.disableAllControlsThisFrame();
        var mouse = API.getCursorPositionMantainRatio();
        var newPos = Math.atan2(mouse.Y - halfScreenHeight, mouse.X - halfScreenWidth);
        newPos = newPos * (180 / Math.PI);

        
        if (newPos < 0) {
            newPos = 360 - (-newPos);
        }

        newPos += 90;

        if (newPos > 360) {
            newPos = newPos - 360;
        }

        this.inside = Math.floor(newPos);
    }
}

API.onUpdate.connect(() => {
    if (currentLockPick != null) {
        currentLockPick.drawLockPick();
    }
});

function endLockPickMiniGame() {
    currentLockPick = null;
    API.showShard("Picklock Complete", 2000);
    API.showCursor(false);
}

function newLockPickMiniGame() {
    currentLockPick = new LockPick();
    API.showCursor(true);
}
