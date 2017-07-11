var player = null;
var Pmoney = 0;
var ShoppingPos = new Vector3();
var MaskCamera = null;
var menuPool = null;

var MenuOpen = false;

var mainMenu = null;
var mainMenuItems = ["Animals", "Balaclavas", "Characters", "Clowns", "Crime Masks", "Cultural", "Festive", "Intimidation", "Monsters", "Paper Bags", "Ski Masks", "Sports", "Tactical", "Valentines", "Zombies"];

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
["Teen Wolf", 200, 59, 0],
["Albino Teen Wolf", 200, 79, 2],
["Tan Teen Wolf", 200, 79, 1],
["Dark Teen Wolf", 200, 79, 0],
["White Gangsta Teen Wolf", 200, 80, 2],
["Red Gangsta Teen Wolf", 200, 80, 1],
["Black Gangsta Teen Wolf", 200, 80, 0],
["Albino Visor Teen Wolf", 200, 81, 2],
["Dark Visor Teen Wolf", 200, 81, 1],
["Tan Visor Teen Wolf", 200, 81, 0],
["Albino Workout Teen Wolf", 200, 82, 2],
["Dark Workout Teen Wolf", 200, 82, 1],
["Tan Workout Teen Wolf", 200, 82, 0],
["Albino Santa Teen Wolf", 200, 83, 2],
["Dark Santa Teen Wolf", 200, 83, 1],
["Tan Santa Teen Wolf", 200, 83, 0],
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
["Berserker", 200, 91, 5],
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
["Green Oni", 200, 94, 5],
["Gold Oni", 200, 94, 4],
["Black Oni", 200, 94, 3],
["White Oni", 200, 94, 2],
["Blue Oni", 200, 94, 1],
["Red Oni", 200, 94, 0],
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
["Latino Mrs Claus", 200, 88, 2],
["Black Mrs Claus", 200, 88, 1],
["White Mrs Claus", 200, 88, 0],
["Badass Bad Elf", 200, 87, 2],
["Gangsta Bad Elf", 200, 87, 1],
["Rebel Bad Elf", 200, 87, 0],
["High Mrs Claus", 200, 86, 2],
["Smashed Mrs Claus", 200, 86, 1],
["Wasted Mrs Claus", 200, 86, 0],
["Burnt Turkey", 200, 85, 2],
["Cooked Turkey", 200, 85, 1],
["Raw Turkey", 200, 85, 0],
["Light Pudding", 200, 78, 1],
["Dark Pudding", 200, 78, 0],
["Purple Festive Luchador", 200, 77, 5],
["Red Festive Luchador", 200, 77, 4],
["White Festive Luchador", 200, 77, 3],
["Black Festive Luchador", 200, 77, 2],
["Dark Green Festive Luchador", 200, 77, 1],
["Green Festive Luchador", 200, 77, 0],
["Filty Bad Santa", 200, 76, 2],
["Grumpy Bad Santa", 200, 76, 1],
["Bruised Bad Santa", 200, 76, 0],
["Brown Crazy Gingerbread", 200, 75, 2],
["Blue Crazy Gingerbread", 200, 75, 1],
["Pink Crazy Gingerbread", 200, 75, 0],
["Angry Gingerbread", 200, 74, 2],
["Mad Gingerbread", 200, 74, 1],
["Manic Gingerbread", 200, 74, 0],
["Latino Elf", 200, 34, 2],
["Black Elf", 200, 34, 1],
["White Elf", 200, 34, 0],
["Gingerbread", 200, 33, 0],
["Stocking", 200, 32, 0],
["Penguin", 200, 31, 0],
["Snowman Mask", 200, 10, 0],
["Reindeer Mask", 200, 9, 0],
["Latino Santa", 200, 8, 2],
["Black Santa", 200, 8, 1],
["White Santa", 200, 8, 0]
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
["Black Bearded Lucifer", 200, 72, 2],
["Orange Bearded Lucifer", 200, 72, 1],
["Red Bearded Lucifer", 200, 72, 0],
["White Haggard Witch", 200, 71, 2],
["Gray Haggard Witch", 200, 71, 1],
["Yellow Haggard Witch", 200, 71, 0],
["Red Hypnotic Alien", 200, 70, 2],
["Green Hypnotic Alien", 200, 70, 1],
["Blue Hypnotic Alien", 200, 70, 0],
["Black Sack Slasher", 200, 69, 2],
["Bloody Sack Slasher", 200, 69, 1],
["Classic Sack Slasher", 200, 69, 0],
["Black Classic Lucifer", 200, 68, 2],
["Orange Classic Lucifer", 200, 68, 1],
["Red Classic Lucifer", 200, 68, 0],
["Scabby Sewer Creature", 200, 67, 2],
["Rotten Sewer Creature", 200, 67, 1],
["Dirty Sewer Creature", 200, 67, 0],
["Purple Toxic Insect", 200, 66, 2],
["Red Toxic Insect", 200, 66, 1],
["Green Toxic Insect", 200, 66, 0],
["Gray Lycanthrope", 200, 65, 2],
["Dark Lycanthrope", 200, 65, 1],
["Pale Lycanthrope", 200, 65, 0],
["Cream Skull Burst", 200, 64, 2],
["Red Skull Burst", 200, 64, 1],
["White Skull Burst", 200, 64, 0],
["Gray Flayed Demon", 200, 63, 2],
["Green Flayed Demon", 200, 63, 1],
["Red Flayed Demon", 200, 63, 0],
["Black Scalded Psycho", 200, 62, 2],
["Bloody Scalded Psycho", 200, 62, 1],
["White Scalded Psycho", 200, 62, 0],
["Rotten Butler", 200, 61, 2],
["Dead Butler", 200, 61, 1],
["Creepy Butler", 200, 61, 0],
["Nasty Watermelon", 200, 60, 2],
["Rotten Pumpkin", 200, 60, 1],
["Evil Pumpkin", 200, 60, 0],
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
["Silver Skull", 200, 2, 3],
["Gray Skull", 200, 2, 2],
["Black Skull", 200, 2, 1],
["Bone Skull", 200, 2, 0],
["Green Monster", 200, 7, 3],
["Red Monster", 200, 7, 2],
["Black Monster", 200, 7, 1],
["White Monster", 200, 7, 0]
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

