var player = null;
var Pmoney = 0;
var ShoppingPos = new Vector3();
var BarberCamera = null;
var CameraPos = new Vector3(-1336.287, -1277.151, 5.679597);
var CameraRot = new Vector3(0, 0, 120);

var menuPool = null;
var MenuOpen = false;

var barberMenu = null;
var barberMenuItems = ["Hairstyles", "Beards", "Eyebrows", "Chest", "Contacts", "Face Paint", "Make-up"];

// Current Setup Variables
var curr_hair = 0;
var curr_hairColor = 0;
var curr_hairHighlight = 0;
var curr_beard = 0;
var curr_eyebrows = 0;
var curr_eyebrowsColor = 0;
var curr_eyebrowsColor2 = 0;
var curr_contacts = 0;
var curr_facePaint = 0;
var curr_makeUp = 0;
var curr_makeUp2 = 0;

//and the list goes on and on
var HairStyles = null;
var HairStylesItems = [
["Close Shave", 10, 0],
["Buzzcut", 10, 1],
["Faux Hawk", 10, 2],
["Hipster", 10, 3],
["Side Parting", 10, 4],
["Shorter Cut", 10, 5],
["Biker", 10, 6],
["Ponytail", 10, 7],
["Cornrows", 10, 8],
["Slicked", 10, 9],
["Short Brushed", 10, 10],
["Spikey", 10, 11],
["Caesar", 10, 12],
["Chopped", 10, 13],
["Dreads", 10, 14],
["Long Hair", 10, 15],
["Shaggy Curls", 10, 16],
["Surfer Dude", 10, 17],
["Short Side Part", 10, 18],
["High Slicked Sides", 10, 19],
["Long Slicked", 10, 20],
["Hipster Youth", 10, 21],
["Mullet", 10, 22],
//["Nightvision Goggles.", 10, 23],
["Classic Cornrows", 10, 24],
["Palm Cornrows", 10, 25],
["Lightning Cornrows", 10, 26],
["Whipped Cornrows", 10, 27],
["Zig Zag Cornrows", 10, 28],
["Snail Cornrows", 10, 29],
["Hightop", 10, 30],
["Loose Swept Back", 10, 31],
["Undercut Swept Back", 10, 32],
["Undercut Swept Side", 10, 33],
["Spiked Mohawk", 10, 34],
["Mod", 10, 35],
["Layered Mod", 10, 36]
];

var Beards = null;
var BeardsItems = [
["Clean Shaven", 0, 0],
["BEARBS", 200, 100],
["HAIRS", 300, 5]
];

var Eyebrows = null;
var EyebrowsItems = [
["Clean Shaven", 0, 0],
["CATERPILLARS", 200, 100],
["HAIRS", 300, 5]
];

var Chest = null;
var ChestItems = [
["Clean Shaven", 0, 0],
["TOM SELLECK", 200, 100],
["HAIRS", 300, 5]
];

var Contacts = null;
var ContactsItems = [
["Clean Shaven", 0, 0],
["CATERPILLARS", 200, 100],
["HAIRS", 300, 5]
];

var Facepaint = null;
var FacepaintItems = [
["These", 0, 0],
["are", 200, 100],
["broken!", 300, 5]
];

var MakeUp = null;
var MakeUpItems = [
["Clean Shaven", 0, 0],
["CATERPILLARS", 200, 100],
["HAIRS", 300, 5]
];

API.onUpdate.connect(function(s, e) 
{
	if (menuPool != null) 
	{
        menuPool.ProcessMenus();
    }
	if (MenuOpen) 
	{
        API.disableAllControlsThisFrame();
    }
});

//When ModMenu is started, create all the required menus and hide them
API.onResourceStart.connect(function(s, e) 
{
	menuPool = API.getMenuPool();
	player = API.getLocalPlayer();
	
		//mens
		get_curr_Vars();
		createBarberMenu();
		create_Hairstyles();
		//create_Beards();
		//create_Chest();
		//create_Contacts();
		//create_FacePaint();
		//create_MakeUp();
		//create_Eyebrows();

});

API.onServerEventTrigger.connect(function (eventName, args) 
{
  switch (eventName) 
  {
    case 'OPEN_BARBER_MENU':
	if (!MenuOpen) {
		//ShoppingPos = args[0];
		openBarberMenu();
		}
    break;
  }
});

function get_curr_Vars() 
{
	//Create the main menu
	curr_hair = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Hair");
	curr_hairColor = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_HairColor");
	curr_hairHighlight = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_HairHighlight");
	curr_beard = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_FACIAL_HAIR");
	curr_eyebrows = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_EYEBROWS");
	curr_eyebrowsColor = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_EYEBROWS_COLOR");
	curr_eyebrowsColor2 = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_EYEBROWS_COLOR2");
	curr_contacts = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_EYE_COLOR");
	//curr_facePaint = 0;
	curr_makeUp = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_MAKEUP_COLOR");
	curr_makeUp2 = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_MAKEUP_COLOR2");	
	
	/*females... eww
	curr_eyebrowsColor2 = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_EYEBROWS_COLOR2");
	curr_eyebrowsColor2 = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_EYEBROWS_COLOR2");
	curr_lipstickColor = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_LIPSTICK_COLOR");
	curr_lipstickColor2 = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_LIPSTICK_COLOR2");
	*/
	
}

