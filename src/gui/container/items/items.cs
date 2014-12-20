if(!isObject("ContainerItems"))
new ScriptGroup(ContainerItems){};

function ContainerItems::createData(%this, %category, %name, %image)
{
	if(%category $= "")
	{
		error("ContainerItems.createData: no category entered!");
		return;
	}

	if(%name $= "")
	{
		error("ContainerItems.createData: no name entered!");
		return;
	}

	if(isObject(%data = ("Inv" @ %name @ "Data")))
	%data.delete();

 	%path = "Add-Ons/Client_District/src/gui/container/items/";
 	%icon = %path @ %category @ "/" @ (%image $= "" ? %name : %image);

	if(!isFile(%icon @ ".png"))
	{
		warn("ContainerItems.createData: invalid category path or item icon not found.");
		warn(" +- using default \"none\" item data for \"" @ %name @ "\"");

		%icon = %path @ "none";
	}

	%itemData = new ScriptObject(%data)
	{
		uiName = %name;
		iconName = %icon;
	};
	%this.add(%itemData);

	return %item;
}

ContainerItems.createData("Food", "Apple");
ContainerItems.createData("Food", "Bacon");
ContainerItems.createData("Food", "Baguette");
ContainerItems.createData("Food", "Banana");
ContainerItems.createData("Food", "Bananas", "BananaBundle");
ContainerItems.createData("Food", "Pepper", "Bellpepper");
ContainerItems.createData("Food", "Berries");
ContainerItems.createData("Food", "Bread");
ContainerItems.createData("Food", "Carrot");
ContainerItems.createData("Food", "Carrots", "CarrotBundle");
ContainerItems.createData("Food", "Corn");
ContainerItems.createData("Food", "Drumbell");
ContainerItems.createData("Food", "Lemon");
ContainerItems.createData("Food", "Onion");
ContainerItems.createData("Food", "Orange");
ContainerItems.createData("Food", "Pear");
ContainerItems.createData("Food", "Potatos", "PotatoBundle");
ContainerItems.createData("Food", "Ribs");
ContainerItems.createData("Food", "Steak");
ContainerItems.createData("Food", "Tomato");

ContainerItems.createData("Items", "$5");
ContainerItems.createData("Items", "$10");
ContainerItems.createData("Items", "$20");
ContainerItems.createData("Items", "$50");
ContainerItems.createData("Items", "$100");

ContainerItems.createData("Hats", "Greaser");
ContainerItems.createData("Hats", "House Wife");
ContainerItems.createData("Hats", "The Hatter");
ContainerItems.createData("Hats", "Wiser");

// function ContainerItems::createData(%this, %name, %image)
// {
// 	if(isObject(%data = ("Inv" @ %name @ "Data")))
// 	return;

// 	%path = "Add-Ons/Client_CityMod/res/gui/Items/";
// 	%image = %path @ (%image $= "" ? %name : %image);

// 	%item = new ScriptObject(%data)
// 	{
// 		uiName = %name;
// 		iconName = (isFile(%image @ ".png") ? %image : %path @ "none");
// 	};
// 	%this.add(%item);

// 	return %item;
// }

// function ContainerItems::createItem(%this, %data, %array)
// {
// 	if(!isObject(%data))
// 	return echo("\c2ERROR: ContainerItems datablock not found" SPC %data);

// 	%item = new ScriptObject()
// 	{
// 		uiName = %data.uiName;
// 		iconName = %data.iconName;
// 	};
// 	%this.add(%item);

// 	for(%i=0;%i<getFieldCount(%array);%i+=2)
// 	{
// 		%type = getField(%array, %i);
// 		%val = getField(%array, %i+1);

// 		eval("%item." @ %type @ "=%val;");
// 	}

// 	return %item;
// }