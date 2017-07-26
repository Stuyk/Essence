var player = null;
var Pmoney = 0;
var ShoppingPos = new Vector3();
var BarberCamera = null;

var menuPool = null;
var MenuOpen = false;

var barberMenu = null;
var barberMenuItems = ["Hairstyles", "Beards", "Eyebrows", "Chest", "Contacts", "Face Paint", "Make-up"];

var MakeupMenu = null;
var MakeupMenuItems = ["Eyes", "Lips"];

// Current Setup Variables
var curr_hair = 0;
var curr_haircolor = 0;
var curr_hairhighlight = 0;
var curr_beard = 0;
var curr_beardcolor = 0;
var curr_beardopacity = 0;
var curr_chest = 0;
var curr_chestcolor = 0;
var curr_chestopacity = 0;
var curr_eyebrows = 0;
var curr_eyebrowscolor = 0;
var curr_eyebrowsopacity = 0;
var curr_contacts = 0;
var curr_facepaint = 0;
var curr_facepaintopacity = 0;
var curr_eyemakeup = 0;
var curr_eyemakeupopacity = 0;
var curr_lipstick = 0;
var curr_lipstickcolor = 0;
var curr_lipstickopacity = 0;

//not changed here. just to removed so you can see
var curr_shirt = 0;
var curr_shirt_tex = 0;
var curr_hat = -1;
var curr_hat_tex = 0;


//and the list goes on and on
var HairStyles = null;
var HairStylesItems = [
["Close Shave", 10, 0, "multiplayer_overlays", "FM_M_Hair_001_a"],
["Buzzcut", 10, 1, "multiplayer_overlays", "FM_M_Hair_001_z"],
["Faux Hawk", 10, 2, "multiplayer_overlays", "FM_M_Hair_001_z"],
["Hipster", 10, 3, "multiplayer_overlays", "FM_M_Hair_003_a"],
["Side Parting", 10, 4, "multiplayer_overlays", "FM_M_Hair_001_z"],
["Shorter Cut", 10, 5, "multiplayer_overlays", "FM_M_Hair_001_z"],
["Biker", 10, 6, "multiplayer_overlays", "FM_M_Hair_001_z"],
["Ponytail", 10, 7, "multiplayer_overlays", "FM_M_Hair_001_z"],
["Cornrows", 10, 8, "multiplayer_overlays", "FM_M_Hair_008_a"],
["Slicked", 10, 9, "multiplayer_overlays", "FM_M_Hair_001_z"],
["Short Brushed", 10, 10, "multiplayer_overlays", "FM_M_Hair_001_z"],
["Spikey", 10, 11, "multiplayer_overlays", "FM_M_Hair_001_z"],
["Caesar", 10, 12, "multiplayer_overlays", "FM_M_Hair_001_z"],
["Chopped", 10, 13, "multiplayer_overlays", "FM_M_Hair_001_z"],
["Dreads", 10, 14, "multiplayer_overlays", "FM_M_Hair_long_a"],
["Long Hair", 10, 15, "multiplayer_overlays", "FM_M_Hair_long_a"],
["Shaggy Curls", 10, 16, "multiplayer_overlays", "FM_M_Hair_001_z"],
["Surfer Dude", 10, 17, "multiplayer_overlays", "FM_M_Hair_001_a"],
["Short Side Part", 10, 18, "mpbusiness_overlays", "FM_Bus_M_Hair_000_a"],
["High Slicked Sides", 10, 19, "mpbusiness_overlays", "FM_Bus_M_Hair_001_a"],
["Long Slicked", 10, 20, "mphipster_overlays", "FM_Hip_M_Hair_000_a"],
["Hipster Youth", 10, 21, "mphipster_overlays", "FM_Hip_M_Hair_001_a"],
["Mullet", 10, 22, "multiplayer_overlays", "FM_M_Hair_001_a"],
["Classic Cornrows", 10, 24, "mplowrider_overlays", "LR_M_Hair_000"],
["Palm Cornrows", 10, 25, "mplowrider_overlays", "LR_M_Hair_001"],
["Lightning Cornrows", 10, 26, "mplowrider_overlays", "LR_M_Hair_002"],
["Whipped Cornrows", 10, 27, "mplowrider_overlays", "LR_M_Hair_003"],
["Zig Zag Cornrows", 10, 28, "mplowrider2_overlays", "LR_M_Hair_004"],
["Snail Cornrows", 10, 29, "mplowrider2_overlays", "LR_M_Hair_005"],
["Hightop", 10, 30, "mplowrider2_overlays", "LR_M_Hair_006"],
["Loose Swept Back", 10, 31, "mpbiker_overlays", "MP_Biker_Hair_000_M"],
["Undercut Swept Back", 10, 32, "mpbiker_overlays", "MP_Biker_Hair_001_M"],
["Undercut Swept Side", 10, 33, "mpbiker_overlays", "MP_Biker_Hair_002_M"],
["Spiked Mohawk", 10, 34, "mpbiker_overlays", "MP_Biker_Hair_003_M"],
["Mod", 10, 35, "mpbiker_overlays", "MP_Biker_Hair_004_M"],
["Layered Mod", 10, 36, "mpbiker_overlays", "MP_Biker_Hair_005_M"],
["Flat Top", 10, 37, "mpgunrunning_overlays", "MP_Gunrunning_Hair_M_000_M"],
["Military Buzzcut", 10, 38, "mpgunrunning_overlays", "MP_Gunrunning_Hair_M_001_M"]
];

