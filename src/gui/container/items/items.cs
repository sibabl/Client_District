if(!isObject("ContainerItems"))
new ScriptGroup(ContainerItems){};

function ContainerItems::createData(%this, %name, %image)
{
	if(isObject(%data = ("Inv" @ %name @ "Data")))
	return;

	%path = "Add-Ons/Client_CityMod/res/gui/Items/";
	%image = %path @ (%image $= "" ? %name : %image);

	%item = new ScriptObject(%data)
	{
		uiName = %name;
		iconName = (isFile(%image @ ".png") ? %image : %path @ "none");
	};
	%this.add(%item);

	return %item;
}

function ContainerItems::createItem(%this, %data, %array)
{
	if(!isObject(%data))
	return echo("\c2ERROR: ContainerItems datablock not found" SPC %data);

	%item = new ScriptObject()
	{
		uiName = %data.uiName;
		iconName = %data.iconName;
	};
	%this.add(%item);

	for(%i=0;%i<getFieldCount(%array);%i+=2)
	{
		%type = getField(%array, %i);
		%val = getField(%array, %i+1);

		eval("%item." @ %type @ "=%val;");
	}

	return %item;
}

ContainerItems.createData("Apple");
ContainerItems.createData("Bacon");
ContainerItems.createData("Baguette");
ContainerItems.createData("Banana");
ContainerItems.createData("Bananas", "BananaBundle");
ContainerItems.createData("Pepper", "Bellpepper");
ContainerItems.createData("Berries");
ContainerItems.createData("Bread");
ContainerItems.createData("Carrot");
ContainerItems.createData("Carrots", "CarrotBundle");
ContainerItems.createData("Corn");
ContainerItems.createData("Drumbell");
ContainerItems.createData("Lemon");
ContainerItems.createData("Onion");
ContainerItems.createData("Orange");
ContainerItems.createData("Pear");
ContainerItems.createData("Potatos", "PotatoBundle");
ContainerItems.createData("Ribs");
ContainerItems.createData("Steak");
ContainerItems.createData("Tomato");

ContainerItems.createData("TestHat");