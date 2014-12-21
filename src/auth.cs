$District::Version = 0;

function District_HandshakePing(%value)
{
	commandToServer('District_HandshakePong', $District::Version);
	$District::ServerVersion = %value;
	$District::Connected = 1;
}

package District_Client_Auth
{
	function disconnect()
	{
		Parent::disconnect();
		$District::Connected = 0;
	}
};
activatePackage("District_Client_Auth");