var Beards = null;
var BeardsItems = [
["Clean Shaven", 100, -1],
["Light Stubble", 100, 0],
["Balbo", 100, 1],
["Circle Beard", 100, 2],
["Goatee", 100, 3],
["Chin", 100, 4],
["Chin Fuzz", 100, 5],
["Pencil Chin Strap", 100, 6],
["Scruffy", 100, 7],
["Musketeer", 100, 8],
["Mustache", 100, 9],
["Trimmed Beard", 100, 10],
["Stubble", 100, 11],
["Thin Circle Beard", 100, 12],
["Horseshoe", 100, 13],
["Pencil and 'Chops", 100, 14],
["Chin Strap Beard", 100, 15],
["Balbo and Sideburns", 100, 16],
["Mutton Chops", 100, 17],
["Scruffy Beard", 100, 18],
["Curly", 100, 19],
["Curly & Deep Stranger", 100, 20],
["Handlebar", 100, 21],
["Faustic", 100, 22],
["Otto & Patch", 100, 23],
["Otto and Full Stranger", 100, 24],
["Light Franz", 100, 25],
["The Hampstead", 100, 26],
["The Ambrose", 100, 27],
["Lincoln Curtain", 100, 28]
];

var Eyebrows = null;
var EyebrowsItems = [
["none", 100, -1],
["Balanced", 100, 0],
["Fashion", 100, 1],
["Cleopatra", 100, 2],
["Quizzical", 100, 3],
["Femme", 100, 4],
["Seductive", 100, 5],
["Pinched", 100, 6],
["Chola", 100, 7],
["Triomphe", 100, 8],
["Carefree", 100, 9],
["Curvaceous", 100, 10],
["Rodent", 100, 11],
["Double Tram", 100, 12],
["Thin", 100, 13],
["Penciled", 100, 14],
["Mother Plucker", 100, 15],
["Straight and Narrow", 100, 16],
["Natural", 100, 17],
["Fuzzy", 100, 18],
["Unkempt", 100, 19],
["Caterpillar", 100, 20],
["Regular", 100, 21],
["Mediterranean", 100, 22],
["Groomed", 100, 23],
["Bushels", 100, 24],
["Feathered", 100, 25],
["Prickly", 100, 26],
["Monobrow", 100, 27],
["Winged", 100, 28],
["Triple Tram", 100, 29],
["Arched Tram", 100, 30],
["Cutouts", 100, 31],
["Fade Away", 100, 32],
["Solo Tram", 100, 33]
];

var Chest = null;
var ChestItems = [
["Shaved", 100, -1],
["Natural", 100, 0],
["The Strip", 100, 1],
["The Tree", 100, 2],
["Hairy", 100, 3],
["Grisly", 100, 4],
["Ape", 100, 5],
["Groomed Ape", 100, 6],
["Bikini", 100, 7],
["Lightning Bolt", 100, 8],
["Reverse Lightning", 100, 9],
["Love Heart", 100, 10],
["Chestache", 100, 11],
["Happy Face", 100, 12],
["Skull", 100, 13],
["Snail Trail", 100, 14],
["Slug and Nips", 100, 15],
["Hairy Arms", 100, 16]
];

