exec("./gui/init.cs");
exec("./auth.cs");

package District_Client
{
	function PlayGui::createInvHUD(%this)
	{
		Parent::createInvHUD(%this);

		HUD_BrickNameBG.visible = 0;
		HUD_BrickBox.visible = 0;
	}

	function PlayGui::createToolHUD(%this)
	{
		Parent::createToolHUD(%this);

		HUD_ToolNameBG.visible = 0;
		HUD_ToolBox.visible = 0;
	}

	function GameConnection::initialControlSet(%this)
	{
		Parent::initialControlSet(%this);
	}

	function scrollInventory(%i)
	{
		Parent::scrollInventory(%i);
	}
};