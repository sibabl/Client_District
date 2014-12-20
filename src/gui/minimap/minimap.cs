//function a()
//{
//	exec("./Minimap.cs");

//	District_Minimap();

//	schedule(800,0,a);
//}

function District_Minimap()
{
	if(isObject("Minimap"))
	Minimap.delete();

	%Minimap = new GuiSwatchCtrl(District_Minimap)
	{
		profile = "GuiDefaultProfile";
		horizSizing = "left";
		vertSizing = "top";
		position = "480 320";
		position = getWord(getRes(), 0)-160 SPC getWord(getRes(), 1)-160;
		extent = "128 128";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		color = "20 20 20 120";
	};
	PlayGui.add(%Minimap);
}