var SkiMenu = null;
var SkiMenuItems = [
["Skull Tactical Ski", 200, 104, 25],
["Olive Tactical Ski", 200, 104, 24],
["Pink Camo Tactical Ski", 200, 104, 23],
["Purple Camo Tactical Ski", 200, 104, 22],
["Tan Camo Tactical Ski", 200, 104, 21],
["Green Camo Tactical Ski", 200, 104, 20],
["Sand Tactical Ski", 200, 104, 19],
["Moss Tactical Ski", 200, 104, 18],
["Light Woodland Tactical Ski", 200, 104, 17],
["Flecktarn Tactical Ski", 200, 104, 16],
["Brushstroke Tactical Ski", 200, 104, 15],
["Peach Camo Tactical Ski", 200, 104, 14],
["Cobble Tactical Ski", 200, 104, 13],
["Contrast Tactical Ski", 200, 104, 12],
["Splinter Tactical Ski", 200, 104, 11],
["Aqua Camo Tactical Ski", 200, 104, 10],
["Gray Woodland Tactical Ski", 200, 104, 9],
["Moss Digital Tactical Ski", 200, 104, 8],
["Crosshatch Tactical Ski", 200, 104, 7],
["Dark Woodland Tactical Ski", 200, 104, 6],
["Fall Tactical Ski", 200, 104, 5],
["Peach Digital Tactical Ski", 200, 104, 4],
["Gray Digital Tactical Ski", 200, 104, 3],
["Green Digital Tactical Ski", 200, 104, 2],
["Brown Digital Tactical Ski", 200, 104, 1],
["Blue Digital Tactical Ski", 200, 104, 0],
["Blue Camo Bigness Face", 200, 101, 15],
["Gray Leopard Bigness Face", 200, 101, 14],
["Gray Abstract Bigness Face", 200, 101, 13],
["Pale Abstract Bigness Face", 200, 101, 12],
["Bold Abstract Bigness Face", 200, 101, 11],
["Zebra Bigness Face", 200, 101, 10],
["Black Bigness Face", 200, 101, 9],
["Geo Camo Bigness Face", 200, 101, 8],
["Gray Camo Bigness Face", 200, 101, 7],
["Camo Bigness Face", 200, 101, 6],
["Gray Bigness Face", 200, 101, 5],
["Fall Bigness Face", 200, 101, 4],
["Yellow Bigness Face", 200, 101, 3],
["Magenta Bigness Face", 200, 101, 2],
["Blue Bigness Face", 200, 101, 1],
["Orange Bigness Face", 200, 101, 0],
["Khaki Toggle Ski", 200, 55, 1],
["Charcoal Toggle Ski", 200, 55, 0],
["Skull Hooded Ski", 200, 53, 8],
["Urban Hooded Ski", 200, 53, 7],
["Forest Hooded Ski", 200, 53, 6],
["Charcoal Hooded Ski", 200, 53, 5],
["Khaki Hooded Ski", 200, 53, 4],
["Green Hooded Ski", 200, 53, 3],
["White Hooded Ski", 200, 53, 2],
["Gray Hooded Ski", 200, 53, 1],
["Black Hooded Ski", 200, 53, 0],
["Skull Tight Ski", 200, 52, 10],
["Yellow Tight Ski", 200, 52, 9],
["Blue Tight Ski", 200, 52, 8],
["Urban Tight Ski", 200, 52, 7],
["Forest Tight Ski", 200, 52, 6],
["Charcoal Tight Ski", 200, 52, 5],
["Khaki Tight Ski", 200, 52, 4],
["Green Tight Ski", 200, 52, 3],
["White Tight Ski", 200, 52, 2],
["Gray Tight Ski", 200, 52, 1],
["Black Tight Ski", 200, 52, 0]
];

