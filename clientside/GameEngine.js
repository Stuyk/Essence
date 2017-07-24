"use strict";
var screenManager = null;
API.onUpdate.connect(() => {
    if (screenManager === null) {
        return;
    }
});
