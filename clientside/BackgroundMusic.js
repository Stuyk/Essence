"use strict";
var musicPage = null; // Main CEF Page
// Main CEFHelper Class.
class BackgroundMusic {
    // Main Constructor - Requires the Path of the CEF File or HTML File or whatever.
    constructor(resourcePath) {
        this.path = resourcePath;
        this.open = false;
    }
    show() {
        if (this.open == false) {
            this.open = true;
            API.setCefDrawState(true);
            this.browser = API.createCefBrowser(0, 0, false);
            API.waitUntilCefBrowserInit(this.browser);
            API.setCefBrowserPosition(this.browser, 0, 0);
            API.loadPageCefBrowser(this.browser, this.path);
        }
    }
    // Destroys the CEF Browser.
    destroy() {
        this.open = false;
        API.destroyCefBrowser(this.browser);
    }
    // No idea what the fuck this does.
    eval(string) {
        this.browser.eval(string);
    }
}
// Destroy the CEF Panel if the user disconnects. That way we don't fucking destroy their game and aliens invade and shit.
API.onResourceStop.connect(function () {
    if (musicPage !== null) {
        musicPage.destroy();
        musicPage = null;
    }
    API.setHudVisible(true);
});
// Destroy any active CEF Panel.
function clearBackgroundMusic() {
    if (musicPage !== null) {
        musicPage.destroy();
        musicPage = null;
    }
}
// Show page, literally any fuggin path.
function loadBackgroundMusic(path) {
    if (musicPage !== null) {
        musicPage.destroy();
    }
    musicPage = null;
    musicPage = new BackgroundMusic(path);
    musicPage.show();
}
