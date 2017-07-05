var menuPool = null;

var MenuOpen = false;

var mainMenu = null;
var mainMenuItems = ["Masks", "Balacavas", "Holiday", "", "Close Menu"];

var MaskMenu = null;
var MaskMenuItems = ["Pig","Skull","Ape","Owl"];

var BalacavaMenu = null;
var BalacavaMenuItems = ["Black","Loose","Striped"];

var HolidayMenu = null;
var HolidayMenuItems = ["Snowman","Santa","Reindeer"];

var CameraPos = new Vector3(-1336.287, -1277.151, 3.879597);
var CameraRot = new Vector3(0, 0, 120);


API.onUpdate.connect(function(s, e) 
{
	if (menuPool != null) 
	{
        menuPool.ProcessMenus();
    }
});

//When ModMenu is started, create all the required menus and hide them
API.onResourceStart.connect(function(s, e) 
{
	menuPool = API.getMenuPool();
	createMainMenu();
	createMaskMenu();
	createBalacavaMenu();
	createHolidayMenu();
});

API.onServerEventTrigger.connect(function (eventName, args) 
{
  switch (eventName) 
  {
    case 'OPEN_MASK_MENU':
	if (MenuOpen = false) {
	openMainMenu();
	}
    break;
  }
});

function createMainMenu() 
{
	
	MenuOpen = true;
	//set the camera
	var player = API.getLocalPlayer();
	MaskCamera = API.createCamera(CameraPos, CameraRot);
	//API.pointCameraAtPosition(MaskCamera, player.position);
	API.setActiveCamera(MaskCamera);
	//Create the main menu
	mainMenu = API.createMenu("Mask Menu", 0, 0, 6);
	
	for (var i = 0; i < mainMenuItems.length; i++) 
	{
		mainMenu.AddItem(API.createMenuItem(mainMenuItems[i], ""));
	}

	mainMenu.CurrentSelection = 0;
	menuPool.Add(mainMenu);
	mainMenu.Visible = false; 
	
	//Gets called when we select a category
	mainMenu.OnItemSelect.connect(function(sender, item, index)
	{
		switch (index) 
		{
			case 0:
			openMaskMenu();
			break;

			case 1:
			openBalacavaMenu();
			break;

			case 2:
			openHolidayMenu();
			break;

			case 4:
			CloseMenu();
			break;
		}
	});
}

function createMaskMenu() 
{
	//Create the main mask selection menu
	MaskMenu = API.createMenu("Masks", 0, 0, 6);
	
	for (var i = 0; i < MaskMenuItems.length; i++) 
	{
		MaskMenu.AddItem(API.createMenuItem(MaskMenuItems[i], ""));
	}
	MaskMenu.AddItem(API.createMenuItem("Back", ""));
	MaskMenu.CurrentSelection = 0;
	menuPool.Add(MaskMenu);
	MaskMenu.Visible = false; 
	
	//Gets called when we select a mod category
	MaskMenu.OnItemSelect.connect(function(sender, item, index)
	{
		//the back button first.
		if (index == (MaskMenuItems.length)) {
			mainMenu.Visible = true;
			MaskMenu.Visible = false; 
		}
		
	});
}

function createBalacavaMenu() 
{
	//Create the balacava mask selection menu
	BalacavaMenu = API.createMenu("Balacavas", 0, 0, 6);
	
	for (var i = 0; i < BalacavaMenuItems.length; i++) 
	{
		BalacavaMenu.AddItem(API.createMenuItem(BalacavaMenuItems[i], ""));
	}
	BalacavaMenu.AddItem(API.createMenuItem("Back", ""));	

	BalacavaMenu.CurrentSelection = 0;
	menuPool.Add(BalacavaMenu);
	BalacavaMenu.Visible = false; 
	
	//Gets called when we select a mod category
	BalacavaMenu.OnItemSelect.connect(function(sender, item, index)
	{
		//the back button first.		
		if (index == (BalacavaMenuItems.length)) {			
			mainMenu.Visible = true;
			BalacavaMenu.Visible = false; 
		}

	});
}

function createHolidayMenu() 
{
	//Create the Holiday mask selection menu
	HolidayMenu = API.createMenu("Holiday Masks", 0, 0, 6);
	
	for (var i = 0; i < HolidayMenuItems.length; i++) 
	{
		HolidayMenu.AddItem(API.createMenuItem(HolidayMenuItems[i], ""));
	}
	HolidayMenu.AddItem(API.createMenuItem("Back", ""));	

	HolidayMenu.CurrentSelection = 0;
	menuPool.Add(HolidayMenu);
	HolidayMenu.Visible = false; 
	
	//Gets called when we select a mod category
	HolidayMenu.OnItemSelect.connect(function(sender, item, index)
	{
		//the back button first.
		if (index == (HolidayMenuItems.length)) {
			mainMenu.Visible = true;
			HolidayMenu.Visible = false; 	
		}
		
	});
}

function openMainMenu() 
{
    mainMenu.CurrentSelection = 0;
	mainMenu.Visible = true; 
}

function openMaskMenu() 
{
	mainMenu.Visible = false;
	MaskMenu.Visible = true; 
}

function openBalacavaMenu() 
{
	mainMenu.Visible = false;
	BalacavaMenu.Visible = true; 
}

function openHolidayMenu() 
{
	mainMenu.Visible = false;
	HolidayMenu.Visible = true; 
}

function CloseMenu() 
{
	mainMenu.Visible = false;
	API.setActiveCamera(null);
	MenuOpen = false;
}