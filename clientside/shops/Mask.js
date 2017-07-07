var player = null;
var Pmoney = 0;
var ShoppingPos = new Vector3();
var MaskCamera = null;
var menuPool = null;

var MenuOpen = false;

var mainMenu = null;
var mainMenuItems = ["Animals", "Balaclavas", "Characters", "Clowns", "Crime Masks", "Cultural", "Festive", "Intimidation", "Monsters", "Paper Bags", "Ski Masks", "Sports", "Tactical", "Valentines", "Zombies", "Exit"];

var AnimalMenu = null;
var AnimalMenuItems = [
["Black and Tan Pug", 200, 100, 5],
["Josephine Pug", 200, 100, 4],
["Brown Pug", 200, 100, 3],
["Gray Pug", 200, 100, 2],
["Black Pug", 200, 100, 1],
["Moe Pug", 200, 100, 0],
["Zebra", 200, 97, 5],
["Pinto Horse", 200, 97, 4],
["Brown Horse", 200, 97, 3],
["Gray Horse", 200, 97, 2],
["Black Horse", 200, 97, 1],
["Chestnut Horse", 200, 97, 0],
["Albino Crazed Ape", 200, 96, 3],
["Gray Crazed Ape", 200, 96, 2],
["Orangutan Crazed Ape", 200, 96, 1],
["Silverback Crazed Ape", 200, 96, 0],
["Danger Dino", 200, 93, 5],
["Rainforest Dino", 200, 93, 4],
["Earth Dino", 200, 93, 3],
["Tropical Dino", 200, 93, 2],
["Gray Dino", 200, 93, 1],
["Striped Dino", 200, 93, 0],
["Black Wolf", 200, 26, 1],
["Grey Wolf", 200, 26, 0],
["Black Vulture", 200, 25, 1],
["Pink Vulture", 200, 25, 0],
["White Eagle", 200, 24, 1],
["Brown Eagle", 200, 24, 0],
["Brown Bull", 200, 23, 1],
["Black Bull", 200, 23, 0],
["Golden Bison", 200, 22, 1],
["Brown Bison", 200, 22, 0],
["Grey Bear", 200, 21, 1],
["Brown Bear", 200, 21, 0],
["Black Racoon", 200, 20, 1],
["Gray Racoon", 200, 20, 0],
["White Owl", 200, 19, 1],
["Brown Owl", 200, 19, 0],
["Brown Fox", 200, 18, 1],
["Red Fox", 200, 18, 0],
["Tabby Cat", 200, 17, 1],
["Gray Cat", 200, 17, 0],
["Pink Pig", 300, 1, 0],
["Brown Pig", 300, 1, 1],
["Bloody Pig", 300, 1, 2],
["Black Pig", 300, 1, 3],
["Tan Ape", 300, 5, 3],
["Brown Ape", 300, 5, 1],
["Green Ape ", 300, 5, 2],
["Pink Ape", 300, 5, 0]
];

var BalacavaMenu = null;
var BalacavaMenuItems = [
["Pink Stripe Knit Balaclava", 200, 58, 9],
["Black Stripe Knit Balaclava", 200, 58, 8],
["Blue Stripe Knit Balaclava", 200, 58, 7],
["Pogo Balaclava", 200, 58, 6],
["Impotent Rage Balaclava", 200, 58, 5],
["Orange Camo Knit Balaclava", 200, 58, 4],
["Pink Camo Knit Balaclava", 200, 58, 3],
["Neon Camo Knit Balaclava", 200, 58, 2],
["Nature Knit Balaclava", 200, 58, 1],
["Bandit Knit Balaclava", 200, 58, 0],
["Purple Knit Balaclava", 200, 57, 21],
["Orange Knit Balaclava", 200, 57, 20],
["Green Knit Balaclava", 200, 57, 19],
["Red Knit Balaclava", 200, 57, 18],
["Blue Knit Balaclava", 200, 57, 17],
["White Knit Balaclava", 200, 57, 16],
["Sessanta Nove Balaclava", 200, 57, 15],
["Perseus Balaclava", 200, 57, 14],
["Perseus Band Balaclava", 200, 57, 13],
["Didler Sacha Balaclava", 200, 57, 12],
["Princess Balaclava", 200, 57, 11],
["Flying Bravo Balaclava", 200, 57, 10],
["Flying Bravo FB Balaclava", 200, 57, 9],
["Pink Knit Balaclava", 200, 57, 8],
["Dirty Knit Balaclava", 200, 57, 7],
["Woodland Knit Balaclava", 200, 57, 6],
["Rainbow Knit Balaclava", 200, 57, 5],
["Brown Knit Balaclava", 200, 57, 4],
["Gray Knit Balaclava", 200, 57, 3],
["Copper Knit Balaclava", 200, 57, 2],
["Army Green Knit Balaclava", 200, 57, 1],
["Black Knit Balaclava", 200, 57, 0],
["Split Loose Balaclava", 200, 56, 8],
["Outback Loose Balaclava", 200, 56, 7],
["Red Loose Balaclava", 200, 56, 6],
["Woodland Loose Balaclava", 200, 56, 5],
["Bloody Loose Balaclava", 200, 56, 4],
["Khaki Loose Balaclava", 200, 56, 3],
["Skull Loose Balaclava", 200, 56, 2],
["Black Loose Balaclava", 200, 56, 1],
["Blue Loose Balaclava", 200, 56, 0],
["Scruffy Balaclava", 200, 37, 0]
];

