if(!isObject("ContainerItems"))
new ScriptGroup(ContainerItems){};

function ContainerItems::createData(%this, %category, %name, %image)
{
	if(isObject(%data = ("Inv" @ %name @ "Data")))
	%data.delete();

	%path = "./" @ %category @ "/" (%image $= "" ? %name : %image ) @ ".png";

	if(!isFile(%path))
	{
		error("ContainerItems.createData: invalid category path or item not found.");
		return;
	}

	%itemData = new ScriptObject(%data)
	{
		uiName = %name;
		image = %path;
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