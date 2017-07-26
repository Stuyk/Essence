"use strict";
var screenManager = null;
class ScreenManager {
    constructor() {
        this.screens = new Array();
        this.currentScreen = 0;
    }
    addGameScreen() {
        var gameScreen = new GameScreen();
        this.screens.push(gameScreen);
        return gameScreen;
    }
    isLengthZero() {
        if (this.screens.length <= 0) {
            return true;
        }
        return false;
    }
    set CurrentScreen(value) {
        value = Math.abs(value);
        if (value > this.screens.length) {
            this.currentScreen = this.screens.length;
            return;
        }
        this.currentScreen = value;
    }
    run() {
        if (this.isLengthZero()) {
            return;
        }
    }
}
class GameScreen {
    constructor() {
        this.entities = new Array();
    }
    get Entities() {
        return this.entities;
    }
}
class Sprite {
    constructor(value) {
        this.path = value;
    }
}
API.onUpdate.connect(() => {
    if (screenManager == null) {
        return;
    }
    screenManager.run();
});
API.onResourceStart.connect(() => {
    screenManager = new ScreenManager();
    var gameScreen = screenManager.addGameScreen();
});