var CharacterMenu = null;
var CharacterMenuItems = [
["Mime Plastic Face", 200, 50, 9],
["Puppet Plastic Face", 200, 50, 8],
["Doll Plastic Face", 200, 50, 7],
["Manneguin Plastic Face", 200, 50, 6],
["Brown Plastic Face", 200, 50, 5], 
["Black Plastic Face", 200, 50, 4], 
["Clown Plastic Face", 200, 50, 3], 
["Pink Plastic Face", 200, 50, 2],
["The Don Plastic Face", 200, 50, 1], 
["Green Plastic Face", 200, 50, 0],
["Moorehead", 200, 45, 0],
["Princess Robot Bubblegum", 200, 44, 0],
["Impotent Rage", 200, 43, 0],
["Pogo", 200, 3, 0]
];

var ClownMenu = null;
var ClownMenuItems = [
["Sinister Clown", 200, 95, 7],
["Franken Clown", 200, 95, 6],
["Neon Clown", 200, 95, 5],
["Scavenger Clown", 200, 95, 4],
["Orange Clown", 200, 95, 3], 
["Green Clown", 200, 95, 2], 
["Blue Clown", 200, 95, 1], 
["Red Clown", 200, 95, 0]
];

var CrimeMenu = null;
var CrimeMenuItems = [
["LSPD T-Shirt Mask", 200, 54, 10],
["Pink Camo T-Shirt Mask", 200, 54, 9],
["TPI T-Shirt Mask", 200, 54, 8],
["Love Fist T-Shirt Mask", 200, 54, 7],
["Stripy T-Shirt Mask", 200, 54, 6],
["Woodland T-Shirt Mask", 200, 54, 5],
["Justice T-Shirt Mask", 200, 54, 4],
["Benders T-Shirt Mask", 200, 54, 3],
["Tan T-Shirt Mask", 200, 54, 2],
["White T-Shirt Mask", 200, 54, 1],
["Black T-Shirt Mask", 200, 54, 0],
["Electric Skull Face Bandana", 200, 51, 9],
["Yellow Face Bandana", 200, 51, 8],
["Paisly Face Bandana", 200, 51, 7],
["Purple Face Bandana", 200, 51, 6],
["Green Face Bandana", 200, 51, 5],
["Forest Face Bandana", 200, 51, 4],
["Dessert Face Bandana", 200, 51, 3],
["Urban Face Bandana", 200, 51, 2],
["Skull Face Bandana", 200, 51, 1],
["Black Face Bandana", 200, 51, 0],
["Electrical Duct Tape", 200, 48, 3],
["White Duct Tape", 200, 48, 2],
["Dark Grey Duct Tape", 200, 48, 1],
["Light Grey Duct Tape", 200, 48, 0],
["Red Arrow Tape", 200, 47, 3],
["Hazard Tape", 200, 47, 2],
["Black Arrow Tape", 200, 47, 1],
["Crime Scene Tape", 200, 47, 0]
];

var CulturalMenu = null;
var CulturalMenuItems = [
["Purple Oni", 200, 105, 23],
["Sea Green Oni", 200, 105, 22],
["Stone Oni", 200, 105, 21],
["Gray and Gold Oni", 200, 105, 20],
["White Possessed Oni", 200, 105, 19],
["Gray and Orange Oni", 200, 105, 18],
["Black and Gold Oni", 200, 105, 17],
["Gray Oni", 200, 105, 16],
["Stone Possessed Oni", 200, 105, 15],
["Gold Stone Possessed Oni", 200, 105, 14],
["Orange Oni", 200, 105, 13],
["Black And Yellow Oni", 200, 105, 12],
["Grayscale Oni", 200, 105, 11],
["Plum Oni", 200, 105, 10],
["Yellow Oni", 200, 105, 9],
["Brown Oni", 200, 105, 8],
["Black Possessed Oni", 200, 105, 7],
["Black Painted Oni", 200, 105, 6],
["Red Painted Oni", 200, 105, 5],
["Gold Painted Oni", 200, 105, 4],
["White Painted Oni", 200, 105, 3],
["Sandstone Oni", 200, 105, 2],
["Weathered Oni", 200, 105, 1],
["Obsidian Oni", 200, 105, 0],
["Black Ornate Skull", 200, 99, 5],
["White Ornate Skull", 200, 99, 4],
["Teal Ornate Skull", 200, 99, 3],
["Blue Ornate Skull", 200, 99, 2],
["Silver Ornate Skull", 200, 99, 1],
["Red Ornate Skull", 200, 99, 0],
["Green Oni", 200, 14, 10],
["Gold Oni", 200, 14, 10],
["Black Oni", 200, 14, 10],
["White Oni", 200, 14, 10],
["Blue Oni", 200, 14, 10],
["Red Oni", 200, 14, 10],
["Fashion Hockey", 200, 14, 10],
["Tourist Hockey", 200, 14, 2],
["Vinewood Hockey", 200, 14, 1],
["Green Carnival", 200, 6, 3],
["Blue Carnival", 200, 6, 2],
["Black Carnival", 200, 6, 1],
["White Carnival", 200, 6, 0]
];

