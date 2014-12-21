function Container(%Id, %name, %x, %y, %array)
{
	if(%x <= 0 || %y <= 0)
	{
		error("Container: Invalid container size. [" @ %x @ "_" @ %y @ "]");
		return;
	}

	if(!isObject(Container))
	{
		new GuiBitmapButtonCtrl(Container)
		{
			profile = "BlankButtonProfile";
			horizSizing = "width";
			vertSizing = "height";
			position = "0 0";
			extent = getRes();
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			command = "Container.dropItem();";
			text = " ";
			groupNum = "-1";
			buttonType = "PushButton";
			bitmap = "Add-Ons/Client_CityMod/res/ui/blankButton/blank";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			mKeepCached = "0";
			mColor = "255 255 255 255";
		};
	}

	if(isObject(%Container = "Container_" @ %Id))
	{
		Container.chainDeleteItems(%Container);
		%Container.delete();
	}

	%lineCount = mFloor(((63 * %x) + 8) / 8) - 1;
	for(%i = 0; %i < %lineCount; %i++)
	%line = %line @ "_";

	new GuiSwatchCtrl(%Container) //BODY
	{
		profile = "GuiDefaultProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0 0";
		extent = (63 * %x) + 8 SPC (63 * %y) + 32;
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		color = "20 20 20 120";

		ContainerName = %name;
		ContainerId = %Id;
		SizeX = %x;
		SizeY = %y;

		new GuiMLTextCtrl() //TEXT
		{
			profile = "GuiMLTextProfile";
			horizSizing = "center";
			vertSizing = "bottom";
			position = "0 2";
			extent = (63 * %x) + 8 SPC 18;
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = "<just:center><shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:18>" @ %name;
			maxBitmapHeight = "-1";
			selectable = "0";
			autoResize = "1";
		};

		new GuiMLTextCtrl() //LINE
		{
			profile = "GuiMLTextProfile";
			horizSizing = "center";
			vertSizing = "bottom";
			position = "0 8";
			extent = (63 * %x) + 8 SPC 18;
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = "<just:center><shadow:2:2><shadowcolor:00000066><color:DDDDDD><font:Impact:18>" @ %line;
			maxBitmapHeight = "-1";
			selectable = "0";
			autoResize = "1";
		};
	};

	for(%yy=0;%yy<%y;%yy++)
	for(%xx=0;%xx<%x;%xx++)
	Container.addSlot(%Container, %xx, %yy);

	if(%name $= "Inventory")
	{
		if(%x >= 2 && %y >= 2)
		{
			%pos = %Container.position;
			%ext = %Container.extent;
			%Container.resize(getWord(%pos, 0), getWord(%pos, 1), getWord(%ext, 0), getWord(%ext, 1)+7);

			for(%i=0;%i<%x;%i++)
			{
				if(!isObject(%slot = %Container.slot[%i @ "_" @ %y-1]))
				{
					error("Container - HOTBAR: Slot not found. [" @ %i @ "_" @ %y-1 @ "]");
					return;
				}

				%slot.position = getWord(%slot.position, 0) SPC getWord(%slot.position, 1)+7;
				%slot.isHotbar = 1;
			}

			%obj = new GuiMLTextCtrl()
			{
				profile = "GuiMLTextProfile";
				horizSizing = "center";
				vertSizing = "bottom";
				position = 0 SPC -48 + (%y * 63);
				extent = (63 * %x) + 8 SPC 18;
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				lineSpacing = "2";
				allowColorChars = "0";
				maxChars = "-1";
				text = "<just:center><shadow:2:2><shadowcolor:00000066><color:DDDDDD><font:Impact:18>" @ %line;
				maxBitmapHeight = "-1";
				selectable = "0";
				autoResize = "1";
			};
			%Container.add(%obj);
		}
		else
		echo("\c2ERROR Container - HOTBAR: Container size is too small!");
	}
	else
	{
		%close = new GuiSwatchCtrl()
		{
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = getWord(%Container.extent, 0) - 22 SPC 4;
			extent = "18 18";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "200 0 0 120";

			new GuiBitmapButtonCtrl()
			{
				profile = "BlankButtonProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 0";
				extent = "18 18";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				command = "Container.removeContainer(" @ %Id @ ");";
				text = "X";
				groupNum = "-1";
				buttonType = "PushButton";
				bitmap = "Add-Ons/Client_CityMod/res/ui/blankButton/blank";
				lockAspectRatio = "0";
				alignLeft = "0";
				alignTop = "0";
				overflowImage = "0";
				mKeepCached = "0";
				mColor = "255 255 255 255";
			};
		};
		%Container.add(%close);
	}
	Container.add(%Container);
}

