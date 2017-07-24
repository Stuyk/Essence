var screenManager = null;
// Screen Manager
var ScreenManager = (function () {
    // Constructor, used to create a new instance of the 'Screen Manager'.
    function ScreenManager() {
        this.screens = new Array();
        this.currentScreen = 0;
    }
    // Creates and returns a new 'GameScreen' to create 'Levels'.
    ScreenManager.prototype.addGameScreen = function () {
        var gameScreen = new GameScreen();
        this.screens.push(gameScreen);
        return gameScreen;
    };
    // Check if there are any screens in the array.
    ScreenManager.prototype.isLengthZero = function () {
        if (this.screens.length <= 0) {
            return true;
        }
        return false;
    };
    Object.defineProperty(ScreenManager.prototype, "CurrentScreen", {
        // Get * Set for CurrentScreen
        set: function (value) {
            // Set the value to positive, no matter what.
            value = Math.abs(value);
            // Check if the value is above the screen limit.
            if (value > this.screens.length) {
                this.currentScreen = this.screens.length;
                return;
            }
            // If the value is not above the screen limit, set it.
            this.currentScreen = value;
        },
        enumerable: true,
        configurable: true
    });
    // Our Screen Manager Game Loop
    ScreenManager.prototype.run = function () {
        // If there are currently no game screens added, don't render anything.
        if (this.isLengthZero()) {
            return;
        }
        // Check contents of current screen.
    };
    return ScreenManager;
}());
// Game Screen
var GameScreen = (function () {
    // Constructor, used to create a new instance of a 'GameScreen' which renders all the content inside of it.
    function GameScreen() {
        this.entities = new Array();
    }
    Object.defineProperty(GameScreen.prototype, "Entities", {
        // Get currently active entities for this game screen.
        get: function () {
            return this.entities;
        },
        enumerable: true,
        configurable: true
    });
    return GameScreen;
}());
// Sprite
var Sprite = (function () {
    // Constructor, used to specify a partial path to sprites. Sprites must be labeled 0 - Number to render.
    function Sprite(value) {
        this.path = value;
    }
    return Sprite;
}());
// The Main Game Loop
API.onUpdate.connect(function () {
    if (screenManager == null) {
        return;
    }
    screenManager.run();
});
// Sandbox Code
API.onResourceStart.connect(function () {
    screenManager = new ScreenManager();
    var gameScreen = screenManager.addGameScreen();
});
//# sourceMappingURL=StuykEngine.js.map