var FestiveMenu = null;
var FestiveMenuItems = [
["Stocking", 200, 6, 0]
];

var IntimidationMenu = null;
var IntimidationMenuItems = [
["Please Stop Me Hockey", 200, 30, 0],
["Wooden Warrior", 200, 16, 8], 
["Lighting Warrior", 200, 16, 7],
["Stone Warrior", 200, 16, 6],
["Deadeye Warrior", 200, 16, 5],
["Carbon Warrior", 200, 16, 4],
["Neon Warrior", 200, 16, 3],
["Molten Warrior", 200, 16, 2],
["Circiuit Warrior", 200, 16, 1],
["Metal Warrior", 200, 16, 0],
["Bear Hockey", 200, 14, 6],
["Beast Hockey", 200, 14, 5],
["Wolf Hockey", 200, 14, 4],
["Hound Hockey", 200, 14, 3],
["Bullet Hockey", 200, 14, 0],
["White Hockey", 200, 4, 0],
["Black Bloody Hockey", 200, 4, 2]
];

var MonsterMenu = null;
var MonsterMenuItems = [
["Striped Skull", 200, 108, 23],
["Terracotta Skull", 200, 108, 22],
["Leather Solar-Eyed Skull", 200, 108, 21],
["Orange Swirl-Eyed Skull", 200, 108, 20],
["Mustard Painted Skull", 200, 108, 19],
["Green Painted Skull", 200, 108, 18],
["Pink Painted Skull", 200, 108, 17],
["Blue Painted Skull", 200, 108, 16],
["Tattooed Skull", 200, 108, 15],
["Wide Eyed Skull", 200, 108, 14],
["Possessed Skull", 200, 108, 13],
["Orange Open-Eyed Skull", 200, 108, 12],
["Chocolate Leather Skull", 200, 108, 11],
["Tan Leather Skull", 200, 108, 10],
["Stained Skull", 200, 108, 9],
["Inked Skull", 200, 108, 8],
["Sand Skull", 200, 108, 7],
["Moss Skull", 200, 108, 6],
["Fleshy Skull", 200, 108, 5],
["Fresh Skull", 200, 108, 4],
["Venom Skull", 200, 108, 3],
["Aged Skull", 200, 108, 2],
["Weathered Skull", 200, 108, 1],
["Clean Skull", 200, 108, 0],
["Infernal Sea Beast", 200, 92, 5],
["Deity Sea Beast", 200, 92, 4],
["Otherworldly Sea Beast", 200, 92, 3],
["Reptilian Sea Beast", 200, 92, 2],
["Alien Sea Beast", 200, 92, 1],
["Amphibian Sea Beast", 200, 92, 0],
["Gray Frank", 200, 42, 1],
["Pale Frank", 200, 42, 0],
["Blue Vampyr", 200, 41, 1],
["White Vampyr", 200, 41, 0],
["Green Mummy", 200, 40, 1],
["White Mummy", 200, 40, 0],
["Brown Infected", 200, 39, 1],
["Pink Infected", 200, 39, 0],
["Green Skeletal", 200, 29, 1],
["Tan Skeletal", 200, 29, 1],
["Charcoal Skeletal", 200, 29, 1],
["Gray Skeletal", 200, 29, 1],
["Black Skeletal", 200, 29, 1],
["Pale Stitched Hockey", 200, 15, 2],
["Stitched Hockey", 200, 15, 1],
["Skull Hockey", 200, 15, 0],
["Electric Hockey", 200, 14, 15],
["Nightmare Hockey", 200, 14, 14],
["Flame Skull Hockey", 200, 14, 13],
["Rotten Zombie Hockey", 200, 14, 12],
["Vile Zombie Hockey", 200, 14, 11],
["Silver Skull", 200, 2, 0],
["Gray Skull", 200, 2, 2],
["Black Skull", 200, 2, 3],
["Bone Skull", 200, 2, 1],
["Green Monster", 200, 7, 0],
["Red Monster", 200, 7, 1],
["Black Monster", 200, 7, 2],
["White Monster", 200, 7, 3]
];

