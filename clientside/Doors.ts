var doors: Door[] = new Array<Door>();
var lastUpdate = Date.now();

class Door {
    private model: number;
    private position: Vector3;

    constructor(model, position) {
        this.model = model;
        this.position = position;
    }

    get Model(): number {
        return this.model;
    }

    get Position(): Vector3 {
        return this.position;
    }
}

function retrieveDoor(id: number, position: Vector3) {
    var door = new Door(id, position);
    doors.push(door);
}

API.onUpdate.connect(() => {
    if (Date.now() > lastUpdate + 5000) {
        lastUpdate = Date.now();
        checkAllDoors();
    }
});

function checkAllDoors() {
    var pos = API.getEntityPosition(API.getLocalPlayer());
    for (var i = 0; i < doors.length; i++) {
        if (doors[i].Position.DistanceTo(pos) <= 20) {
            API.callNative("SET_STATE_OF_CLOSEST_DOOR_OF_TYPE", doors[i].Model, doors[i].Position.X, doors[i].Position.Y, doors[i].Position.Z, false, 0, 1);
        }
    }
}