var Contacts = null;
var ContactsItems = [
["Green", 100, 0],
["Emerald", 100, 1],
["Light Blue", 100, 2],
["Ocean Blue", 100, 3],
["Light Brown", 100, 4],
["Dark Brown", 100, 5],
["Hazel", 100, 6],
["Dark Gray", 100, 7],
["Light Gray", 100, 8],
["Pink", 100, 9],
["Yellow", 100, 10],
["Purple", 100, 11],
["Blackout", 100, 12],
["Shades of Gray", 100, 13],
["Tequila Sunrise", 100, 14],
["Atomic", 100, 15],
["Warp", 100, 16],
["ECola", 100, 17],
["Space Ranger", 100, 18],
["Yin Yang", 100, 19],
["BullsEye", 100, 20],
["Lizard", 100, 21],
["Dragon", 100, 22],
["Extra Terrestrial", 100, 23],
["Goat", 100, 24],
["Smiley", 100, 25],
["Possessed", 100, 26],
["Demon", 100, 27],
["Infected", 100, 28],
["Alien", 100, 29],
["Undead", 100, 30],
["Zombie", 100, 31]
];

var FacePaint = null;
var FacePaintItems = [
["None", 100, -1],
["Kiss My Axe", 100, 16],
["Panda Pussy", 100, 17],
["The Bat", 100, 18],
["Skull in Scarlet", 100, 19],
["Serpentine", 100, 20],
["The Veldt", 100, 21],
["Tribal Lines", 100, 26],
["Tribal Swirls", 100, 27],
["Tribal Orange", 100, 28],
["Tribal Red", 100, 29],
["Trapped in a Box", 100, 30],
["Clowning", 100, 31],
["Stars n Stripes", 100, 33],
["Shadow Demon", 100, 42],
["Fleshy Demon", 100, 43],
["Flayed Demon", 100, 44],
["Sorrow Demon", 100, 45],
["Smiler Demon", 100, 46],
["Cracked Demon", 100, 47],
["Danger Skull", 100, 48],
["Wicked Skull", 100, 49],
["Menace Skull", 100, 50],
["Bone Jaw Skull", 100, 51],
["Flesh Jaw Skull", 100, 52],
["Spirit Skull", 100, 53],
["Ghoul Skull", 100, 54],
["Phantom Skull", 100, 55],
["Gnasher Skull", 100, 56],
["Exposed Skull", 100, 57],
["Ghostly Skull", 100, 58],
["Fury Skull", 100, 59],
["Demi Skull", 100, 60],
["Inbred Skull", 100, 61],
["Spooky Skull", 100, 62],
["Slashed Skull", 100, 63],
["Web Sugar Skull", 100, 64],
["Senor Sugar Skull", 100, 65],
["Swirl Sugar Skull", 100, 66],
["Floral Sugar Skull", 100, 67],
["Mono Sugar Skull", 100, 68],
["Femme Sugar Skull", 100, 69],
["Demi Sugar Skull", 100, 70],
["Scarred Sugar Skull", 100, 71]
];

var EyeMakeUp = null;
var EyeMakeUpItems = [
["Clean Shaven", 0, 0],
["CATERPILLARS", 200, 100],
["HAIRS", 300, 5]
];