var PaperBagMenu = null;
var PaperBagMenuItems = [
["Blackout Paper Bag", 200, 49, 25], 
["Love Paper Bag", 200, 49, 24],
["Modernist Paper Bag", 200, 49, 23],
["Sticker Paper Bag", 200, 49, 22],
["Dapper Paper Bag", 200, 49, 21],
["The Bird Paper Bag", 200, 49, 20],
["Puzzle Paper Bag", 200, 49, 19],
["Help Me Paper Bag", 200, 49, 18],
["Alien Paper Bag", 200, 49, 17],
["Pink Paper Bag", 200, 49, 16],
["Dog Paper Bag", 200, 49, 15],
["Skull Paper Bag", 200, 49, 14],
["Zigzag Paper Bag", 200, 49, 13],
["Fury Paper Bag", 200, 49, 12],
["Monster Paper Bag", 200, 49, 11],
["Cop Paper Bag", 200, 49, 10],
["Diabolic Paper Bag", 200, 49, 9],
["Kill Me Paper Bag", 200, 49, 8],
["Burger Shot Paper Bag", 200, 49, 7],
["Shy Paper Bag", 200, 49, 6],
["Mouth Paper Bag", 200, 49, 5],
["Fat Cat Paper Bag", 200, 49, 4],
["Happy Paper Bag", 200, 49, 3],
["Sad Paper Bag", 200, 49, 2],
["Manic Paper Bag", 200, 49, 1],
["Up-n-Atom Paper Bag", 200, 49, 0]
];




var HolidayMenu = null;
var HolidayMenuItems = ["If you see this","It means that I","Done goofed on","Something."];

var CameraPos = new Vector3(-1336.287, -1277.151, 5.679597);
var CameraRot = new Vector3(0, 0, 120);


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
	createMainMenu();
	createAnimalMenu();
	createBalacavaMenu();
	createCrimeMenu();	
	createCharacterMenu();	
	createClownMenu();	
	createCulturalMenu();	
	createFestiveMenu();	
	createIntimidationMenu();	
	createMonsterMenu();		
	createPaperBagMenu();		
	createHolidayMenu();
});

API.onServerEventTrigger.connect(function (eventName, args) 
{
  switch (eventName) 
  {
    case 'OPEN_MASK_MENU':
	if (!MenuOpen) {
		//ShoppingPos = args[0];
		openMainMenu();
		}
    break;
  }
});

function createMainMenu() 
{
	//Create the main menu
	mainMenu = API.createMenu(" ", 0, 0, 3);
	API.setMenuTitle(mainMenu, "");
	API.setMenuBannerSprite(mainMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");
	
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
			openAnimalMenu();
			var TheDude = API.getLocalPlayer();
			MaskId = AnimalMenuItems[0][2];
			TextureId = AnimalMenuItems[0][3];
			API.setPlayerClothes(TheDude, 1, MaskId, TextureId);
			break;

			case 1:
			openBalacavaMenu();
			var TheDude = API.getLocalPlayer();
			MaskId = BalacavaMenuItems[0][2];
			TextureId = BalacavaMenuItems[0][3];
			API.setPlayerClothes(TheDude, 1, MaskId, TextureId);
			break;

			case 2:
			openCharacterMenu();
			var TheDude = API.getLocalPlayer();
			MaskId = CharacterMenuItems[0][2];
			TextureId = CharacterMenuItems[0][3];
			API.setPlayerClothes(TheDude, 1, MaskId, TextureId);			
			break;

			case 3:
			openClownMenu();
			var TheDude = API.getLocalPlayer();
			MaskId = ClownMenuItems[0][2];
			TextureId = ClownMenuItems[0][3];
			API.setPlayerClothes(TheDude, 1, MaskId, TextureId);
			break;			

			case 4:
			openCrimeMenu();
			var TheDude = API.getLocalPlayer();
			MaskId = CrimeMenuItems[0][2];
			TextureId = CrimeMenuItems[0][3];
			API.setPlayerClothes(TheDude, 1, MaskId, TextureId);
			break;
			
			case 5:
			openCulturalMenu();
			var TheDude = API.getLocalPlayer();
			MaskId = CulturalMenuItems[0][2];
			TextureId = CulturalMenuItems[0][3];
			API.setPlayerClothes(TheDude, 1, MaskId, TextureId);
			break;
			
			case 6:
			openFestiveMenu();
			var TheDude = API.getLocalPlayer();
			MaskId = FestiveMenuItems[0][2];
			TextureId = FestiveMenuItems[0][3];
			API.setPlayerClothes(TheDude, 1, MaskId, TextureId);			
			break;
			
			case 7:
			openIntimidationMenu();
			var TheDude = API.getLocalPlayer();
			MaskId = IntimidationMenuItems[0][2];
			TextureId = IntimidationMenuItems[0][3];
			API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
			break;
			
			case 8:
			openMonsterMenu();
			var TheDude = API.getLocalPlayer();
			MaskId = MonsterMenuItems[0][2];
			TextureId = MonsterMenuItems[0][3];
			API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
			break;			
			
			case 9:
			openPaperBagMenu();
			var TheDude = API.getLocalPlayer();
			MaskId = PaperBagMenuItems[0][2];
			TextureId = PaperBagMenuItems[0][3];
			API.setPlayerClothes(TheDude, 1, MaskId, TextureId);
			break;
			
			case 10:
			openHolidayMenu();
			break;
			
			case 11:
			openHolidayMenu();
			break;
			
			case 12:
			openHolidayMenu();
			break;
			
			case 13:
			openHolidayMenu();
			break;

			case 14:
			openHolidayMenu();
			break;
			
			case 15:
			CloseMenu();
			break;
		}
	});
}

