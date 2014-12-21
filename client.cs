$District::Version = 0;

function District_HandshakePing(%value)
{
	commandToServer('District_HandshakePong', $District::Version);
	$District::ServerVersion = %value;
	$District::Connected = 1;
}

package DistrictClient
{
	function disconnect()
	{
		Parent::disconnect();
		$District::Connected = 0;
	}
};
activatePackage("DistrictClient");

exec("./lib/init.cs");
exec("./src/init.cs");