var LipMakeUp = null;
var LipMakeUpItems = [
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

//When The Menu is started, create all the required menus and hide them
API.onResourceStart.connect(function(s, e) 
{
	menuPool = API.getMenuPool();
	player = API.getLocalPlayer();
	
		//mens
		get_curr_Vars();
		createBarberMenu();
		create_Hairstyles();
		create_Beards();
		create_Eyebrows();
		create_Chest();
		create_Contacts();
		create_FacePaint();
		//create_MakeUp();
			

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

//################# Getting and Setting Vars #################
function get_curr_Vars() 
{
	//Create the main menu
	curr_hair = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_HAIR");
	curr_haircolor = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_HAIRCOLOR");
	curr_hairhighlight = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_HAIRHIGHLIGHT");
	curr_scalpCollection = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_SCALP_COLLECTION");
	curr_scalpOverlay = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_SCALP_OVERLAY");	
	curr_beard = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_FACIAL_HAIR");
	curr_beardcolor = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_FACIAL_HAIR_COLOR");	
	curr_beardopacity = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_FACIAL_HAIR_OPACITY");	
	curr_chest = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_CHEST_HAIR");	
	curr_chestcolor = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_CHEST_HAIR_COLOR");	
	curr_chestopacity = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_CHEST_HAIR_OPACITY");	
	curr_eyebrows = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_EYEBROWS");
	curr_eyebrowscolor = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_EYEBROWS_COLOR");
	curr_eyebrowsopacity = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_EYEBROWS_OPACITY");
	curr_contacts = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_EYE_COLOR");
	curr_facepaint = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_FACEPAINT");
	curr_facepaintopacity = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_FACEPAINT_OPACITY");
	curr_eyemakeup = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_MAKEUP");
	curr_eyemakeupopacity = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_MAKEUP_OPACITY");
	curr_lipstick = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_LIPSTICK");	
	curr_lipstickcolor = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_LIPSTICK_COLOR");
	curr_lipstickopacity = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_LIPSTICK_OPACITY");

	
}

function reset_curr_Vars() 
{
	//Create the main menu
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_Hair", curr_hair);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_HairColor", curr_haircolor);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_HairHighlight", curr_hairhighlight);
	API.getEntitySyncedData(API.getLocalPlayer(), "ESS_SCALP_COLLECTION", curr_scalpCollection);
	API.getEntitySyncedData(API.getLocalPlayer(), "ESS_SCALP_OVERLAY", curr_scalpOverlay);		
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_FACIAL_HAIR", curr_beard);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_FACIAL_HAIR_COLOR", curr_beardcolor);	
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_FACIAL_HAIR_OPACITY", curr_beardopacity);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_CHEST_HAIR", curr_chest);	
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_CHEST_HAIR_COLOR", curr_chestcolor);	
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_CHEST_HAIR_OPACITY", curr_chestopacity);	
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_EYEBROWS", curr_eyebrows);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_EYEBROWS_COLOR", curr_eyebrowscolor);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_EYEBROWS_OPACITY", curr_eyebrowsopacity);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_EYE_COLOR", curr_contacts);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_FACEPAINT", curr_facepaint);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_FACEPAINT_OPACITY", curr_facepaintopacity);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_MAKEUP", curr_eyemakeup);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_MAKEUP_OPACITY", curr_eyemakeupopacity);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_LIPSTICK", curr_lipstick);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_LIPSTICK_COLOR", curr_lipstickColor);
	API.setEntitySyncedData(API.getLocalPlayer(), "ESS_LIPSTICK_OPACITY", curr_lipstickopacity);
}