//----------
function createAnimalMenu() 
{
	//Create the Animal mask selection menu
	AnimalMenu = API.createMenu("      ", 0, 0, 3);
    API.setMenuTitle(AnimalMenu, "");
	API.setMenuBannerSprite(AnimalMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(AnimalMenu);
	AnimalMenu.Visible = false; 
	
	AnimalMenu.OnIndexChange.connect(function(sender, index)
	{
		if (index == (AnimalMenuItems.length)) {
			API.setPlayerClothes(API.getLocalPlayer(), 1, 0, 0);
			return;
		}
		var TheDude = API.getLocalPlayer();
		MaskId = AnimalMenuItems[index][2];
		TextureId = AnimalMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	AnimalMenu.OnItemSelect.connect(function(sender, item, index)
	{
		if (index == (AnimalMenuItems.length)) {			
			mainMenu.Visible = true;
			AnimalMenu.Visible = false; 
			return;
		}
		// CALL SERVER "buy" code.
		CloseMenu();
	});
}


//----------
function createBalacavaMenu() 
{
	//Create the Animal mask selection menu
	BalacavaMenu = API.createMenu("      ", 0, 0, 3);
    API.setMenuTitle(BalacavaMenu, "");
	API.setMenuBannerSprite(BalacavaMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(BalacavaMenu);
	BalacavaMenu.Visible = false; 
	
	BalacavaMenu.OnIndexChange.connect(function(sender, index)
	{
		if (index == (BalacavaMenuItems.length)) {
			API.setPlayerClothes(API.getLocalPlayer(), 1, 0, 0);
			return;
		}
		var TheDude = API.getLocalPlayer();
		MaskId = BalacavaMenuItems[index][2];
		TextureId = BalacavaMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	BalacavaMenu.OnItemSelect.connect(function(sender, item, index)
	{
		if (index == (BalacavaMenuItems.length)) {			
			mainMenu.Visible = true;
			BalacavaMenu.Visible = false; 
			return;
		}
		// CALL SERVER "buy" code.
		CloseMenu();
	});
}

//----------
function createCharacterMenu() 
{
	//Create the Character mask selection menu
	CharacterMenu = API.createMenu("      ", 0, 0, 3);
    API.setMenuTitle(CharacterMenu, "");
	API.setMenuBannerSprite(CharacterMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(CharacterMenu);
	CharacterMenu.Visible = false; 
	
	CharacterMenu.OnIndexChange.connect(function(sender, index)
	{
		if (index == (CharacterMenuItems.length)) {
			API.setPlayerClothes(API.getLocalPlayer(), 1, 0, 0);
			return;
		}
		var TheDude = API.getLocalPlayer();
		MaskId = CharacterMenuItems[index][2];
		TextureId = CharacterMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	CharacterMenu.OnItemSelect.connect(function(sender, item, index)
	{
		if (index == (CharacterMenuItems.length)) {			
			mainMenu.Visible = true;
			CharacterMenu.Visible = false; 
			return;
		}
		// CALL SERVER "buy" code.
		CloseMenu();
	});
}

//----------
function createClownMenu() 
{
	//Create the Clown mask selection menu
	ClownMenu = API.createMenu("      ", 0, 0, 3);
    API.setMenuTitle(ClownMenu, "");
	API.setMenuBannerSprite(ClownMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(ClownMenu);
	ClownMenu.Visible = false; 
	
	ClownMenu.OnIndexChange.connect(function(sender, index)
	{
		if (index == (ClownMenuItems.length)) {
			API.setPlayerClothes(API.getLocalPlayer(), 1, 0, 0);
			return;
		}
		var TheDude = API.getLocalPlayer();
		MaskId = ClownMenuItems[index][2];
		TextureId = ClownMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	ClownMenu.OnItemSelect.connect(function(sender, item, index)
	{
		if (index == (ClownMenuItems.length)) {			
			mainMenu.Visible = true;
			ClownMenu.Visible = false; 
			return;
		}
		// CALL SERVER "buy" code.
		CloseMenu();
	});
}

//----------
function createCrimeMenu() 
{
	//Create the Crime mask selection menu
	CrimeMenu = API.createMenu("      ", 0, 0, 3);
    API.setMenuTitle(CrimeMenu, "");
	API.setMenuBannerSprite(CrimeMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(CrimeMenu);
	CrimeMenu.Visible = false; 
	
	CrimeMenu.OnIndexChange.connect(function(sender, index)
	{
		if (index == (CrimeMenuItems.length)) {
			API.setPlayerClothes(API.getLocalPlayer(), 1, 0, 0);
			return;
		}
		var TheDude = API.getLocalPlayer();
		MaskId = CrimeMenuItems[index][2];
		TextureId = CrimeMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	CrimeMenu.OnItemSelect.connect(function(sender, item, index)
	{
		if (index == (CrimeMenuItems.length)) {			
			mainMenu.Visible = true;
			CrimeMenu.Visible = false; 
			return;
		}
		// CALL SERVER "buy" code.
		CloseMenu();
	});
}

//----------
function createCulturalMenu() 
{
	//Create the Cultural mask selection menu
	CulturalMenu = API.createMenu("      ", 0, 0, 3);
    API.setMenuTitle(CulturalMenu, "");
	API.setMenuBannerSprite(CulturalMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(CulturalMenu);
	CulturalMenu.Visible = false; 
	
	CulturalMenu.OnIndexChange.connect(function(sender, index)
	{
		if (index == (CulturalMenuItems.length)) {
			API.setPlayerClothes(API.getLocalPlayer(), 1, 0, 0);
			return;
		}
		var TheDude = API.getLocalPlayer();
		MaskId = CulturalMenuItems[index][2];
		TextureId = CulturalMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	CulturalMenu.OnItemSelect.connect(function(sender, item, index)
	{
		if (index == (CulturalMenuItems.length)) {			
			mainMenu.Visible = true;
			CulturalMenu.Visible = false; 
			return;
		}
		// CALL SERVER "buy" code.
		CloseMenu();
	});
}

//----------
function createFestiveMenu() 
{
	//Create the Festive mask selection menu
	FestiveMenu = API.createMenu("      ", 0, 0, 3);
    API.setMenuTitle(FestiveMenu, "");
	API.setMenuBannerSprite(FestiveMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(FestiveMenu);
	FestiveMenu.Visible = false; 
	
	FestiveMenu.OnIndexChange.connect(function(sender, index)
	{
		if (index == (FestiveMenuItems.length)) {
			API.setPlayerClothes(API.getLocalPlayer(), 1, 0, 0);
			return;
		}
		var TheDude = API.getLocalPlayer();
		MaskId = FestiveMenuItems[index][2];
		TextureId = FestiveMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	FestiveMenu.OnItemSelect.connect(function(sender, item, index)
	{
		if (index == (FestiveMenuItems.length)) {			
			mainMenu.Visible = true;
			FestiveMenu.Visible = false; 
			return;
		}
		// CALL SERVER "buy" code.
		CloseMenu();
	});
}

//----------
function createIntimidationMenu() 
{
	//Create the Intimidation mask selection menu
	IntimidationMenu = API.createMenu("      ", 0, 0, 3);
    API.setMenuTitle(IntimidationMenu, "");
	API.setMenuBannerSprite(IntimidationMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(IntimidationMenu);
	IntimidationMenu.Visible = false; 
	
	IntimidationMenu.OnIndexChange.connect(function(sender, index)
	{
		if (index == (IntimidationMenuItems.length)) {
			API.setPlayerClothes(API.getLocalPlayer(), 1, 0, 0);
			return;
		}
		var TheDude = API.getLocalPlayer();
		MaskId = IntimidationMenuItems[index][2];
		TextureId = IntimidationMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	IntimidationMenu.OnItemSelect.connect(function(sender, item, index)
	{
		if (index == (IntimidationMenuItems.length)) {			
			mainMenu.Visible = true;
			IntimidationMenu.Visible = false; 
			return;
		}
		// CALL SERVER "buy" code.
		CloseMenu();
	});
}

//----------
function createMonsterMenu() 
{
	//Create the Monster mask selection menu
	MonsterMenu = API.createMenu("      ", 0, 0, 3);
    API.setMenuTitle(MonsterMenu, "");
	API.setMenuBannerSprite(MonsterMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(MonsterMenu);
	MonsterMenu.Visible = false; 
	
	MonsterMenu.OnIndexChange.connect(function(sender, index)
	{
		if (index == (MonsterMenuItems.length)) {
			API.setPlayerClothes(API.getLocalPlayer(), 1, 0, 0);
			return;
		}
		var TheDude = API.getLocalPlayer();
		MaskId = MonsterMenuItems[index][2];
		TextureId = MonsterMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	MonsterMenu.OnItemSelect.connect(function(sender, item, index)
	{
		if (index == (MonsterMenuItems.length)) {			
			mainMenu.Visible = true;
			MonsterMenu.Visible = false; 
			return;
		}
		// CALL SERVER "buy" code.
		CloseMenu();
	});
}

//----------
function createPaperBagMenu() 
{
	//Create the PaperBag mask selection menu
	PaperBagMenu = API.createMenu("      ", 0, 0, 3);
    API.setMenuTitle(PaperBagMenu, "");
	API.setMenuBannerSprite(PaperBagMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(PaperBagMenu);
	PaperBagMenu.Visible = false; 
	
	PaperBagMenu.OnIndexChange.connect(function(sender, index)
	{
		if (index == (PaperBagMenuItems.length)) {
			API.setPlayerClothes(API.getLocalPlayer(), 1, 0, 0);
			return;
		}
		var TheDude = API.getLocalPlayer();
		MaskId = PaperBagMenuItems[index][2];
		TextureId = PaperBagMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	PaperBagMenu.OnItemSelect.connect(function(sender, item, index)
	{
		if (index == (PaperBagMenuItems.length)) {			
			mainMenu.Visible = true;
			PaperBagMenu.Visible = false; 
			return;
		}
		// CALL SERVER "buy" code.
		CloseMenu();
	});
}




function createHolidayMenu() 
{
	//Create the Holiday mask selection menu
	HolidayMenu = API.createMenu("      ", 0, 0, 3);
    API.setMenuTitle(HolidayMenu, "");		
	API.setMenuBannerSprite(HolidayMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");
	
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
	MenuOpen = true;
	CamOn();
    mainMenu.CurrentSelection = 0;
	mainMenu.Visible = true; 
}

// --------------------
function openAnimalMenu() 
{
	mainMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetAnimalMenu();
	AnimalMenu.Visible = true; 
}

function resetAnimalMenu()
{
	AnimalMenu.Clear();
	for (var i = 0; i < AnimalMenuItems.length; i++) 
	{
		var newitem = API.createMenuItem(AnimalMenuItems[i][0], "");
		var vCost = AnimalMenuItems[i][1];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
        AnimalMenu.AddItem(newitem);
	}
	AnimalMenu.AddItem(API.createMenuItem("Back", ""));
	AnimalMenu.CurrentSelection = 0;
}

// --------------------
function openBalacavaMenu() 
{
	mainMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetBalacavaMenu();
	BalacavaMenu.Visible = true; 
}

function resetBalacavaMenu()
{
	BalacavaMenu.Clear();
	for (var i = 0; i < BalacavaMenuItems.length; i++) 
	{
		var newitem = API.createMenuItem(BalacavaMenuItems[i][0], "");
		var vCost = BalacavaMenuItems[i][1];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
        BalacavaMenu.AddItem(newitem);
	}
	BalacavaMenu.AddItem(API.createMenuItem("Back", ""));
	BalacavaMenu.CurrentSelection = 0;
}

// --------------------
function openCharacterMenu() 
{
	mainMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetCharacterMenu();
	CharacterMenu.Visible = true; 
}

function resetCharacterMenu()
{
	CharacterMenu.Clear();
	for (var i = 0; i < CharacterMenuItems.length; i++) 
	{
		var newitem = API.createMenuItem(CharacterMenuItems[i][0], "");
		var vCost = CharacterMenuItems[i][1];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
        CharacterMenu.AddItem(newitem);
	}
	CharacterMenu.AddItem(API.createMenuItem("Back", ""));
	CharacterMenu.CurrentSelection = 0;
}

// --------------------
function openClownMenu() 
{
	mainMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetClownMenu();
	ClownMenu.Visible = true; 
}

function resetClownMenu()
{
	ClownMenu.Clear();
	for (var i = 0; i < ClownMenuItems.length; i++) 
	{
		var newitem = API.createMenuItem(ClownMenuItems[i][0], "");
		var vCost = ClownMenuItems[i][1];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
        ClownMenu.AddItem(newitem);
	}
	ClownMenu.AddItem(API.createMenuItem("Back", ""));
	ClownMenu.CurrentSelection = 0;
}

// --------------------
function openCrimeMenu() 
{
	mainMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetCrimeMenu();
	CrimeMenu.Visible = true; 
}

function resetCrimeMenu()
{
	CrimeMenu.Clear();
	for (var i = 0; i < CrimeMenuItems.length; i++) 
	{
		var newitem = API.createMenuItem(CrimeMenuItems[i][0], "");
		var vCost = CrimeMenuItems[i][1];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
        CrimeMenu.AddItem(newitem);
	}
	CrimeMenu.AddItem(API.createMenuItem("Back", ""));
	CrimeMenu.CurrentSelection = 0;
}

// --------------------
function openCulturalMenu() 
{
	mainMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetCulturalMenu();
	CulturalMenu.Visible = true; 
}

function resetCulturalMenu()
{
	CulturalMenu.Clear();
	for (var i = 0; i < CulturalMenuItems.length; i++) 
	{
		var newitem = API.createMenuItem(CulturalMenuItems[i][0], "");
		var vCost = CulturalMenuItems[i][1];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
        CulturalMenu.AddItem(newitem);
	}
	CulturalMenu.AddItem(API.createMenuItem("Back", ""));
	CulturalMenu.CurrentSelection = 0;
}

// --------------------
function openFestiveMenu() 
{
	mainMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetFestiveMenu();
	FestiveMenu.Visible = true; 
}

function resetFestiveMenu()
{
	FestiveMenu.Clear();
	for (var i = 0; i < FestiveMenuItems.length; i++) 
	{
		var newitem = API.createMenuItem(FestiveMenuItems[i][0], "");
		var vCost = FestiveMenuItems[i][1];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
        FestiveMenu.AddItem(newitem);
	}
	FestiveMenu.AddItem(API.createMenuItem("Back", ""));
	FestiveMenu.CurrentSelection = 0;
}

// --------------------
function openIntimidationMenu() 
{
	mainMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetIntimidationMenu();
	IntimidationMenu.Visible = true; 
}

function resetIntimidationMenu()
{
	IntimidationMenu.Clear();
	for (var i = 0; i < IntimidationMenuItems.length; i++) 
	{
		var newitem = API.createMenuItem(IntimidationMenuItems[i][0], "");
		var vCost = IntimidationMenuItems[i][1];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
        IntimidationMenu.AddItem(newitem);
	}
	IntimidationMenu.AddItem(API.createMenuItem("Back", ""));
	IntimidationMenu.CurrentSelection = 0;
}

// --------------------
function openMonsterMenu() 
{
	mainMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetMonsterMenu();
	MonsterMenu.Visible = true; 
}

function resetMonsterMenu()
{
	MonsterMenu.Clear();
	for (var i = 0; i < MonsterMenuItems.length; i++) 
	{
		var newitem = API.createMenuItem(MonsterMenuItems[i][0], "");
		var vCost = MonsterMenuItems[i][1];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
        MonsterMenu.AddItem(newitem);
	}
	MonsterMenu.AddItem(API.createMenuItem("Back", ""));
	MonsterMenu.CurrentSelection = 0;
}

// --------------------
function openPaperBagMenu() 
{
	mainMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetPaperBagMenu();
	PaperBagMenu.Visible = true; 
}

function resetPaperBagMenu()
{
	PaperBagMenu.Clear();
	for (var i = 0; i < PaperBagMenuItems.length; i++) 
	{
		var newitem = API.createMenuItem(PaperBagMenuItems[i][0], "");
		var vCost = PaperBagMenuItems[i][1];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
        PaperBagMenu.AddItem(newitem);
	}
	PaperBagMenu.AddItem(API.createMenuItem("Back", ""));
	PaperBagMenu.CurrentSelection = 0;
}




function openHolidayMenu() 
{
	mainMenu.Visible = false;
	HolidayMenu.Visible = true; 
}

function CamOn() 
{
	//set the camera
	MaskCamera = API.createCamera(CameraPos, new Vector3());
	API.pointCameraAtPosition(MaskCamera, API.getEntityPosition(API.getLocalPlayer()).Add(new Vector3(0, 0, .75)));
	API.setActiveCamera(MaskCamera);
}

function CloseMenu() 
{
	mainMenu.Visible = false;
	AnimalMenu.Visible = false;
	BalacavaMenu.Visible = false;
	CharacterMenu.Visible = false;	
	ClownMenu.Visible = false;		
	CrimeMenu.Visible = false;	
	CulturalMenu.Visible = false;		
	FestiveMenu.Visible = false;			
	IntimidationMenu.Visible = false;		
	MonsterMenu.Visible = false;		
	PaperBagMenu.Visible = false;		
	API.setActiveCamera(null);
	MenuOpen = false;
}
