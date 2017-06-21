"use strict";
API.onResourceStart.connect(() => {
    resource.BrowserManager.showCEF("clientside/login.html");
});
