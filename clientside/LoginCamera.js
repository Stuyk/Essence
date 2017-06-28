"use strict";
var cam = null;
API.onResourceStart.connect(() => {
    cam = API.createCamera(API.getEntityPosition(API.getLocalPlayer()), new Vector3());
    API.setCameraFov(cam, 110);
    API.setCameraPosition(cam, API.getCameraPosition(cam).Add(new Vector3(0, 0, 100)));
    API.setActiveCamera(cam);
    //API.playScreenEffect("DrugsDrivingIn", 6000, true);
});
API.onUpdate.connect(() => {
    if (cam !== null) {
        var rot = API.getCameraRotation(cam);
        API.setCameraRotation(cam, rot.Add(new Vector3(0, 0, 0.05)));
    }
});
API.onResourceStop.connect(() => {
    API.callNative("_STOP_ALL_SCREEN_EFFECTS");
});
function killLoginCamera() {
    cam = null;
}
