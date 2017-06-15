"use strict";
// Screen Stuff
var screenX = API.getScreenResolution().Width;
var screenY = API.getScreenResolution().Height;
// Mission PauseState
var missionPauseState = true;
// Stylesheet Properties
var backgroundRGBA = [0, 0, 0, 100];
var textRGBA = [255, 255, 255, 255];
var overlayRGBA = [1, 87, 155, 255];
var markerRGBA = [255, 255, 255, 50];
var blipColor = 77;
// Time Check
var timeSinceLastCheck = new Date().getTime();
// Used for over-head notifications.
var headNotification = null;
// Array of mission objectives / locations.
var objectives = new Array();
// Array of mission blips / markers.
var objectiveMarkers = [];
var objectiveBlips = [];
// Array of current allied players on team.
var teammates = new Set();
var team = "";
API.onServerEventTrigger.connect(function (event, args) {
    if (!event.includes("Mission")) {
        return;
    }
    switch (event) {
        // Team Removal / Updates
        case "Mission_Add_Player":
            addPlayer(args[0]);
            return;
        case "Mission_Remove_Player":
            removePlayer(args[0]);
            return;
        // Mission Instance - Objectives
        case "Mission_New_Objective":
            var objective = new Objective(args[0], args[1]);
            objectives.push(objective);
            return;
        // Mission Instance - Setup Objective Markers
        case "Mission_Setup_Objectives":
            setupMarkers();
            setupBlips();
            return;
        // Mission Instance - Remove Objective
        case "Mission_Remove_Objective":
            removeObjective(args[0]);
            return;
        // Mission Instance
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
            if (args.Count <= 1) {
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
/**
 * Cleans up anything / everything but ALLIES.
 */
function partialCleanup() {
    objectives = [];
    cleanupMarkers();
    cleanupBlips();
}
/**
 * Cleans up anything / everything.
 */
function fullCleanup() {
    objectives = [];
    cleanupMarkers();
    cleanupBlips();
    cleanupTeammates();
}
/**
 *  Called when we need to setup all the markers for our objectives.
 */
function setupMarkers() {
    // Deletes any existing markers and then creates a clean array.
    cleanupMarkers();
    // Get all of our objective locations, loop through and determine the type of marker we need.
    for (var i = 0; i < objectives.length; i++) {
        let newMarker = null;
        switch (objectives[i].Type) {
            case "Location":
                newMarker = API.createMarker(20 /* ChevronUpX1 */, objectives[i].Location.Add(new Vector3(0, 0, 2)), new Vector3(), new Vector3(), new Vector3(1, 1, 1), markerRGBA[0], markerRGBA[1], markerRGBA[2], markerRGBA[3]);
                break;
            case "Capture":
                newMarker = API.createMarker(1 /* VerticalCylinder */, objectives[i].Location, new Vector3(), new Vector3(), new Vector3(5, 5, 5), markerRGBA[0], markerRGBA[1], markerRGBA[2], markerRGBA[3]);
                break;
            case "FastCapture":
                newMarker = API.createMarker(1 /* VerticalCylinder */, objectives[i].Location, new Vector3(), new Vector3(), new Vector3(5, 5, 5), markerRGBA[0], markerRGBA[1], markerRGBA[2], markerRGBA[3]);
                break;
            case "Destroy":
                newMarker = API.createMarker(28 /* DebugSphere */, objectives[i].Location, new Vector3(), new Vector3(), new Vector3(0.2, 0.2, 0.2), markerRGBA[0], markerRGBA[1], markerRGBA[2], markerRGBA[3]);
                break;
            default:
                return;
        }
        if (newMarker !== null) {
            objectiveMarkers.push(newMarker);
        }
    }
}
/**
 *  Called when we need to setup all the blips for our objectives.
 */
function setupBlips() {
    // Deleted any existing blips and then created a clean array.
    cleanupBlips();
    // Get all of our objective locations, loop through and determine the type of blip we need.
    for (var i = 0; i < objectives.length; i++) {
        let newBlip = API.createBlip(objectives[i].Location);
        switch (objectives[i].Type) {
            case "Capture":
                API.setBlipSprite(newBlip, 164);
                API.setBlipColor(newBlip, blipColor);
                break;
            case "Location":
                API.setBlipSprite(newBlip, 162);
                API.setBlipColor(newBlip, blipColor);
                break;
            case "Teleport":
                API.deleteEntity(newBlip);
                return;
            case "Destroy":
                API.setBlipSprite(newBlip, 486);
                API.setBlipColor(newBlip, blipColor);
                break;
        }
        objectiveBlips.push(newBlip);
    }
}
/**
 * Cleanup blips.
 */
function cleanupBlips() {
    if (objectiveBlips.length <= 0) {
        return;
    }
    for (var i = 0; i < objectiveBlips.length; i++) {
        API.deleteEntity(objectiveBlips[i]);
    }
    objectiveBlips = [];
}
/**
 * Cleanup Markers.
 */
function cleanupMarkers() {
    if (objectiveMarkers.length <= 0) {
        return;
    }
    for (var i = 0; i < objectiveMarkers.length; i++) {
        API.deleteEntity(objectiveMarkers[i]);
    }
    objectiveMarkers = [];
}
/**
* Cleanup Teammates.
*/
function cleanupTeammates() {
    if (teammates.size <= 0) {
        return;
    }
    teammates.forEach(({ Blip }) => API.deleteEntity(Blip));
    teammates = new Set();
    team = "";
}
/**
 * Remove an objective based on location.
 */
function removeObjective(location) {
    var index = -1;
    for (var i = 0; i < objectives.length; i++) {
        if (objectives[i].Location.ToString() === location.ToString()) {
            index = i;
            break;
        }
    }
    if (objectiveMarkers.length > 0) {
        API.deleteEntity(objectiveMarkers[index]);
    }
    if (objectiveBlips.length > 0) {
        API.deleteEntity(objectiveBlips[index]);
    }
    objectiveMarkers.splice(index, 1);
    objectives.splice(index, 1);
    objectiveBlips.splice(index, 1);
}
class Teammate {
    constructor(id) {
        this.teammateID = id;
        this.teammateBlip = API.createBlip(API.getEntityPosition(id));
        this.teammateName = API.getPlayerName(id);
        this.teammateOldHealth = API.getPlayerHealth(id);
        API.setBlipSprite(this.teammateBlip, 1);
        API.setBlipColor(this.teammateBlip, blipColor);
    }
    updateBlip() {
        if (!API.doesEntityExist(this.teammateID)) {
            return;
        }
        if (!API.doesEntityExist(this.teammateBlip)) {
            return;
        }
        API.setBlipPosition(this.teammateBlip, API.getEntityPosition(this.teammateID));
        if (this.teammateOldHealth === API.getPlayerHealth(this.teammateID)) {
            return;
        }
        this.teammateOldHealth = API.getPlayerHealth(this.teammateID);
        let playerHealth = API.getPlayerHealth(this.teammateID);
        // Set Green
        if (playerHealth >= 80) {
            API.setBlipColor(this.teammateBlip, 69);
            this.teammateName = `~g~${API.getPlayerName(this.teammateID)}`;
        }
        // Set Orange / Green
        if (playerHealth <= 79 && playerHealth >= 56) {
            API.setBlipColor(this.teammateBlip, 24);
            this.teammateName = `~g~${API.getPlayerName(this.teammateID)}`;
        }
        // Set Orange
        if (playerHealth <= 55 && playerHealth >= 30) {
            API.setBlipColor(this.teammateBlip, 81);
            this.teammateName = `~o~${API.getPlayerName(this.teammateID)}`;
        }
        // Set Red
        if (playerHealth <= 29 && playerHealth >= 16) {
            API.setBlipColor(this.teammateBlip, 49);
            this.teammateName = `~r~${API.getPlayerName(this.teammateID)}`;
        }
        // Set Deep Red
        if (playerHealth <= 15 && playerHealth >= 1) {
            API.setBlipColor(this.teammateBlip, 76);
            this.teammateName = `~r~${API.getPlayerName(this.teammateID)}`;
        }
        // Dead
        if (playerHealth <= 0) {
            API.setBlipColor(this.teammateBlip, 85);
            this.teammateName = `~h~~u~${API.getPlayerName(this.teammateID)}`;
        }
        updateTeamVariable();
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
    constructor(loc, type) {
        this.objectiveLocation = loc;
        this.objectiveType = type;
        this.objectiveProgress = -1;
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
                var pointer = Point.Round(API.worldToScreenMantainRatio(this.objectiveLocation));
                API.drawText(`${this.objectiveProgress}%`, pointer.X, pointer.Y - 100, 0.5, textRGBA[0], textRGBA[1], textRGBA[2], textRGBA[3], 4, 1, false, true, 600);
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
        var pointer = Point.Round(API.worldToScreenMantainRatio(location));
        API.drawText(`${this.headText}`, pointer.X, Math.round(pointer.Y + this.headAddon), 0.5, textRGBA[0], textRGBA[1], textRGBA[2], Math.round(this.headAlpha), 4, 1, false, true, 600);
        this.headAddon -= 0.5;
        this.headAlpha -= 3;
    }
}
/** OnUpdate Event */
API.onUpdate.connect(function () {
    if (headNotification !== null) {
        headNotification.run();
    }
    if (teammates.size >= 1) {
        displayCurrentPlayers();
        updateAllyPositions();
    }
    if (missionPauseState) {
        return;
    }
    if (objectives.length >= 1) {
        if (new Date().getTime() > timeSinceLastCheck) {
            missionObjectives();
            timeSinceLastCheck = new Date().getTime() + 30;
        }
    }
    displayObjectiveProgress();
});
// Check all objective types.
function missionObjectives() {
    var pos = API.getEntityPosition(API.getLocalPlayer());
    var obj = null;
    for (var i = 0; i < objectives.length; i++) {
        if (pos.DistanceTo(objectives[i].Location) <= 12) {
            obj = objectives[i];
            break;
        }
        if (objectives[i].Type == "Teleport") {
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
    }
}
// Display player list.
function displayCurrentPlayers() {
    API.drawText(`~b~Current Team ~w~~n~${team}`, screenX - 100, 20, 0.4, textRGBA[0], textRGBA[1], textRGBA[2], textRGBA[3], 4, 1, false, true, 150);
}
// Display current objectives progress.
function displayObjectiveProgress() {
    for (var i = 0; i < objectives.length; i++) {
        objectives[i].run();
    }
}
function updateAllyPositions() {
    if (teammates.size <= 0) {
        return;
    }
    teammates.forEach((value) => {
        value.updateBlip();
    });
}
/** Used to add a player to the array stack. **/
function addPlayer(target) {
    var instance = null;
    teammates.forEach((value, index) => {
        if (value.ID.value === target.value) {
            instance = value;
            return false; // Break
        }
        return true; // Return
    });
    if (instance !== null) {
        return;
    }
    var teammate = new Teammate(target);
    teammates.add(teammate);
    updateTeamVariable();
}
function updateTeamVariable() {
    team = "";
    teammates.forEach((value) => {
        team = team.concat(value.Name + "~n~");
        API.sendChatMessage(`${team}`);
    });
}
/**
 *  Used to remove a player from the array stack.
 * @param target
 */
function removePlayer(target) {
    var instance = null;
    teammates.forEach((value, index) => {
        if (value.ID.value === target.value) {
            instance = value;
            return false; // Break
        }
        return true; // Return
    });
    if (instance === null) {
        return;
    }
    teammates.delete(instance);
    updateTeamVariable();
}
//// OBJECTIVE TYPES
/** This is a point to point location objective type. */
function objectiveLocation() {
    for (var i = 0; i < objectives.length; i++) {
        var playerPos = API.getEntityPosition(API.getLocalPlayer());
        if (playerPos.DistanceTo(objectives[i].Location) <= 3) {
            API.triggerServerEvent("checkObjective");
        }
    }
}
function objectiveTeleport() {
    API.triggerServerEvent("checkObjective");
}
function objectiveCapture() {
    for (var i = 0; i < objectives.length; i++) {
        if (API.getEntityPosition(API.getLocalPlayer()).DistanceTo(objectives[i].Location) <= 5) {
            API.triggerServerEvent("checkObjective");
        }
    }
}
function objectiveDestroy() {
    if (!API.isPlayerShooting(API.getLocalPlayer())) {
        return;
    }
    for (var i = 0; i < objectives.length; i++) {
        if (API.getPlayerAimCoords(API.getLocalPlayer()).DistanceTo(objectives[i].Location) <= 0.5) {
            if (API.isPlayerShooting(API.getLocalPlayer())) {
                API.triggerServerEvent("checkObjective");
            }
        }
    }
}