//################# Creating Menus #################
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
			HairColorID = curr_haircolor;
			HairColor2ID = curr_hairhighlight;
			API.setPlayerClothes(TheDude, 2, HairId, 0);
			API.clearPlayerAccessory(TheDude, 0);
			SetPlayerScalp(HairStylesItems[0][3],HairStylesItems[0][4]);
			API.callNative("_SET_PED_HAIR_COLOR", API.getLocalPlayer(), HairColorID, HairColor2ID);

			break;	

			case 1:
			get_curr_Vars();
			openBeardsMenu();
			var TheDude = API.getLocalPlayer();
			BeardId = BeardsItems[0][2];
			API.callNative("SET_PED_HEAD_OVERLAY", TheDude, 1, BeardId, API.f(1));
			API.callNative("_SET_PED_HEAD_OVERLAY_COLOR", TheDude, 1, 1, curr_beardcolor, curr_beardopacity);
			break;		

			case 2:
			get_curr_Vars();
			openEyebrowsMenu();
			var TheDude = API.getLocalPlayer();
			EyebrowId = EyebrowsItems[0][2];
			API.callNative("SET_PED_HEAD_OVERLAY", TheDude, 2, EyebrowId, API.f(1));
			API.callNative("_SET_PED_HEAD_OVERLAY_COLOR", TheDude, 1, 1, curr_eyebrowscolor, curr_eyebrowsopacity);
			break;		

			case 3:
			get_curr_Vars();
			openChestMenu();
			var TheDude = API.getLocalPlayer();
			ChestId = ChestItems[0][2];
			API.setPlayerClothes(TheDude, 11, 15, 0);
			var ChestColorID = curr_chestcolor;
			var ChestOpacityID = curr_chestopacity;
			API.callNative("SET_PED_HEAD_OVERLAY", TheDude, 10, ChestId, API.f(1));
			API.callNative("_SET_PED_HEAD_OVERLAY_COLOR", TheDude, 10, 1, curr_chestcolor, curr_chestopacity);
			break;		
			
			case 4:
			get_curr_Vars();
			openContactsMenu();
			var TheDude = API.getLocalPlayer();
			ContactsId = ContactsItems[0][2];
			API.callNative("_SET_PED_EYE_COLOR", TheDude, ContactsId);
			break;	

			case 5:
			get_curr_Vars();
			openFacePaintMenu();
			var TheDude = API.getLocalPlayer();
			FacePaintId = FacePaintItems[0][2];
			API.callNative("SET_PED_HEAD_OVERLAY", TheDude, 4, FacePaintId, curr_facepaintopacity);
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
	//Create the Hairstyles selection menu
	HairstylesMenu = API.createMenu("      ", "Hairstyles", 0, 0, 3);
    API.setMenuTitle(HairstylesMenu, "");
	API.setMenuBannerSprite(HairstylesMenu, "shopui_title_barber", "shopui_title_barber");


	menuPool.Add(HairstylesMenu);
	HairstylesMenu.Visible = false;
	

	HairstylesMenu.OnIndexChange.connect(function(sender, index)
	{
		get_curr_Vars();
		var TheDude = API.getLocalPlayer();
		HairId = HairStylesItems[index][2];
		HairColorID = curr_haircolor;
		HairColor2ID = curr_hairhighlight;
		API.setPlayerClothes(TheDude, 2, HairId, 0);	
		SetPlayerScalp(HairStylesItems[index][3],HairStylesItems[index][4]);
		API.callNative("_SET_PED_HAIR_COLOR", TheDude, HairColorID, HairColor2ID);
	});

	HairstylesMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = HairStylesItems[index][1];
		
		if (HairstylesMenu.MenuItems[index].RightLabel == ""){
			API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
			API.sendChatMessage("You already have this hairstyle!");
		} else {
			if ( vCost <= Pmoney){
				//send buy command here
				//get_curr_Vars();
				API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
				API.triggerServerEvent("BARBER_BUY", vCost, HairStylesItems[index][2], curr_hairColor, curr_hairhighlight);
				CloseMenu();
			}else {
				API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
			}
		}
	});
	
	HairstylesMenu.OnMenuClose.connect(function(sender, item, index)
	{
		var TheDude = API.getLocalPlayer();
		API.setPlayerAccessory(TheDude, 0, curr_hat, curr_hat_tex);
		barberMenu.Visible = true;
		HairstylesMenu.Visible = false; 
	});
}

//----------
function create_Beards() 
{
	//Create the Beards selection menu
	BeardsMenu = API.createMenu("      ", "Beards", 0, 0, 3);
    API.setMenuTitle(BeardsMenu, "");
	API.setMenuBannerSprite(BeardsMenu, "shopui_title_barber", "shopui_title_barber");


	menuPool.Add(BeardsMenu);
	BeardsMenu.Visible = false;
		
	
	BeardsMenu.OnIndexChange.connect(function(sender, index)
	{
		//get_curr_Vars();
		var TheDude = API.getLocalPlayer();
		BeardId = BeardsItems[index][2];
		BeardColorID = curr_beardcolor;
		BeardOpacity = curr_beardopacity;
		API.callNative("SET_PED_HEAD_OVERLAY", TheDude, 1, BeardId, API.f(1));
		API.callNative("_SET_PED_HEAD_OVERLAY_COLOR", TheDude, 1, 1, BeardColorID, BeardOpacity);
	});

	BeardsMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = BeardsItems[index][1];
		
		if (BeardsMenu.MenuItems[index].RightLabel == ""){
			API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
			API.sendChatMessage("You already have this Beard!");
		} else {
			if ( vCost <= Pmoney){
				//send buy command here
				//get_curr_Vars();
				API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
				API.triggerServerEvent("BEARD_BUY", vCost, BeardsItems[index][2], curr_beardcolor, curr_beardopacity);
				CloseMenu();
			}else {
				API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
			}
		}
	});
	
	BeardsMenu.OnMenuClose.connect(function(sender, item, index)
	{
		barberMenu.Visible = true;
		BeardsMenu.Visible = false; 
	});
}