function Container::addSlot(%this, %Container, %x, %y)
{
	if(!isObject(%Container))
	{
		echo("\c2ERROR Container.addSlot: Container not found.");
		return;
	}

	%slot = new GuiSwatchCtrl() //SLOT
	{
		profile = "GuiDefaultProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = 8 + (%x * 63) SPC 32 + (%y * 63);
		extent = "55 55";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		color = "20 20 20 120";

		new GuiBitmapCtrl() //BORDER
		{
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "55 55";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			wrap = "0";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			keepCached = "0";
			mColor = "255 255 255 255";
			mMultiply = "0";

			new GuiBitmapCtrl() //ICON
			{
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 0";
				extent = "55 55";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				wrap = "0";
				lockAspectRatio = "0";
				alignLeft = "0";
				alignTop = "0";
				overflowImage = "0";
				keepCached = "0";
				mColor = "255 255 255 255";
				mMultiply = "0";

				new GuiSwatchCtrl() //LAYER
				{
					profile = "GuiDefaultProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "0 36";
					extent = "55 19";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					color = "20 20 20 120";
				};

				new GuiBitmapButtonCtrl() //TEXT
				{
					profile = "BlankButtonProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "0 0";
					extent = "55 91";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					command = "Container.clickButton(" @ %Container @ ", " @ %x @ ", " @ %y @ ");";
					text = " ";
					groupNum = "-1";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_CityMod/res/ui/blankButton/blank";
					lockAspectRatio = "0";
					alignLeft = "0";
					alignTop = "0";
					overflowImage = "0";
					mKeepCached = "0";
					mColor = "255 255 255 255";
				};
			};
		};
	};
	%Container.add(%slot);
	%Container.slot[%x @ "_" @ %y] = %slot;
}

function Container::clickButton(%this, %Container, %x, %y)
{
	if(!isObject(%Container))
	{
		echo("\c2ERROR Container.clickButton: Container not found.");
		return;
	}

	if(!isObject(%slot = %Container.slot[%x @ "_" @ %y]))
	{
		echo("\c2ERROR Container.clickButton: Slot not found. [" @ %x @ "_" @ %y @ "]");
		return;
	}

	%slotItem = %slot.Item;
	%currItem = ContainerItems.currItem;

	if(isObject(%currItem))
	{
		if(isObject(%slotItem))
		{
			Container.setSlot(%Container, %currItem, %x, %y);
			Container.setItem(%Container, %slotItem, getWord(ContainerItems.lastSlot, 0), getWord(ContainerItems.lastSlot, 1));
		}
		else
		{
			Container.setSlot(%Container, %currItem, %x, %y);
			Container.setItem("");
		}
	}
	else
	{
		if(isObject(%slotItem))
		{
			Container.setSlot(%Container, "", %x, %y);
			Container.setItem(%Container, %slotItem, %x, %y);
		}
	}
}

function Container::SwitchCursor(%this, %Img)
{
	DefaultCursor.delete();

	%cursor = new GuiCursor(DefaultCursor)
	{
		bitmapName = %Img;
		hotSpot = "27 27";
	};

	if(%Img $= "Default")
	{
		DefaultCursor.bitmapName = "base/client/ui/CUR_3darrow";
		DefaultCursor.hotSpot = "1 1";
	}

	canvas.setCursor(DefaultCursor);
}

function Container::setSlot(%this, %Container, %item, %x, %y)
{
	if(!isObject(ContainerItems))
	{
		echo("\c2ERROR Container.setSlot: ContainerItems not found.");
		return;
	}

	if(!isObject(%Container))
	{
		echo("\c2ERROR Container.setSlot: Container not found.");
		return;
	}

	if(!isObject(%slot = %Container.slot[%x @ "_" @ %y]))
	{
		echo("\c2ERROR Container.setSlot: Slot not found. [" @ %x @ "_" @ %y @ "]");
		return;
	}

	%border = %slot.getObject(0);
	%icon = %border.getObject(0);
	%layer = %icon.getObject(0);
	%text = %icon.getObject(1);

	if(isObject(%item) || %item $= "")
	{
		%slot.Item = %item;

		%icon.setBitmap(%item $= "" ? "" : %item.iconName);
		%text.setText(%item $= "" ? "" : %item.uiName);

		if(%slot.isHotbar)
		{
			if(isObject(Hotbar))
			{
				if(!isObject(%slot = Hotbar.slot[%x]))
				{
					echo("\c2ERROR Container.setSlot: Hotbar slot not found. [" @ %x @ "]");
					return;
				}

				%border = %slot.getObject(0);
				%icon = %border.getObject(0);
				%layer = %icon.getObject(0);
				%text = %icon.getObject(1);

				%slot.Item = %item;

				%icon.setBitmap(%item $= "" ? "" : %item.iconName);
				%text.setText(%item $= "" ? "" : %item.uiName);

				%Count = Hotbar.currSlot;

				if(%Count $= %x)
				Hotbar.switchSlot(%Count);
			}
		}

		return;
	}
	echo("\c2ERROR Container.setSlot: Invalid item input");
}

