var screenRes = API.getScreenResolutionMantainRatio();
var widthHalf = Math.round(screenRes.Width / 2);
var heightHalf = Math.round(screenRes.Height / 2);

var holdCounter = 0;
var vehicleMenuButtons: InteractionButton[] = new Array<InteractionButton>();
var playerMenuButtons: InteractionButton[] = new Array<InteractionButton>();
// All of our interaction options go in here.
API.onResourceStart.connect(() => {
    vehicleLeftMenus();
    vehicleCenterMenus();
    vehicleRightMenus();
});

function vehicleCenterMenus() {
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf - 100), new Size(200, 50), "Vehicle_Hood", "Hood"));
    // 75 Space
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf - 25), new Size(200, 50), "Vehicle_Engine", "Engine"));
    // 75 Space
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf - 100, heightHalf + 50), new Size(200, 50), "Vehicle_Trunk", "Trunk"));
}

function vehicleLeftMenus() {
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf - 325, heightHalf - 100), new Size(200, 50), "Vehicle_Door_0", "Driver Door"));
    // 75 Space
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf - 325, heightHalf - 25), new Size(200, 50), "Vehicle_Door_2", "Driver Rear Door"));
    // 75 Space
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf - 325, heightHalf + 50), new Size(200, 50), "Vehicle_Windows_Down", "Windows Down"));
}

function vehicleRightMenus() {
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf + 125, heightHalf - 100), new Size(200, 50), "Vehicle_Door_1", "Passenger Door"));
    // 75 Space
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf + 125, heightHalf - 25), new Size(200, 50), "Vehicle_Door_3", "Passenger Rear Door"));
    // 75 Space
    vehicleMenuButtons.push(new InteractionButton(new Point(widthHalf + 125, heightHalf + 50), new Size(200, 50), "Vehicle_Windows_Up", "Windows Up"));
}


API.onUpdate.connect(() => {
    if (API.isChatOpen()) {
        return;
    }

    if (!API.hasEntitySyncedData(API.getLocalPlayer(), "ESS_LoggedIn")) {
        return;
    }

    API.disableControlThisFrame(Enums.Controls.VehicleHorn);

    if (API.isDisabledControlPressed(Enums.Controls.Context)) {
        holdCounter += 5;
        if (holdCounter > 200) {
            isInteracting();
        }
    }

    if (API.isDisabledControlJustReleased(Enums.Controls.Context)) {
        holdCounter = 0;
        API.showCursor(false);
    }

    if (holdCounter <= 100) {
        if (API.isCursorShown()) {
            API.showCursor(false);
        }
    }
});

function isInteracting() {
    if (!API.isCursorShown()) {
        API.showCursor(true);
    }

    if (API.isPlayerInAnyVehicle(API.getLocalPlayer())) {
        showVehicleMenu();
    }
}

// Menu for Vehicles
function showVehicleMenu() {
    for (var i = 0; i < vehicleMenuButtons.length; i++) {
        vehicleMenuButtons[i].draw();
    }
}

class InteractionButton {
    private position: Point;
    private size: Size;
    private clientEvent: string;
    private action: string;
    constructor(pos: Point, size: Size, clientEvent: string, action: string) {
        this.position = pos;
        this.size = size;
        this.clientEvent = clientEvent;
        this.action = action;
    }
    draw() {
        if (this.collision()) {
            API.drawRectangle(this.position.X, this.position.Y, this.size.Width, this.size.Height, 255, 255, 255, 100);
            this.drawtext();
            this.clicked();
        } else {
            API.drawRectangle(this.position.X, this.position.Y, this.size.Width, this.size.Height, 0, 0, 0, 100);
            this.drawtext();
        }
    }
    private collision() {
        var mouse = API.getCursorPositionMantainRatio();

        if (mouse.X > this.position.X && mouse.X < this.position.X + this.size.Width && mouse.Y > this.position.Y && mouse.Y < this.position.Y + this.size.Height) {
            return true;
        }
        return false;
    }
    private clicked() {
        if (API.isControlJustPressed(Enums.Controls.CursorAccept)) {
            API.playSoundFrontEnd("CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET");
            API.triggerServerEvent(this.clientEvent);
        }
    }
    private drawtext() {
        API.drawText(this.action, Math.round(this.position.X + (this.size.Width / 2)), Math.round(this.position.Y + (this.size.Height / 2)) - 18, 0.5, 255, 255, 255, 255, 4, 1, false, false, 600);
    }
}