var SportMenu = null;
var SportMenuItems = [
["Crossed Rampage Hockey", 200, 15, 3],
["Royal Hockey", 200, 14, 9],
["Striped Rampage Hockey", 200, 14, 8],
["Dust Devils Hockey", 200, 14, 7], 
["Red Hockey", 200, 4, 1],
["Dust Devils Hockey", 200, 4, 3]
];

var TacticalMenu = null;
var TacticalMenuItems = [
["White Snake Skull", 200, 106, 25],
["Red Snake Skull", 200, 106, 24],
["Pink Camo Snake Skull", 200, 106, 23],
["Purple Camo Skull", 200, 106, 22],
["Tan Camo Snake Skull", 200, 106, 21],
["Green Camo Snake Skull", 200, 106, 20],
["Sand Snake Skull", 200, 106, 19],
["Moss Snake Skull", 200, 106, 18],
["Light Woodland Snake Skull", 200, 106, 17],
["Flecktarn Snake Skull", 200, 106, 16],
["Brush Stroke Snake Skull", 200, 106, 15],
["Peach Camo Snake Skull", 200, 106, 14],
["Cobble Snake Skull", 200, 106, 13],
["Contrast Camo Snake Skull", 200, 106, 12],
["Splinter Snake Skull", 200, 106, 11],
["Aqua Camo Snake Skull", 200, 106, 10],
["Gray Woodland Snake Skull", 200, 106, 9],
["Moss Digital Snake Skull", 200, 106, 8],
["Crosshatch Snake Skull", 200, 106, 7],
["Dark Woodland Snake Skull", 200, 106, 6],
["Fall Snake Snake Skull", 200, 106, 5],
["Peach Digital Snake Skull", 200, 106, 4],
["Gray Digital Snake Skull", 200, 106, 3],
["Green Snake Skull", 200, 106, 2],
["Brown Snake Skull", 200, 106, 1],
["Blue Snake Skull", 200, 106, 0],
["Forest Combat Mask", 200, 28, 4],
["Tan Combat Mask", 200, 28, 3],
["Charcoal Combat Mask", 200, 28, 2],
["Gray Combat Mask", 200, 28, 1], 
["Black Combat Mask", 200, 28, 0],
["Gas Mask", 200, 38, 0],
["Gas Mask Breather", 200, 36, 0],
["Gas Mask Face", 200, 46, 0],
];

var ValentinesMenu = null;
var ValentinesMenuItems = [
["Cupid", 200, 13, 0],
["Black & Gold Masquerade", 200, 12, 2],
["Silver Masqurade", 200, 12, 1],
["Bronze Masqurade", 200, 12, 0],
["Black Mysterious", 200, 11, 2],
["Red Mysterious", 200, 11, 1],
["White Mysterious", 200, 11, 0]
];

