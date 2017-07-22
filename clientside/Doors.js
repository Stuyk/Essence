var doors = new Array();
var lastUpdate = Date.now();
var Door = (function () {
    function Door(model, position) {
        this.model = model;
        this.position = position;
    }
    Object.defineProperty(Door.prototype, "Model", {
        get: function () {
            return this.model;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(Door.prototype, "Position", {
        get: function () {
            return this.position;
        },
        enumerable: true,
        configurable: true
    });
    return Door;
}());
function retrieveDoor(id, position) {
    var door = new Door(id, position);
    doors.push(door);
}
API.onUpdate.connect(function () {
    if (Date.now() > lastUpdate + 5000) {
        lastUpdate = Date.now();
        checkAllDoors();
    }
});
function checkAllDoors() {
    if (doors.length <= 0) {
        return;
    }
    var pos = API.getEntityPosition(API.getLocalPlayer());
    for (var i = 0; i < doors.length; i++) {
        if (doors[i].Position.DistanceTo(pos) <= 20) {
            API.callNative("SET_STATE_OF_CLOSEST_DOOR_OF_TYPE", doors[i].Model, doors[i].Position.X, doors[i].Position.Y, doors[i].Position.Z, false, 0, 1);
        }
    }
}
//# sourceMappingURL=Doors.js.map