//----------
function create_Eyebrows() 
{
	//Create the Eyebrows selection menu
	EyebrowsMenu = API.createMenu("      ", "Eyebrows", 0, 0, 3);
    API.setMenuTitle(EyebrowsMenu, "");
	API.setMenuBannerSprite(EyebrowsMenu, "shopui_title_barber", "shopui_title_barber");


	menuPool.Add(EyebrowsMenu);
	EyebrowsMenu.Visible = false;
		
	
	EyebrowsMenu.OnIndexChange.connect(function(sender, index)
	{
		//get_curr_Vars();
		var TheDude = API.getLocalPlayer();
		BrowId = EyebrowsItems[index][2];
		BrowColorID = curr_eyebrowscolor;
		BrowOpacityID = curr_eyebrowsopacity;
		API.callNative("SET_PED_HEAD_OVERLAY", TheDude, 2, BrowId, API.f(1));
		API.callNative("_SET_PED_HEAD_OVERLAY_COLOR", TheDude, 2, 1, BrowColorID, BrowOpacityID);
	});

	EyebrowsMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = EyebrowsItems[index][1];
		
		if (EyebrowsMenu.MenuItems[index].RightLabel == ""){
			API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
			API.sendChatMessage("You already have this Beard!");
		} else {
			if ( vCost <= Pmoney){
				//send buy command here
				//get_curr_Vars();
				API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
				API.triggerServerEvent("BROW_BUY", vCost, EyebrowsItems[index][2], curr_eyebrowscolor, curr_eyebrowsopacity);
				CloseMenu();
			}else {
				API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
			}
		}
	});
	
	EyebrowsMenu.OnMenuClose.connect(function(sender, item, index)
	{
		barberMenu.Visible = true;
		EyebrowsMenu.Visible = false; 
	});
}

//----------
function create_Chest() 
{
	//Create the Chest selection menu
	ChestMenu = API.createMenu("      ", "Chest", 0, 0, 3);
    API.setMenuTitle(ChestMenu, "");
	API.setMenuBannerSprite(ChestMenu, "shopui_title_barber", "shopui_title_barber");


	menuPool.Add(ChestMenu);
	ChestMenu.Visible = false;
		
	
	ChestMenu.OnIndexChange.connect(function(sender, index)
	{
		//get_curr_Vars();
		var TheDude = API.getLocalPlayer();
		var ChestId = ChestItems[index][2];
		var ChestColorID = curr_chestcolor;
		var ChestOpacityID = curr_chestopacity;
		API.callNative("SET_PED_HEAD_OVERLAY", TheDude, 10, ChestId, API.f(1));
		API.callNative("_SET_PED_HEAD_OVERLAY_COLOR", TheDude, 10, 1, curr_chestcolor, curr_chestopacity);
	});

	ChestMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = ChestItems[index][1];
		
		if (ChestMenu.MenuItems[index].RightLabel == ""){
			API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
			API.sendChatMessage("You already have this Chest Hair!");
		} else {
			if ( vCost <= Pmoney){
				//send buy command here
				//get_curr_Vars();
				API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
				API.triggerServerEvent("BROW_BUY", vCost, ChestItems[index][2], curr_hairColor, curr_hairhighlight);
				CloseMenu();
			}else {
				API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
			}
		}
	});
	
	ChestMenu.OnMenuClose.connect(function(sender, item, index)
	{
		var TheDude = API.getLocalPlayer();
		barberMenu.Visible = true;
		ChestMenu.Visible = false; 
		noTopless();
	});
}

