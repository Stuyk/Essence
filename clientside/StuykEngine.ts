var screenManager: ScreenManager = null;
// Screen Manager
class ScreenManager {
    private screens: Array<GameScreen>;
    private currentScreen: number;
    // Constructor, used to create a new instance of the 'Screen Manager'.
    constructor() {
        this.screens = new Array<GameScreen>();
        this.currentScreen = 0;
    }
    // Creates and returns a new 'GameScreen' to create 'Levels'.
    public addGameScreen() {
        var gameScreen: GameScreen = new GameScreen();
        this.screens.push(gameScreen);
        return gameScreen;
    }
    // Check if there are any screens in the array.
    private isLengthZero() {
        if (this.screens.length <= 0) {
            return true;
        }
        return false;
    }
    // Get * Set for CurrentScreen
    set CurrentScreen(value: number) {
        // Set the value to positive, no matter what.
        value = Math.abs(value);
        // Check if the value is above the screen limit.
        if (value > this.screens.length) {
            this.currentScreen = this.screens.length;
            return;
        }
        // If the value is not above the screen limit, set it.
        this.currentScreen = value;
    }
    // Our Screen Manager Game Loop
    run() {
        // If there are currently no game screens added, don't render anything.
        if (this.isLengthZero()) {
            return;
        }
        // Check contents of current screen.
    }
}
// Game Screen
class GameScreen {
    private entities: Array<any>;
    // Constructor, used to create a new instance of a 'GameScreen' which renders all the content inside of it.
    constructor() {
        this.entities = new Array<any>();
    }
    // Get currently active entities for this game screen.
    get Entities(): Array<any> {
        return this.entities;
    }
}
// Sprite
class Sprite {
    private path: string;
    // Constructor, used to specify a partial path to sprites. Sprites must be labeled 0 - Number to render.
    constructor(value: string) {
        this.path = value;
    }
}
// The Main Game Loop
API.onUpdate.connect(() => {
    if (screenManager == null) {
        return;
    }
    screenManager.run();
});
// Sandbox Code
API.onResourceStart.connect(() => {
    screenManager = new ScreenManager();
    var gameScreen: GameScreen = screenManager.addGameScreen();
});
