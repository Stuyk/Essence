"use strict";
var resX = API.getScreenResolutionMaintainRatio().Width;
var resY = API.getScreenResolutionMaintainRatio().Height;
var res = API.getScreenResolutionMaintainRatio();
var cef = null;
class CefHelper {
    constructor(resourcePath) {
        this.path = resourcePath;
        this.open = false;
    }
    show() {
        if (this.open == false) {
            this.open = true;
            var resolution = API.getScreenResolution();
            this.browser = API.createCefBrowser(resolution.Width, resolution.Height, true);
            API.waitUntilCefBrowserInit(this.browser);
            API.setCefBrowserPosition(this.browser, 0, 0);
            API.loadPageCefBrowser(this.browser, this.path);
            API.showCursor(true);
            API.setCanOpenChat(false);
            API.setHudVisible(false);
            API.setChatVisible(false);
        }
    }
    showNonLocal() {
        if (this.open == false) {
            this.open = true;
            var resolution = API.getScreenResolution();
            this.browser = API.createCefBrowser(resolution.Width, resolution.Height, false);
            API.waitUntilCefBrowserInit(this.browser);
            API.setCefBrowserPosition(this.browser, 0, 0);
            API.loadPageCefBrowser(this.browser, this.path);
            API.showCursor(true);
            API.setCanOpenChat(false);
            API.setHudVisible(false);
            API.setChatVisible(false);
        }
    }
    destroy() {
        this.open = false;
        API.destroyCefBrowser(this.browser);
        API.showCursor(false);
        API.setCanOpenChat(true);
        API.setHudVisible(true);
        API.setChatVisible(true);
    }
    eval(string) {
        this.browser.eval(string);
    }
}
API.onResourceStop.connect(function () {
    if (cef !== null) {
        cef.destroy();
        cef = null;
    }
    API.setHudVisible(true);
});
function killPanel() {
    if (cef !== null) {
        cef.destroy();
        cef = null;
    }
}
function showCEF(path) {
    if (cef !== null) {
        cef.destroy();
    }
    cef = null;
    cef = new CefHelper(path);
    cef.show();
}
function showNonLocalCEF(path) {
    if (cef !== null) {
        cef.destroy();
    }
    cef = null;
    cef = new CefHelper(path);
    cef.showNonLocal();
}
function callCEF(func, args) {
    if (cef === null) {
        return;
    }
    cef.browser.call(func, args);
}
function Login(username, password) {
    API.triggerServerEvent("clientLogin", username, password);
}
function Register(username, password) {
    API.triggerServerEvent("clientRegister", username, password);
}
//# sourceMappingURL=BrowserManager.js.map