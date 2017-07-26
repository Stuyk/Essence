"use strict";
var screenX = API.getScreenResolution().Width;
var screenY = API.getScreenResolution().Height;
var missionPauseState = true;
var deathPause = false;
var backgroundRGBA = [0, 0, 0, 100];
var textRGBA = [255, 255, 255, 255];
var overlayRGBA = [1, 87, 155, 255];
var markerRGBA = [175, 228, 238, 150];
var blipColor = 77;
var timeSinceLastCheck = new Date().getTime();
var headNotification = null;
var objectives = new Array();
var objectiveMarkers = [];
var teammates = new Array();
var team = "";
API.onResourceStop.connect(() => {
    fullCleanup();
});
API.onPlayerRespawn.connect(() => {
    deathPause = true;
});
API.onServerEventTrigger.connect(function (event, args) {
    switch (event) {
        case "Mission_Add_Player":
            addPlayer(args[0]);
            return;
        case "Mission_Cleanup_Players":
            cleanupTeammates();
            return;
        case "Mission_New_Objective":
            var objective = new Objective(args[0], args[1], args[2]);
            objectives.push(objective);
            return;
        case "Mission_Setup_Objectives":
            setupMarkers();
            setupBlips();
            return;
        case "Mission_Remove_Objective":
            removeObjective(args[0]);
            return;
        case "Mission_New_Instance":
            fullCleanup();
            return;
        case "Mission_Abandon":
            fullCleanup();
            return;
        case "Mission_Finish":
            partialCleanup();
            return;
        case "Mission_Pause_State":
            missionPauseState = args[0];
            return;
        case "Mission_Head_Notification":
            headNotification = new PlayerHeadNotification(args[0]);
            if (args.length <= 1) {
                return;
            }
            switch (args[1]) {
                case "Objective":
                    API.playSoundFrontEnd("On_Call_Player_Join", "DLC_HEISTS_GENERAL_FRONTEND_SOUNDS");
                    return;
                case "Finish":
                    API.playSoundFrontEnd("Mission_Pass_Notify", "DLC_HEISTS_GENERAL_FRONTEND_SOUNDS");
                    return;
                case "Fail":
                    API.playSoundFrontEnd("Hack_Failed", "DLC_HEIST_BIOLAB_PREP_HACKING_SOUNDS");
                    return;
                case "ObjectivesComplete":
                    API.playSoundFrontEnd("Highlight_Move", "DLC_HEIST_PLANNING_BOARD_SOUNDS");
                    return;
                case "NewObjective":
                    API.playSoundFrontEnd("FocusIn", "HintCamSounds");
                    return;
                case "Error":
                    API.playSoundFrontEnd("Highlight_Error", "DLC_HEIST_PLANNING_BOARD_SOUNDS");
                    return;
            }
            return;
        case "Mission_Update_Progression":
            var vector = args[0];
            for (var i = 0; i < objectives.length; i++) {
                if (objectives[i].Location.ToString() === vector.ToString()) {
                    objectives[i].Progress = args[1];
                }
            }
            return;
    }
});
function partialCleanup() {
    cleanupMarkers();
    cleanupBlips();
    objectives = [];
}
function fullCleanup() {
    cleanupMarkers();
    cleanupBlips();
    cleanupTeammates();
    objectives = [];
}
function setupMarkers() {
    cleanupMarkers();
    for (var i = 0; i < objectives.length; i++) {
        let newMarker = null;
        switch (objectives[i].Type) {
            case "Location":
                newMarker = API.createMarker(3, objectives[i].Location.Add(new Vector3(0, 0, 2)), new Vector3(), new Vector3(), new Vector3(1, 1, 1), markerRGBA[0], markerRGBA[1], markerRGBA[2], markerRGBA[3]);
                break;
            case "Capture":
                newMarker = API.createMarker(1, objectives[i].Location, new Vector3(), new Vector3(), new Vector3(5, 5, 5), markerRGBA[0], markerRGBA[1], markerRGBA[2], markerRGBA[3]);
                break;
            case "Destroy":
                newMarker = API.createMarker(28, objectives[i].Location, new Vector3(), new Vector3(), new Vector3(0.2, 0.2, 0.2), markerRGBA[0], markerRGBA[1], markerRGBA[2], markerRGBA[3]);
                break;
            case "VehicleCapture":
                newMarker = API.createMarker(1, objectives[i].Location, new Vector3(), new Vector3(), new Vector3(5, 5, 5), markerRGBA[0], markerRGBA[1], markerRGBA[2], markerRGBA[3]);
                break;
            case "VehicleLocation":
                newMarker = API.createMarker(3, objectives[i].Location.Add(new Vector3(0, 0, 2)), new Vector3(), new Vector3(), new Vector3(1, 1, 1), markerRGBA[0], markerRGBA[1], markerRGBA[2], markerRGBA[3]);
                break;
            case "PickupObject":
                newMarker = API.createMarker(3, objectives[i].Location.Add(new Vector3(0, 0, 1)), new Vector3(), new Vector3(0, 180, 0), new Vector3(0.5, 0.5, 0.5), markerRGBA[0], markerRGBA[1], markerRGBA[2], markerRGBA[3]);
                break;
            case "RetrieveVehicle":
                newMarker = API.createMarker(3, objectives[i].Location.Add(new Vector3(0, 0, 3)), new Vector3(), new Vector3(0, 180, 0), new Vector3(1, 1, 1), markerRGBA[0], markerRGBA[1], markerRGBA[2], markerRGBA[3]);
                break;
            case "BreakIntoVehicle":
                newMarker = API.createMarker(3, objectives[i].Location.Add(new Vector3(0, 0, 3)), new Vector3(), new Vector3(0, 180, 0), new Vector3(1, 1, 1), markerRGBA[0], markerRGBA[1], markerRGBA[2], markerRGBA[3]);
                break;
            case "KillPlayer":
                return;
            default:
                return;
        }
        if (newMarker !== null) {
            objectiveMarkers.push(newMarker);
        }
    }
}
function setupBlips() {
    for (var i = 0; i < objectives.length; i++) {
        objectives[i].ObjectiveBlip = API.createBlip(objectives[i].Location);
        switch (objectives[i].Type) {
            case "Capture":
                API.setBlipSprite(objectives[i].ObjectiveBlip, 164);
                API.setBlipColor(objectives[i].ObjectiveBlip, blipColor);
                break;
            case "Location":
                API.setBlipSprite(objectives[i].ObjectiveBlip, 162);
                API.setBlipColor(objectives[i].ObjectiveBlip, blipColor);
                break;
            case "Teleport":
                API.deleteEntity(objectives[i].ObjectiveBlip);
                return;
            case "Destroy":
                API.setBlipSprite(objectives[i].ObjectiveBlip, 486);
                API.setBlipColor(objectives[i].ObjectiveBlip, blipColor);
                break;
            case "SetIntoVehicle":
                API.deleteEntity(objectives[i].ObjectiveBlip);
                return;
            case "VehicleCapture":
                API.setBlipSprite(objectives[i].ObjectiveBlip, 164);
                API.setBlipColor(objectives[i].ObjectiveBlip, blipColor);
                break;
            case "VehicleLocation":
                API.setBlipSprite(objectives[i].ObjectiveBlip, 162);
                API.setBlipColor(objectives[i].ObjectiveBlip, blipColor);
                break;
            case "PickupObject":
                API.setBlipSprite(objectives[i].ObjectiveBlip, 367);
                API.setBlipColor(objectives[i].ObjectiveBlip, blipColor);
                break;
            case "RetrieveVehicle":
                API.setBlipSprite(objectives[i].ObjectiveBlip, 225);
                API.setBlipColor(objectives[i].ObjectiveBlip, blipColor);
                break;
            case "KillPlayer":
                API.deleteEntity(objectives[i].ObjectiveBlip);
                return;
            case "UnlockVehicles":
                API.deleteEntity(objectives[i].ObjectiveBlip);
                return;
            case "BreakIntoVehicle":
                API.setBlipSprite(objectives[i].ObjectiveBlip, 229);
                API.setBlipColor(objectives[i].ObjectiveBlip, blipColor);
                return;
        }
    }
}
function cleanupMarkers() {
    if (objectiveMarkers.length <= 0) {
        return;
    }
    for (var i = 0; i < objectiveMarkers.length; i++) {
        API.deleteEntity(objectiveMarkers[i]);
    }
    objectiveMarkers = [];
}
function cleanupBlips() {
    for (var i = 0; i < objectives.length; i++) {
        if (objectives[i].ObjectiveBlip != null) {
            API.deleteEntity(objectives[i].ObjectiveBlip);
        }
    }
}
function cleanupTeammates() {
    if (teammates.length <= 0) {
        return;
    }
    teammates = new Array();
    team = "";
    deathPause = false;
}
function removeObjective(id) {
    var index = -1;
    for (var i = 0; i < objectives.length; i++) {
        if (objectives[i].ObjectiveID === id) {
            index = i;
            if (objectives[i].ObjectiveBlip != null) {
                API.deleteEntity(objectives[i].ObjectiveBlip);
            }
            break;
        }
    }
    if (objectiveMarkers.length > 0) {
        API.deleteEntity(objectiveMarkers[index]);
    }
    objectiveMarkers.splice(index, 1);
    objectives.splice(index, 1);
}
class Teammate {
    constructor(id) {
        this.teammateID = id;
        this.teammateName = API.getPlayerName(id);
    }
    removeBlip() {
        if (API.doesEntityExist(this.teammateBlip)) {
            API.deleteEntity(this.teammateBlip);
            return;
        }
    }
    updateBlip() {
        if (!API.doesEntityExist(this.teammateID)) {
            return;
        }
        this.updatePosition();
    }
    updatePosition() {
    }
    adjustColor() {
        let playerHealth = API.getPlayerHealth(this.teammateID);
        if (playerHealth >= 80) {
            this.teammateName = `~g~${API.getPlayerName(this.teammateID)}`;
        }
        if (playerHealth <= 79 && playerHealth >= 56) {
            this.teammateName = `~g~${API.getPlayerName(this.teammateID)}`;
        }
        if (playerHealth <= 55 && playerHealth >= 30) {
            this.teammateName = `~o~${API.getPlayerName(this.teammateID)}`;
        }
        if (playerHealth <= 29 && playerHealth >= 16) {
            this.teammateName = `~r~${API.getPlayerName(this.teammateID)}`;
        }
        if (playerHealth <= 15 && playerHealth >= 1) {
            this.teammateName = `~r~${API.getPlayerName(this.teammateID)}`;
        }
        if (playerHealth <= 0) {
            this.teammateName = `~h~~u~${API.getPlayerName(this.teammateID)}`;
        }
    }
    get Name() {
        return this.teammateName;
    }
    get Blip() {
        return this.teammateBlip;
    }
    get ID() {
        return this.teammateID;
    }
}
class Objective {
    constructor(loc, type, objectiveID) {
        this.objectiveLocation = loc;
        this.objectiveType = type;
        this.objectiveProgress = -1;
        this.objectiveID = objectiveID;
        this.objectiveBlip = null;
        API.setWaypoint(loc.X, loc.Y);
    }
    set ObjectiveBlip(value) {
        this.objectiveBlip = value;
    }
    get ObjectiveBlip() {
        return this.objectiveBlip;
    }
    get ObjectiveID() {
        return this.objectiveID;
    }
    set Location(value) {
        this.objectiveLocation = value;
    }
    get Location() {
        return this.objectiveLocation;
    }
    set Type(value) {
        this.objectiveType = value;
    }
    get Type() {
        return this.objectiveType;
    }
    set Progress(value) {
        this.objectiveProgress = value;
    }
    get Progress() {
        return this.objectiveProgress;
    }
    run() {
        if (this.objectiveProgress > -1) {
            if (API.getEntityPosition(API.getLocalPlayer()).DistanceTo(this.objectiveLocation) <= 15) {
                var pointer = API.worldToScreenMaintainRatio(this.objectiveLocation);
                API.drawText(`${this.objectiveProgress}%`, Math.round(pointer.X), Math.round(pointer.Y - 100), 0.5, textRGBA[0], textRGBA[1], textRGBA[2], textRGBA[3], 4, 1, false, true, 600);
            }
        }
    }
}
class PlayerHeadNotification {
    constructor(value) {
        this.headText = value;
        this.headAlpha = 255;
        this.headAddon = 0;
    }
    run() {
        if (this.headAlpha <= 0) {
            headNotification = null;
            return;
        }
        var location = API.getEntityPosition(API.getLocalPlayer()).Add(new Vector3(0, 0, 1.2));
        var pointer = API.worldToScreenMaintainRatio(location);
        API.drawText(`${this.headText}`, Math.round(pointer.X), Math.round(pointer.Y + this.headAddon), 0.5, textRGBA[0], textRGBA[1], textRGBA[2], Math.round(this.headAlpha), 4, 1, false, true, 600);
        this.headAddon -= 0.5;
        this.headAlpha -= 3;
    }
}
API.onUpdate.connect(function () {
    if (deathPause) {
        return;
    }
    if (headNotification !== null) {
        headNotification.run();
    }
    if (missionPauseState) {
        return;
    }
    if (teammates.length >= 1) {
        displayCurrentPlayers();
        updateAllyHealth();
    }
    if (objectives.length >= 1) {
        if (new Date().getTime() > timeSinceLastCheck) {
            missionObjectives();
            timeSinceLastCheck = new Date().getTime() + 30;
        }
    }
    displayObjectiveProgress();
});
function missionObjectives() {
    var pos = API.getEntityPosition(API.getLocalPlayer());
    var obj = null;
    for (var i = 0; i < objectives.length; i++) {
        if (pos.DistanceTo(objectives[i].Location) <= 30) {
            obj = objectives[i];
            break;
        }
    }
    if (obj === null) {
        return;
    }
    switch (obj.Type) {
        case "Location":
            objectiveLocation();
            break;
        case "Teleport":
            objectiveTeleport();
            break;
        case "Capture":
            objectiveCapture();
            break;
        case "Destroy":
            objectiveDestroy();
            break;
        case "VehicleCapture":
            objectiveVehicleCapture();
            break;
        case "VehicleLocation":
            objectiveVehicleLocation();
            break;
        case "PickupObject":
            objectivePickupObject();
            break;
        case "RetrieveVehicle":
            objectiveRetrieveVehicle();
            break;
        case "BreakIntoVehicle":
            objectiveBreakIntoVehicle();
            break;
    }
}
function displayCurrentPlayers() {
    API.drawText(`~b~Current Team ~w~~n~${team}`, screenX - 100, 20, 0.4, textRGBA[0], textRGBA[1], textRGBA[2], textRGBA[3], 4, 1, false, true, 150);
}
function displayObjectiveProgress() {
    for (var i = 0; i < objectives.length; i++) {
        objectives[i].run();
    }
}
function updateAllyHealth() {
    if (teammates.length <= 0) {
        return;
    }
    teammates.forEach((value) => {
        value.updateBlip();
    });
}
function addPlayer(target) {
    var teammate = new Teammate(target);
    var exists = false;
    if (teammates.length > 0) {
        for (var member of teammates) {
            let value = member.Name;
            if (value === teammate.Name) {
                exists = true;
                break;
            }
        }
    }
    if (!exists) {
        teammates.push(teammate);
    }
    updateTeamVariable();
}
function updateTeamVariable() {
    team = "";
    teammates.forEach((value) => {
        team = team.concat(value.Name + "~n~");
    });
}
function objectiveLocation() {
    if (confirmPlayerIsNear(5)) {
        API.triggerServerEvent("checkObjective");
    }
}
function objectiveTeleport() {
    API.triggerServerEvent("checkObjective");
}
function objectiveCapture() {
    if (confirmPlayerIsNear(5)) {
        API.triggerServerEvent("checkObjective");
    }
}
function objectiveVehicleCapture() {
    if (!API.isPlayerInAnyVehicle(API.getLocalPlayer())) {
        return;
    }
    if (confirmPlayerIsNear(5)) {
        API.triggerServerEvent("checkObjective");
    }
}
function objectiveVehicleLocation() {
    if (!API.isPlayerInAnyVehicle(API.getLocalPlayer())) {
        return;
    }
    if (confirmPlayerIsNear(5)) {
        API.triggerServerEvent("checkObjective");
    }
}
function objectiveDestroy() {
    if (!API.isPlayerShooting(API.getLocalPlayer())) {
        return;
    }
    if (confirmPlayerAimIsNear(0.5)) {
        if (API.isPlayerShooting(API.getLocalPlayer())) {
            API.triggerServerEvent("checkObjective");
        }
    }
}
function objectivePickupObject() {
    if (API.isControlJustPressed(51)) {
        if (confirmPlayerIsNear(3)) {
            API.triggerServerEvent("checkObjective");
        }
    }
}
function objectiveBreakIntoVehicle() {
    if (API.isControlJustPressed(51)) {
        if (confirmPlayerIsNear(3)) {
            resource.Lockpick.newLockPickMiniGame();
        }
    }
}
function objectiveRetrieveVehicle() {
    if (!API.isPlayerInAnyVehicle(API.getLocalPlayer())) {
        return;
    }
    if (confirmPlayerIsNear(5)) {
        API.triggerServerEvent("checkObjective");
    }
}
function confirmPlayerIsNear(distanceToCheck) {
    var playerPos = API.getEntityPosition(API.getLocalPlayer());
    for (var i = 0; i < objectives.length; i++) {
        if (playerPos.DistanceTo(objectives[i].Location) <= distanceToCheck) {
            return true;
        }
    }
    return false;
}
function confirmPlayerAimIsNear(distanceToCheck) {
    for (var i = 0; i < objectives.length; i++) {
        if (API.getPlayerAimCoords(API.getLocalPlayer()).DistanceTo(objectives[i].Location) <= distanceToCheck) {
            return true;
        }
    }
    return false;
}