var ZombieMenu = null;
var ZombieMenuItems = [
["Moss Camo Putrefied Zombie", 200, 103, 25],
["Woodland Putrefied Zombie", 200, 103, 24],
["Green Putrefied Zombie", 200, 103, 23],
["Stone Putrefied Zombie", 200, 103, 22],
["Slate Putrefied Zombie", 200, 103, 21],
["Black Putrefied Zombie", 200, 103, 20],
["Sand Zombie", 200, 103, 19],
["Moss Zombie", 200, 103, 18],
["Light Woodland Zombie", 200, 103, 17],
["Flecktarn Zombie", 200, 103, 16],
["Brushstroke Zombie", 200, 103, 15],
["Peach Camo Zombie", 200, 103, 14],
["Cobble Zombie", 200, 103, 13],
["Contrast Camo Zombie", 200, 103, 12],
["Splinter Zombie", 200, 103, 11],
["Aqua Camo Zombie", 200, 103, 10],
["Gray Woodland Zombie", 200, 103, 9],
["Moss Digital Zombie", 200, 103, 8],
["Crosshatch Zombie", 200, 103, 7],
["Dark Woodland Zombie", 200, 103, 6],
["Fall Zombie", 200, 103, 5],
["Peach Digital Zombie", 200, 103, 4],
["Gray Digital Zombie", 200, 103, 3],
["Green Digital Zombie", 200, 103, 2],
["Brown Digital Zombie", 200, 103, 1],
["Blue Digital Zombie", 200, 103, 0]
];

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
	createSkiMenu();
	createSportMenu();
	createTacticalMenu();
	createValentinesMenu();
	createZombieMenu();
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
	mainMenu = API.createMenu(" ", "Main Menu", 0, 0, 3);
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
			openSkiMenu();
			var TheDude = API.getLocalPlayer();
			MaskId = SkiMenuItems[0][2];
			TextureId = SkiMenuItems[0][3];
			API.setPlayerClothes(TheDude, 1, MaskId, TextureId);
			break;
			
			case 11:
			openSportMenu();
			var TheDude = API.getLocalPlayer();
			MaskId = SportMenuItems[0][2];
			TextureId = SportMenuItems[0][3];
			API.setPlayerClothes(TheDude, 1, MaskId, TextureId);
			break;
			
			case 12:
			openTacticalMenu();
			var TheDude = API.getLocalPlayer();
			MaskId = TacticalMenuItems[0][2];
			TextureId = TacticalMenuItems[0][3];
			API.setPlayerClothes(TheDude, 1, MaskId, TextureId);
			break;
			
			case 13:
			openValentinesMenu();
			var TheDude = API.getLocalPlayer();
			MaskId = ValentinesMenuItems[0][2];
			TextureId = ValentinesMenuItems[0][3];
			API.setPlayerClothes(TheDude, 1, MaskId, TextureId);
			break;

			case 14:
			openZombieMenu();
			var TheDude = API.getLocalPlayer();
			MaskId = ZombieMenuItems[0][2];
			TextureId = ZombieMenuItems[0][3];
			API.setPlayerClothes(TheDude, 1, MaskId, TextureId);
			break;
			
		}
	});
	
	//Gets called when we Close this out
	mainMenu.OnMenuClose.connect(function(sender, item, index)
	{
		CloseMenu();
	});
	
}

