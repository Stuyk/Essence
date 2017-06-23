var lastNotification: HeadNotification = null;

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
        var pointer = Point.Round(API.worldToScreenMantainRatio(loc));
        API.drawText(this.text, pointer.X, pointer.Y - this.modY, 0.6, 255, 255, 255, this.alpha, 4, 1, false, true, 600);
        this.alpha -= 2;
        this.modY += 1;
    }

}

API.onUpdate.connect(() => {
    if (lastNotification === null) {
        return;
    }

    lastNotification.run();
})

function createHeadNotification(text: string) {
    lastNotification = new HeadNotification(text);
    API.playSoundFrontEnd("FocusIn", "HintCamSounds");
}