function Container::setItem(%this, %Container, %item, %x, %y)
{
	if(!isObject(ContainerItems))
	{
		echo("\c2ERROR Container.setItem: ContainerItems not found.");
		return;
	}

	if(isObject(%Container))
	{
		if(!isObject(%Container.slot[%x @ "_" @ %y]))
		{
			echo("\c2ERROR Container.setItem: slot not found. [" @ %x @ "_" @ %y @ "]");
			return;
		}

		ContainerItems.lastSlot = %x SPC %y;
		ContainerItems.lastContainer = %Container.ContainerId;
	}

	if(isObject(%item) || %item $= "")
	{
		ContainerItems.currItem = (%item $= "" ? "" : %item);
		Container.SwitchCursor(%item $= "" ? "Default" : %item.iconName);

		return;
	}
	echo("\c2ERROR Container.setItem: Invalid item input");
}

function Container::dropItem(%this)
{
	if(!isObject(ContainerItems))
	{
		echo("\c2ERROR Container.dropItem: ContainerItems not found.");
		return;
	}

	if(isObject(ContainerItems.currItem))
	{
		%Id = ContainerItems.lastContainer;
		%x = getWord(ContainerItems.lastSlot, 0);
		%y = getWord(ContainerItems.lastSlot, 1);

		commandToServer('Inventory_dropItem', %Id, %x, %y);

		ContainerItems.currItem.delete();
		Container.setItem("");
	}
}

function Container::organizeContainers(%this)
{
	//Resizing controls while they're asleep can result in crashes.
	//if(!%this.isAwake())
	//return;

	%topMargin = 10;
	%leftMargin = 10;
	%rightMargin = 10;
	%bottomMargin = 10;
	%screenSize = getRes();
	%count = %this.getCount();

	%sumWidth = 0;

	for(%i = 0; %i < %count; %i++)
	{
		%obj = %this.getObject(%i);
		%sumWidth += getWord(%obj.getExtent(), 0) + %leftMargin + %rightMargin;
	}

	//Check if the controls can fit into the screen without resorting to expensive space-filling
	if(%sumWidth <= getWord(%screenSize, 0))
	{
		//Larger controls are probably more important, and should thus be towards the screen's center
		for(%i = 0; %i < %count; %i++)
		{
			%ext = %this.getObject(%i).getExtent();
			%area[%i] = getWord(%ext, 0) * getWord(%ext, 1);
		}

		for(%i = 0; %i < %count; %i++)
		{
			%max = -999999;
			%right = !%right;

			for(%j = 0; %j < %count; %j++)
			{
				if(%sorted[%j])
				continue;

				if(%area[%j] > %max)
				{
					%max = %area[%j];
					%sortI = %j;
				}
			}

			%obj = %this.getObject(%sortI);
			%sorted[%sortI] = true;

			if(%right)
			{
				%ext = %obj.getExtent();
				//%obj.resize(%highX + %leftMargin, -mFloor(getWord(%ext, 1) / 2),
				//getWord(%ext, 0), getWord(%ext, 1));
				%obj.position = %highX + %leftMargin SPC -mFloor(getWord(%ext, 1) / 2);
				%obj.extent = getWord(%ext, 0) SPC getWord(%ext, 1);


				%posX = getWord(%obj.getPosition(), 0);
				%highX = %posX + getWord(%ext, 0) + %rightMargin;
			}
			else
			{
				%ext = %obj.getExtent();
				//%obj.resize(%lowX - (%rightMargin + getWord(%ext, 0)), -mFloor(getWord(%ext, 1) / 2),
				//getWord(%ext, 0), getWord(%ext, 1));
				%obj.position = %lowX - (%rightMargin + getWord(%ext, 0)) SPC -mFloor(getWord(%ext, 1) / 2);
				%obj.extent = getWord(%ext, 0) SPC getWord(%ext, 1);
				%posX = getWord(%obj.getPosition(), 0);
				%lowX = %posX - %leftMargin;
			}
		}
	}
	%this.centerObjects();
}