function reset_curr_Vars() 
{
	//Create the main menu
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_Hair", curr_hair);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_HairColor", curr_hairColor);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_HairHighlight", curr_hairHighlight);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_FACIAL_HAIR", curr_beard);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_EYEBROWS", curr_eyebrows);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_EYEBROWS_COLOR", curr_eyebrowsColor);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_EYEBROWS_COLOR2", curr_eyebrowsColor2);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_EYE_COLOR", curr_contacts);
	//curr_facePaint = 0;
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_MAKEUP_COLOR", curr_makeUp);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_MAKEUP_COLOR2", curr_makeUp2);	
	
	/*females... eww
	curr_eyebrowsColor2 = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_EYEBROWS_COLOR2");
	curr_eyebrowsColor2 = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_EYEBROWS_COLOR2");
	curr_lipstickColor = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_LIPSTICK_COLOR");
	curr_lipstickColor2 = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_LIPSTICK_COLOR2");
	*/
	
}

function createBarberMenu() 
{
	//Create the main menu
	barberMenu = API.createMenu(" ", "Main Menu", 0, 0, 3);
	API.setMenuTitle(barberMenu, "");
	API.setMenuBannerSprite(barberMenu, "shopui_title_barber", "shopui_title_barber");

	
	for (var i = 0; i < barberMenuItems.length; i++) 
	{
		barberMenu.AddItem(API.createMenuItem(barberMenuItems[i], ""));
	}
	
	barberMenu.CurrentSelection = 0;
	menuPool.Add(barberMenu);
	barberMenu.Visible = false; 
	
	//Gets called when we select a category
	barberMenu.OnItemSelect.connect(function(sender, item, index)
	{
		switch (index) 
		{
			case 0:
			get_curr_Vars();
			openHairstylesMenu();
			var TheDude = API.getLocalPlayer();
			HairId = HairStylesItems[0][2];
			TextureId = curr_hairColor;
			API.setPlayerClothes(TheDude, 2, HairId, TextureId);
			break;			

		}
	});
	
	//Gets called when we Close this out
	barberMenu.OnMenuClose.connect(function(sender, item, index)
	{
		CloseMenu();
	});
	
}

//----------
function create_Hairstyles() 
{
	//Create the Hairstyles mask selection menu
	HairstylesMenu = API.createMenu("      ", "Hairstyles", 0, 0, 3);
    API.setMenuTitle(HairstylesMenu, "");
	API.setMenuBannerSprite(HairstylesMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(HairstylesMenu);
	HairstylesMenu.Visible = false;
		
	
	HairstylesMenu.OnIndexChange.connect(function(sender, index)
	{
		get_curr_Vars();
		var TheDude = API.getLocalPlayer();
		HairId = HairStylesItems[index][2];
		//API.sendChatMessage("Highlight: " + curr_hairHighlight + ". Color:" + curr_hairColor + ". Hair Id: " + HairId);
		TextureId = curr_hairColor;
		API.setPlayerClothes(TheDude, 2, HairId, TextureId);	
	});

	HairstylesMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = HairStylesItems[index][1];
			
		if ( vCost <= Pmoney){
			//send buy command here
			API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
			API.triggerServerEvent("BARBER_BUY", vCost, HairStylesItems[index][2], curr_hairColor, curr_hairHighlight);
			CloseMenu();
		}else {
		    API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
		}
	});
	
	HairstylesMenu.OnMenuClose.connect(function(sender, item, index)
	{
		barberMenu.Visible = true;
		HairstylesMenu.Visible = false; 
		resetBarberMenu();
	});
}


function openBarberMenu() 
{
	MenuOpen = true;
	CamOn();
    barberMenu.CurrentSelection = 0;
	barberMenu.Visible = true; 
	
}

// --------------------
function openHairstylesMenu() 
{
	barberMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetHairstylesMenu();
	HairstylesMenu.Visible = true; 
}

function resetHairstylesMenu()
{
	var vCurrent = curr_hair;
	var listCurrent = 0;
			
	HairstylesMenu.Clear();
	for (var i = 0; i < HairStylesItems.length; i++) 
	{
		var newitem = API.createMenuItem(HairStylesItems[i][0], "");
		var vCost = HairStylesItems[i][1];
		var isAdded = HairStylesItems[i][2];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
		
		if ( isAdded == vCurrent){
			listCurrent = i;
		}
		
        HairstylesMenu.AddItem(newitem);
	}
	HairstylesMenu.CurrentSelection = listCurrent;
}

function CamOn() 
{
	//tyesting new cam location
	var playerspos = API.getLocalPlayer();
	CameraPos = API.returnNative("GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS", 5, playerspos, 0.0, 1.75, 0.75);
	//set the camera
	BarberCamera = API.createCamera(CameraPos, new Vector3());
	API.pointCameraAtPosition(BarberCamera, API.getEntityPosition(API.getLocalPlayer()).Add(new Vector3(0, 0, .75)));
	API.setActiveCamera(BarberCamera);
	API.setHudVisible(false);	
	API.setChatVisible(false);
	resource.PointHelper.togglePointHelpers();
}

function CloseMenu() 
{
	API.setHudVisible(true);
	API.setChatVisible(true);
	resource.PointHelper.togglePointHelpers();
	barberMenu.Visible = false;
	HairstylesMenu.Visible = false;
	API.setActiveCamera(null);
	MenuOpen = false;
}