//----------
function create_Contacts() 
{
	//Create the Contacts selection menu
	ContactsMenu = API.createMenu("      ", "Contacts", 0, 0, 3);
    API.setMenuTitle(ContactsMenu, "");
	API.setMenuBannerSprite(ContactsMenu, "shopui_title_barber", "shopui_title_barber");


	menuPool.Add(ContactsMenu);
	ContactsMenu.Visible = false;
		
	
	ContactsMenu.OnIndexChange.connect(function(sender, index)
	{
		//get_curr_Vars();
		var TheDude = API.getLocalPlayer();
		var ContactsId = ContactsItems[index][2];
		API.callNative("_SET_PED_EYE_COLOR", TheDude, ContactsId);
	});

	ContactsMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = ContactsItems[index][1];
		
		if (ContactsMenu.MenuItems[index].RightLabel == ""){
			API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
			API.sendChatMessage("You already have these contacts in!");
		} else {
			if ( vCost <= Pmoney){
				//send buy command here
				//get_curr_Vars();
				API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
				API.triggerServerEvent("BROW_BUY", vCost, ContactsItems[index][2]);
				CloseMenu();
			}else {
				API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
			}
		}
	});
	
	ContactsMenu.OnMenuClose.connect(function(sender, item, index)
	{
		var TheDude = API.getLocalPlayer();
		barberMenu.Visible = true;
		ContactsMenu.Visible = false; 
		noTopless();
	});
}

//----------
function create_FacePaint() 
{
	//Create the FacePaint selection menu
	FacePaintMenu = API.createMenu("      ", "Face Paint", 0, 0, 3);
    API.setMenuTitle(FacePaintMenu, "");
	API.setMenuBannerSprite(FacePaintMenu, "shopui_title_barber", "shopui_title_barber");


	menuPool.Add(FacePaintMenu);
	FacePaintMenu.Visible = false;
		
	
	FacePaintMenu.OnIndexChange.connect(function(sender, index)
	{
		get_curr_Vars();
		var TheDude = API.getLocalPlayer();
		var FacePaintId = FacePaintItems[index][2];
		var FacePaintOpacityID = API.f(1);
		API.callNative("SET_PED_HEAD_OVERLAY", TheDude, 4, FacePaintId, FacePaintOpacityID);
		API.sendChatMessage("Facepaint: " + FacePaintId + ", Opacity:" + FacePaintOpacityID);		
	});

	FacePaintMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = FacePaintItems[index][1];
		
		if (FacePaintMenu.MenuItems[index].RightLabel == ""){
			API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
			API.sendChatMessage("You already have this Face Paint!");
		} else {
			if ( vCost <= Pmoney){
				//send buy command here
				//get_curr_Vars();
				API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
				API.triggerServerEvent("BROW_BUY", vCost, FacePaintItems[index][2], curr_facepaintopacity);
				CloseMenu();
			}else {
				API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
			}
		}
	});
	
	FacePaintMenu.OnMenuClose.connect(function(sender, item, index)
	{
		var TheDude = API.getLocalPlayer();
		barberMenu.Visible = true;
		ContactsMenu.Visible = false; 
	});
}

//################# Opening Menus #################
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
			newitem.SetRightBadge(BadgeStyle.Tick);
			newitem.SetRightLabel("");
		}
        HairstylesMenu.AddItem(newitem);
	}
	HairstylesMenu.CurrentSelection = listCurrent;
}

// --------------------
function openBeardsMenu() 
{
	barberMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetBeardsMenu();
	BeardsMenu.Visible = true; 
}

function resetBeardsMenu()
{
	var vCurrent = curr_beard;
	var listCurrent = 0;
			
	BeardsMenu.Clear();
	for (var i = 0; i < BeardsItems.length; i++) 
	{
		var newitem = API.createMenuItem(BeardsItems[i][0], "");
		var vCost = BeardsItems[i][1];
		var isAdded = BeardsItems[i][2];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
		
		if ( isAdded == vCurrent){
			listCurrent = i;
			newitem.SetRightBadge(BadgeStyle.Tick);
			newitem.SetRightLabel("");
		}
        BeardsMenu.AddItem(newitem);
	}
	BeardsMenu.CurrentSelection = listCurrent;
}

// --------------------
function openEyebrowsMenu() 
{
	barberMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetEyebrowsMenu();
	EyebrowsMenu.Visible = true; 
}

function resetEyebrowsMenu()
{
	var vCurrent = curr_eyebrows;
	var listCurrent = 0;
			
	EyebrowsMenu.Clear();
	for (var i = 0; i < EyebrowsItems.length; i++) 
	{
		var newitem = API.createMenuItem(EyebrowsItems[i][0], "");
		var vCost = EyebrowsItems[i][1];
		var isAdded = EyebrowsItems[i][2];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
		
		if ( isAdded == vCurrent){
			listCurrent = i;
			newitem.SetRightBadge(BadgeStyle.Tick);
			newitem.SetRightLabel("");
		}
        EyebrowsMenu.AddItem(newitem);
	}
	EyebrowsMenu.CurrentSelection = listCurrent;
}