function Container::centerObjects(%this)
{
	//Resizing controls while they're asleep can result in crashes.
	//if(!%this.isAwake())
	//return;

	%lowX = 999999;
	%lowY = 999999;
	%highX = -999999;
	%highY = -999999;
	%screenSize = getRes();
	%count = %this.getCount();

	for(%i = 0; %i < %count; %i++)
	{
		%obj = %this.getObject(%i);
		%pos = %obj.getPosition();
		%ext = %obj.getExtent();

		%posX = getWord(%pos, 0);
		%posY = getWord(%pos, 1);
		%lowX = %posX < %lowX ? %posX : %lowX; //Inline version of getMin
		%lowY = %posY < %lowY ? %posY : %lowY;

		%extX = getWord(%ext, 0);
		%extY = getWord(%ext, 1);
		%highX = (%posX + %extX) > %highX ? (%posX + %extX) : %highX; //Inline version of getMax
		%highY = (%posY + %extY) > %highY ? (%posY + %extY) : %highY;
	}

	%centX = (%lowX + %highX) / 2;
	%centY = (%lowY + %highY) / 2;
	%diffX = mFloatLength(getWord(%screenSize, 0) / 2 - %centX, 0);
	%diffY = mFloatLength(getWord(%screenSize, 1) / 2 - %centY, 0);

	for(%i = 0; %i < %count; %i++)
	{
		%obj = %this.getObject(%i);
		%pos = %obj.getPosition();
		%ext = %obj.getExtent();

		//Resize the object
		%obj.position = getWord(%pos, 0) + %diffX SPC getWord(%pos, 1) + %diffY;
		%obj.extent = getWord(%ext, 0) SPC getWord(%ext, 1);
		//%obj.resize(getWord(%pos, 0) + %diffX, getWord(%pos, 1) + %diffY,
		//getWord(%ext, 0), getWord(%ext, 1));
	}
}

function Container::chainDeleteItems(%this, %Container)
{
	if(!isObject(ContainerItems))
	{
		echo("\c2ERROR Container.chainDeleteItems: ContainerItems not found.");
		return;
	}

	for(%x=0;%x<%Container.SizeX;%x++)
	{
		for(%y=0;%y<%Container.SizeY;%y++)
		{
			if(!isObject(%slot = %Container.slot[%x @ "_" @ %y]))
			{
				echo("\c2ERROR Container.chainDeleteItems: slot not found. [" @ %x @ "_" @ %y @ "]");
				return;
			}

			if(isObject(%slot.Item))
			{
				%slot.Item.delete();
				Container.setSlot(%Container, "", %x, %y);
			}
		}
	}
}

function Container::removeContainer(%this, %Id)
{
	if(!isObject(%Container = ("Container_" @ %Id)))
	{
		echo("\c2ERROR Container.closeContainer: Container not found.");
		return;
	}
	%Container.delete();

	if(Container.getCount() == 0)
	{
		Container.delete();
		return;
	}

	Container.organizeContainers();
}

function clientCmdInventory_setSlot(%Id, %x, %y, %data)
{
	if(!isObject(%map = stringToMap(%data)))
	{
		echo("\c2ERROR: clientcmdInventory_setSlot: Map object failed to create.");
		return;
	}

	if((%name = %map.get("Name")) $= "")
	{
		echo("\c2ERROR: clientcmdInventory_setSlot: Map object has no name field.");
		%map.delete();

		return;
	}

	if(!isObject(%data = "Inv" @ %name @ "Data"))
	{
		warn("ERROR: clientcmdInventory_setSlot: Inventory data \"" @ %data @ "\" not found.");
		ContainerItems.createData(%name);
	}

	%item = ContainerItems.createItem(%data);
	for(%i = 0; %i < %map.keys().length; %i++)
	{
		%key = %map.keys().value[%i];
		%value = %map.values().value[%i];

		if(%key !$= "Name")
		eval(%item @ "." @ %key @ "=" @ %value @ ";");
	}

	if(isObject(%Container = ("Container_" @ %Id)))
	Container.setSlot(%Container, %item, %x, %y);

	%map.delete();
}