//----------
function createAnimalMenu() 
{
	//Create the Animal mask selection menu
	AnimalMenu = API.createMenu("      ", "Animals", 0, 0, 3);
    API.setMenuTitle(AnimalMenu, "");
	API.setMenuBannerSprite(AnimalMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(AnimalMenu);
	AnimalMenu.Visible = false; 
	
	AnimalMenu.OnIndexChange.connect(function(sender, index)
	{
		var TheDude = API.getLocalPlayer();
		MaskId = AnimalMenuItems[index][2];
		TextureId = AnimalMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	AnimalMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = AnimalMenuItems[index][1];
			
		if ( vCost <= Pmoney){
			//send buy command here
			API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
			API.triggerServerEvent("Buy_Mask", vCost, AnimalMenuItems[index][2], AnimalMenuItems[index][3]);
			CloseMenu();
		}else {
		    API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
		}
	});
	
	AnimalMenu.OnMenuClose.connect(function(sender, item, index)
	{
		mainMenu.Visible = true;
		AnimalMenu.Visible = false; 
		resetMainMenu();
	});
}


//----------
function createBalacavaMenu() 
{
	//Create the Animal mask selection menu
	BalacavaMenu = API.createMenu("      ", "Balaclavas", 0, 0, 3);
    API.setMenuTitle(BalacavaMenu, "");
	API.setMenuBannerSprite(BalacavaMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(BalacavaMenu);
	BalacavaMenu.Visible = false; 
	
	BalacavaMenu.OnIndexChange.connect(function(sender, index)
	{
		var TheDude = API.getLocalPlayer();
		MaskId = BalacavaMenuItems[index][2];
		TextureId = BalacavaMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	BalacavaMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = BalacavaMenuItems[index][1];
			
		if ( vCost <= Pmoney){
			//send buy command here
			API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
			API.triggerServerEvent("Buy_Mask", vCost, BalacavaMenuItems[index][2], BalacavaMenuItems[index][3]);
			CloseMenu();
		}else {
		    API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
		}
	});
	
	BalacavaMenu.OnMenuClose.connect(function(sender, item, index)
	{
		mainMenu.Visible = true;
		BalacavaMenu.Visible = false; 
		resetMainMenu();
	});
}

//----------
function createCharacterMenu() 
{
	//Create the Character mask selection menu
	CharacterMenu = API.createMenu("      ", "Characters", 0, 0, 3);
    API.setMenuTitle(CharacterMenu, "");
	API.setMenuBannerSprite(CharacterMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(CharacterMenu);
	CharacterMenu.Visible = false; 
	
	CharacterMenu.OnIndexChange.connect(function(sender, index)
	{
		var TheDude = API.getLocalPlayer();
		MaskId = CharacterMenuItems[index][2];
		TextureId = CharacterMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	CharacterMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = CharacterMenuItems[index][1];
			
		if ( vCost <= Pmoney){
			//send buy command here
			API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
			API.triggerServerEvent("Buy_Mask", vCost, CharacterMenuItems[index][2], CharacterMenuItems[index][3]);
			CloseMenu();
		}else {
		    API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
		}
	});
	
	CharacterMenu.OnMenuClose.connect(function(sender, item, index)
	{
		mainMenu.Visible = true;
		CharacterMenu.Visible = false; 
		resetMainMenu();
	});
}

//----------
function createClownMenu() 
{
	//Create the Clown mask selection menu
	ClownMenu = API.createMenu("      ", "Clowns", 0, 0, 3);
    API.setMenuTitle(ClownMenu, "");
	API.setMenuBannerSprite(ClownMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(ClownMenu);
	ClownMenu.Visible = false; 
	
	ClownMenu.OnIndexChange.connect(function(sender, index)
	{
		var TheDude = API.getLocalPlayer();
		MaskId = ClownMenuItems[index][2];
		TextureId = ClownMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	ClownMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = ClownMenuItems[index][1];
			
		if ( vCost <= Pmoney){
			//send buy command here
			API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
			API.triggerServerEvent("Buy_Mask", vCost, ClownMenuItems[index][2], ClownMenuItems[index][3]);
			CloseMenu();
		}else {
		    API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
		}
	});
	
	ClownMenu.OnMenuClose.connect(function(sender, item, index)
	{
		mainMenu.Visible = true;
		ClownMenu.Visible = false; 
		resetMainMenu();
	});	
}

//----------
function createCrimeMenu() 
{
	//Create the Crime mask selection menu
	CrimeMenu = API.createMenu("      ", "Crime", 0, 0, 3);
    API.setMenuTitle(CrimeMenu, "");
	API.setMenuBannerSprite(CrimeMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(CrimeMenu);
	CrimeMenu.Visible = false; 
	
	CrimeMenu.OnIndexChange.connect(function(sender, index)
	{
		var TheDude = API.getLocalPlayer();
		MaskId = CrimeMenuItems[index][2];
		TextureId = CrimeMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	CrimeMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = CrimeMenuItems[index][1];
			
		if ( vCost <= Pmoney){
			//send buy command here
			API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
			API.triggerServerEvent("Buy_Mask", vCost, CrimeMenuItems[index][2], CrimeMenuItems[index][3]);
			CloseMenu();
		}else {
		    API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
		}
	});
	
	CrimeMenu.OnMenuClose.connect(function(sender, item, index)
	{
		mainMenu.Visible = true;
		CrimeMenu.Visible = false; 
		resetMainMenu();
	});	
}

//----------
function createCulturalMenu() 
{
	//Create the Cultural mask selection menu
	CulturalMenu = API.createMenu("      ", "Cultural", 0, 0, 3);
    API.setMenuTitle(CulturalMenu, "");
	API.setMenuBannerSprite(CulturalMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(CulturalMenu);
	CulturalMenu.Visible = false; 
	
	CulturalMenu.OnIndexChange.connect(function(sender, index)
	{
		var TheDude = API.getLocalPlayer();
		MaskId = CulturalMenuItems[index][2];
		TextureId = CulturalMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	CulturalMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = CulturalMenuItems[index][1];
			
		if ( vCost <= Pmoney){
			//send buy command here
			API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
			API.triggerServerEvent("Buy_Mask", vCost, CulturalMenuItems[index][2], CulturalMenuItems[index][3]);
			CloseMenu();
		}else {
		    API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
		}
	});
	
	CulturalMenu.OnMenuClose.connect(function(sender, item, index)
	{
		mainMenu.Visible = true;
		CulturalMenu.Visible = false; 
		resetMainMenu();
	});		
}

//----------
function createFestiveMenu() 
{
	//Create the Festive mask selection menu
	FestiveMenu = API.createMenu("      ", "Festive", 0, 0, 3);
    API.setMenuTitle(FestiveMenu, "");
	API.setMenuBannerSprite(FestiveMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(FestiveMenu);
	FestiveMenu.Visible = false; 
	
	FestiveMenu.OnIndexChange.connect(function(sender, index)
	{
		var TheDude = API.getLocalPlayer();
		MaskId = FestiveMenuItems[index][2];
		TextureId = FestiveMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	FestiveMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = FestiveMenuItems[index][1];
			
		if ( vCost <= Pmoney){
			//send buy command here
			API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
			API.triggerServerEvent("Buy_Mask", vCost, FestiveMenuItems[index][2], FestiveMenuItems[index][3]);
			CloseMenu();
		}else {
		    API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
		}
	});
	
	FestiveMenu.OnMenuClose.connect(function(sender, item, index)
	{
		mainMenu.Visible = true;
		FestiveMenu.Visible = false; 
		resetMainMenu();
	});		
}

//----------
function createIntimidationMenu() 
{
	//Create the Intimidation mask selection menu
	IntimidationMenu = API.createMenu("      ", "Intimidation", 0, 0, 3);
    API.setMenuTitle(IntimidationMenu, "");
	API.setMenuBannerSprite(IntimidationMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(IntimidationMenu);
	IntimidationMenu.Visible = false; 
	
	IntimidationMenu.OnIndexChange.connect(function(sender, index)
	{
		var TheDude = API.getLocalPlayer();
		MaskId = IntimidationMenuItems[index][2];
		TextureId = IntimidationMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	IntimidationMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = IntimidationMenuItems[index][1];
			
		if ( vCost <= Pmoney){
			//send buy command here
			API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
			API.triggerServerEvent("Buy_Mask", vCost, IntimidationMenuItems[index][2], IntimidationMenuItems[index][3]);
			CloseMenu();
		}else {
		    API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
		}
	});
	
	IntimidationMenu.OnMenuClose.connect(function(sender, item, index)
	{
		mainMenu.Visible = true;
		IntimidationMenu.Visible = false; 
		resetMainMenu();
	});		
}

//----------
function createMonsterMenu() 
{
	//Create the Monster mask selection menu
	MonsterMenu = API.createMenu("      ", "Monsters", 0, 0, 3);
    API.setMenuTitle(MonsterMenu, "");
	API.setMenuBannerSprite(MonsterMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(MonsterMenu);
	MonsterMenu.Visible = false; 
	
	MonsterMenu.OnIndexChange.connect(function(sender, index)
	{
		var TheDude = API.getLocalPlayer();
		MaskId = MonsterMenuItems[index][2];
		TextureId = MonsterMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	MonsterMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = MonsterMenuItems[index][1];
			
		if ( vCost <= Pmoney){
			//send buy command here
			API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
			API.triggerServerEvent("Buy_Mask", vCost, MonsterMenuItems[index][2], MonsterMenuItems[index][3]);
			CloseMenu();
		}else {
		    API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
		}
	});
	
	MonsterMenu.OnMenuClose.connect(function(sender, item, index)
	{
		mainMenu.Visible = true;
		MonsterMenu.Visible = false; 
		resetMainMenu();
	});		
}

//----------
function createPaperBagMenu() 
{
	//Create the PaperBag mask selection menu
	PaperBagMenu = API.createMenu("      ", "Paper Bags", 0, 0, 3);
    API.setMenuTitle(PaperBagMenu, "");
	API.setMenuBannerSprite(PaperBagMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(PaperBagMenu);
	PaperBagMenu.Visible = false; 
	
	PaperBagMenu.OnIndexChange.connect(function(sender, index)
	{
		var TheDude = API.getLocalPlayer();
		MaskId = PaperBagMenuItems[index][2];
		TextureId = PaperBagMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	PaperBagMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = PaperBagMenuItems[index][1];
			
		if ( vCost <= Pmoney){
			//send buy command here
			API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
			API.triggerServerEvent("Buy_Mask", vCost, PaperBagMenuItems[index][2], PaperBagMenuItems[index][3]);
			CloseMenu();
		}else {
		    API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
		}
	});
	
	PaperBagMenu.OnMenuClose.connect(function(sender, item, index)
	{
		mainMenu.Visible = true;
		PaperBagMenu.Visible = false; 
		resetMainMenu();
	});		
}

//----------
function createSkiMenu() 
{
	//Create the Ski mask selection menu
	SkiMenu = API.createMenu("      ", "Ski Masks", 0, 0, 3);
    API.setMenuTitle(SkiMenu, "");
	API.setMenuBannerSprite(SkiMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(SkiMenu);
	SkiMenu.Visible = false; 
	
	SkiMenu.OnIndexChange.connect(function(sender, index)
	{
		var TheDude = API.getLocalPlayer();
		MaskId = SkiMenuItems[index][2];
		TextureId = SkiMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	SkiMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = SkiMenuItems[index][1];
			
		if ( vCost <= Pmoney){
			//send buy command here
			API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
			API.triggerServerEvent("Buy_Mask", vCost, SkiMenuItems[index][2], SkiMenuItems[index][3]);
			CloseMenu();
		}else {
		    API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
		}
	});
	
	SkiMenu.OnMenuClose.connect(function(sender, item, index)
	{
		mainMenu.Visible = true;
		SkiMenu.Visible = false; 
		resetMainMenu();
	});		
}

//----------
function createSportMenu() 
{
	//Create the Sport mask selection menu
	SportMenu = API.createMenu("      ", "Sports", 0, 0, 3);
    API.setMenuTitle(SportMenu, "");
	API.setMenuBannerSprite(SportMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(SportMenu);
	SportMenu.Visible = false; 
	
	SportMenu.OnIndexChange.connect(function(sender, index)
	{
		var TheDude = API.getLocalPlayer();
		MaskId = SportMenuItems[index][2];
		TextureId = SportMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	SportMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = SportMenuItems[index][1];
			
		if ( vCost <= Pmoney){
			//send buy command here
			API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
			API.triggerServerEvent("Buy_Mask", vCost, SportMenuItems[index][2], SportMenuItems[index][3]);
			CloseMenu();
		}else {
		    API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
		}
	});
	
	SportMenu.OnMenuClose.connect(function(sender, item, index)
	{
		mainMenu.Visible = true;
		SportMenu.Visible = false; 
		resetMainMenu();
	});		
}

//----------
function createTacticalMenu() 
{
	//Create the Tactical mask selection menu
	TacticalMenu = API.createMenu("      ", "Tactical", 0, 0, 3);
    API.setMenuTitle(TacticalMenu, "");
	API.setMenuBannerSprite(TacticalMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(TacticalMenu);
	TacticalMenu.Visible = false; 
	
	TacticalMenu.OnIndexChange.connect(function(sender, index)
	{
		var TheDude = API.getLocalPlayer();
		MaskId = TacticalMenuItems[index][2];
		TextureId = TacticalMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	TacticalMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = TacticalMenuItems[index][1];
			
		if ( vCost <= Pmoney){
			//send buy command here
			API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
			API.triggerServerEvent("Buy_Mask", vCost, TacticalMenuItems[index][2], TacticalMenuItems[index][3]);
			CloseMenu();
		}else {
		    API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
		}
	});
	
	TacticalMenu.OnMenuClose.connect(function(sender, item, index)
	{
		mainMenu.Visible = true;
		TacticalMenu.Visible = false; 
		resetMainMenu();
	});		
}

//----------
function createValentinesMenu() 
{
	//Create the Valentines mask selection menu
	ValentinesMenu = API.createMenu("      ", "Valentines", 0, 0, 3);
    API.setMenuTitle(ValentinesMenu, "");
	API.setMenuBannerSprite(ValentinesMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(ValentinesMenu);
	ValentinesMenu.Visible = false; 
	
	ValentinesMenu.OnIndexChange.connect(function(sender, index)
	{
		var TheDude = API.getLocalPlayer();
		MaskId = ValentinesMenuItems[index][2];
		TextureId = ValentinesMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	ValentinesMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = ValentinesMenuItems[index][1];
			
		if ( vCost <= Pmoney){
			//send buy command here
			API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
			API.triggerServerEvent("Buy_Mask", vCost, ValentinesMenuItems[index][2], ValentinesMenuItems[index][3]);
			CloseMenu();
		}else {
		    API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
		}
	});
	
	ValentinesMenu.OnMenuClose.connect(function(sender, item, index)
	{
		mainMenu.Visible = true;
		ValentinesMenu.Visible = false; 
		resetMainMenu();
	});		
}

//----------
function createZombieMenu() 
{
	//Create the Zombie mask selection menu
	ZombieMenu = API.createMenu("      ", "Zombies", 0, 0, 3);
    API.setMenuTitle(ZombieMenu, "");
	API.setMenuBannerSprite(ZombieMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");


	menuPool.Add(ZombieMenu);
	ZombieMenu.Visible = false; 
	
	ZombieMenu.OnIndexChange.connect(function(sender, index)
	{
		var TheDude = API.getLocalPlayer();
		MaskId = ZombieMenuItems[index][2];
		TextureId = ZombieMenuItems[index][3];
		API.setPlayerClothes(TheDude, 1, MaskId, TextureId);	
	});

	ZombieMenu.OnItemSelect.connect(function(sender, item, index)
	{
		// CALL SERVER "buy" code.
		Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
		var vCost = ZombieMenuItems[index][1];
			
		if ( vCost <= Pmoney){
			//send buy command here
			API.playSoundFrontEnd("PROPERTY_PURCHASE", "HUD_AWARDS");
			API.triggerServerEvent("Buy_Mask", vCost, ZombieMenuItems[index][2], ZombieMenuItems[index][3]);
			CloseMenu();
		}else {
		    API.playSoundFrontEnd("Pre_Screen_Stinger", "DLC_HEISTS_FAILED_SCREEN_SOUNDS");
		}
	});
	
	ZombieMenu.OnMenuClose.connect(function(sender, item, index)
	{
		mainMenu.Visible = true;
		ZombieMenu.Visible = false; 
		resetMainMenu();
	});		
}

function openMainMenu() 
{
	MenuOpen = true;
	CamOn();
	resetMainMenu();
    mainMenu.CurrentSelection = 0;
	mainMenu.Visible = true; 
}

function resetMainMenu()
{
	API.setPlayerClothes(API.getLocalPlayer(), 1, 0, 0);
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
	PaperBagMenu.CurrentSelection = 0;
}

// --------------------
function openSkiMenu() 
{
	mainMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetSkiMenu();
	SkiMenu.Visible = true; 
}

function resetSkiMenu()
{
	SkiMenu.Clear();
	for (var i = 0; i < SkiMenuItems.length; i++) 
	{
		var newitem = API.createMenuItem(SkiMenuItems[i][0], "");
		var vCost = SkiMenuItems[i][1];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
        SkiMenu.AddItem(newitem);
	}
	SkiMenu.CurrentSelection = 0;
}

// --------------------
function openSportMenu() 
{
	mainMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetSportMenu();
	SportMenu.Visible = true; 
}

function resetSportMenu()
{
	SportMenu.Clear();
	for (var i = 0; i < SportMenuItems.length; i++) 
	{
		var newitem = API.createMenuItem(SportMenuItems[i][0], "");
		var vCost = SportMenuItems[i][1];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
        SportMenu.AddItem(newitem);
	}
	SportMenu.CurrentSelection = 0;
}

// --------------------
function openTacticalMenu() 
{
	mainMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetTacticalMenu();
	TacticalMenu.Visible = true; 
}

function resetTacticalMenu()
{
	TacticalMenu.Clear();
	for (var i = 0; i < TacticalMenuItems.length; i++) 
	{
		var newitem = API.createMenuItem(TacticalMenuItems[i][0], "");
		var vCost = TacticalMenuItems[i][1];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
        TacticalMenu.AddItem(newitem);
	}
	TacticalMenu.CurrentSelection = 0;
}

// --------------------
function openValentinesMenu() 
{
	mainMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetValentinesMenu();
	ValentinesMenu.Visible = true; 
}

function resetValentinesMenu()
{
	ValentinesMenu.Clear();
	for (var i = 0; i < ValentinesMenuItems.length; i++) 
	{
		var newitem = API.createMenuItem(ValentinesMenuItems[i][0], "");
		var vCost = ValentinesMenuItems[i][1];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
        ValentinesMenu.AddItem(newitem);
	}
	ValentinesMenu.CurrentSelection = 0;
}

// --------------------
function openZombieMenu() 
{
	mainMenu.Visible = false;
	Pmoney = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
	resetZombieMenu();
	ZombieMenu.Visible = true; 
}

function resetZombieMenu()
{
	ZombieMenu.Clear();
	for (var i = 0; i < ZombieMenuItems.length; i++) 
	{
		var newitem = API.createMenuItem(ZombieMenuItems[i][0], "");
		var vCost = ZombieMenuItems[i][1];
			
		if ( vCost <= Pmoney){
			newitem.SetRightLabel("$" + vCost + "");
		}else {
		    newitem.SetRightLabel("~r~$" + vCost + "");
		}
        ZombieMenu.AddItem(newitem);
	}
	ZombieMenu.CurrentSelection = 0;
}

function CamOn() 
{
	//set the camera
	MaskCamera = API.createCamera(CameraPos, new Vector3());
	API.pointCameraAtPosition(MaskCamera, API.getEntityPosition(API.getLocalPlayer()).Add(new Vector3(0, 0, .75)));
	API.setActiveCamera(MaskCamera);
	API.setHudVisible(false);	
	API.setChatVisible(false);
	resource.PointHelper.togglePointHelpers();
}

function CloseMenu() 
{
	API.setHudVisible(true);
	API.setChatVisible(true);
	resource.PointHelper.togglePointHelpers();
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
	SkiMenu.Visible = false;
	SportMenu.Visible = false;
	TacticalMenu.Visible = false;
	ValentinesMenu.Visible = false;
	ZombieMenu.Visible = false;
	API.setActiveCamera(null);
	MenuOpen = false;
}