// --------------------
function openChestMenu() 
{
	barberMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetChestMenu();
	ChestMenu.Visible = true; 
	goTopless();
}

function resetChestMenu()
{
	var vCurrent = curr_chest;
	var listCurrent = 0;
			
	ChestMenu.Clear();
	for (var i = 0; i < ChestItems.length; i++) 
	{
		var newitem = API.createMenuItem(ChestItems[i][0], "");
		var vCost = ChestItems[i][1];
		var isAdded = ChestItems[i][2];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
		
		if ( isAdded == vCurrent){
			listCurrent = i;
			newitem.SetRightBadge(BadgeStyle.Tick);
			newitem.SetRightLabel("");
		}
        ChestMenu.AddItem(newitem);
	}
	ChestMenu.CurrentSelection = listCurrent;
}

// --------------------
function openContactsMenu() 
{
	barberMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetContactsMenu();
	ContactsMenu.Visible = true; 
}

function resetContactsMenu()
{
	var vCurrent = curr_chest;
	var listCurrent = 0;
			
	ContactsMenu.Clear();
	for (var i = 0; i < ContactsItems.length; i++) 
	{
		var newitem = API.createMenuItem(ContactsItems[i][0], "");
		var vCost = ContactsItems[i][1];
		var isAdded = ContactsItems[i][2];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
		
		if ( isAdded == vCurrent){
			listCurrent = i;
			newitem.SetRightBadge(BadgeStyle.Tick);
			newitem.SetRightLabel("");
		}
        ContactsMenu.AddItem(newitem);
	}
	ContactsMenu.CurrentSelection = listCurrent;
}

// --------------------
function openFacePaintMenu() 
{
	barberMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetFacePaintMenu();
	FacePaintMenu.Visible = true; 
}

function resetFacePaintMenu()
{
	var vCurrent = curr_chest;
	var listCurrent = 0;
			
	FacePaintMenu.Clear();
	for (var i = 0; i < FacePaintItems.length; i++) 
	{
		var newitem = API.createMenuItem(FacePaintItems[i][0], "");
		var vCost = FacePaintItems[i][1];
		var isAdded = FacePaintItems[i][2];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
		
		if ( isAdded == vCurrent){
			listCurrent = i;
			newitem.SetRightBadge(BadgeStyle.Tick);
			newitem.SetRightLabel("");
		}
        FacePaintMenu.AddItem(newitem);
	}
	FacePaintMenu.CurrentSelection = listCurrent;
}

//################# Extra Functions #################
function SetPlayerScalp(coll, olay) 
{
	var TheDude = API.getLocalPlayer();
	API.callNative("_CLEAR_PED_FACIAL_DECORATIONS", TheDude);
	API.callNative("_SET_PED_FACIAL_DECORATION", TheDude, API.getHashKey(coll), API.getHashKey(olay));
}

function goTopless() 
{
	var TheDude = API.getLocalPlayer();
	API.setPlayerClothes(TheDude, 11, 15, 0);
	API.setPlayerClothes(TheDude, 3, 15, 0);
	API.setPlayerClothes(TheDude, 8, 0, 111);
}

function noTopless() 
{
	var TheDude = API.getLocalPlayer();
	API.setPlayerClothes(TheDude, 11, curr_shirt, curr_shirt_tex);
	API.setPlayerClothes(TheDude, 3, 0, 0);
	API.setPlayerClothes(TheDude, 8, 0, 111);
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
	var TheDude = API.getLocalPlayer();
	API.setPlayerClothes(TheDude, 11, curr_shirt, curr_shirt_tex);
	API.setPlayerAccessory(TheDude, 0, curr_hat, curr_hat_tex);
	API.setHudVisible(true);
	API.setChatVisible(true);
	resource.PointHelper.togglePointHelpers();
	barberMenu.Visible = false;
	HairstylesMenu.Visible = false;
	BeardsMenu.Visible = false;
	EyebrowsMenu.Visible = false;
	ChestMenu.Visible = false;	
	ContactsMenu.Visible = false;
	FacePaintMenu.Visible = false;	
	noTopless();
	API.setActiveCamera(null);
	MenuOpen = false;
}

//################# All this SHIT is for the color menu. #################


