<img align="left" width="116" height="116" src="https://raw.githubusercontent.com/marlonajgayle/Net5WebTemplate/develop/src/Content/.template.config/icon.png" />

# DayZ CF-Tools Cloud C# SDK


This SDK gives you the ability to post or query data from the CFTools Cloud API.
You can run anything available in the webapi through this SDK. Currently supported is:
- [x] Get Leaderboard
- [x] Get Server Infos
- [x] Get Playerstats
- [x] Get full playerlist
- [x] Get whitelist entrys
- [x] Kick player
- [x] Send private message
- [x] Send server message
- [x] Send raw rcon command
- [x] Add queue priority
- [x] Add whitelist entry
- [x] Delete queue priority
- [x] Delete whitelist entry
- [x] Search player by identifier


## Getting Started
Use the instructions provided below to get the project up and running.

### Prerequisites
You will need the following tools:
* [Visual Studio Code or Visual Studio 2019](https://visualstudio.microsoft.com/vs/) (version 16.8 or later)
* [.NET Core SDK 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)

### Instructions
1. Install the latest [.NET Core 5 SDK](https://dotnet.microsoft.com/download). 
2. More Soon.

### DayZ CF-TOOLS SDK
To use this SDK follow the setup.
1. Add 2 strings to your app.config
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<appSettings>
		<add key="Application_id" value=""/>
		<add key="secret" value=""/>
	</appSettings>
</configuration>
```
2. Install this project via Package Manager.
3. Read the documentation.


## Technologies and Third Party Libraries
* .NET Core 3.1
* Swashbuckle

## Contributions
- [Krypton91](https://github.com/Krypton91) - Owner & Coder.

## Versions
The [master](https://github.com/Krypton91/CFToolsSDK/master) branch is running .NET Core 3.1

# Documentation

## Get Data
### Fetch Leaderboard

|       Param        |                                             Description                                              |
| -------------------- | ---------------------------------------------------------------------------------------------------- |
| `string server_api_id`           | can be found in cftools. |
| `enum LEADERBOARD_STAT` | kills,deaths,suicides,playtime,longest_kill,longest_shot,kdratio  is supported |
| `enum LEADERBOARD_ORDER` | ASCEDING and DESCENDING  is supported |
| `int limit` | 1-100  is supported |
```csharp
List<Leaderboard> board = await webManager.GetLeaderborad(string server_api_id, LEADERBOARD_STAT stat, LEADERBOARD_ORDER order, int limit);
if(board != null)
{
	Logger.GetInstance().Debug("Successfully recived Leaderboard!");
	foreach (var entry in board)
	{
		Logger.GetInstance().Debug($"{entry.rank}. {entry.latest_name} has {entry.kills} with a KD {entry.kdratio}");
	}
}
```

### Fetch Playerstats

|       Param        |                                             Description                                              |
| -------------------- | ---------------------------------------------------------------------------------------------------- |
| `string server_api_id`           | can be found in cftools. |
| `string cftools_id` | cftools_id of the player. |
```csharp
Session stats = await webManager.GetPlayerStats(string server_api_id, string cftools_id);
```

### Fetch Server Infos

|       Param        |                                             Description                                              |
| -------------------- | ---------------------------------------------------------------------------------------------------- |
| `string ServerIP`           | the ip address of the gameserver where you want to fetch the infos. |
| `string Gameport` | the Gameport of the gameserver where you want to fetch the infos. |
```csharp
GameServer server = await webManager.GetGameServer(CFHelper.GenerateServerId(string ServerIP, string Gameport));
```

### Search cftools_id by identifier

|       Param        |                                             Description                                              |
| -------------------- | ---------------------------------------------------------------------------------------------------- |
| `string identifier`           |  Either a Steam64, BattlEye GUID or Bohemia Interactive UID |
```csharp
string cfid = await webManager.PlayerLookUp(string identifier);
```

### Fetch full playerlist

|       Param        |                                             Description                                              |
| -------------------- | ---------------------------------------------------------------------------------------------------- |
| `string server_api_id`           | the server_api_id can be found in app.cftools.cloud . |
```csharp
FullPlayerList playerlist = await webManager.GetFullPlayerList(string server_api_id);
if(playerlist != null)
{
	Logger.GetInstance().Debug("Successfully recived Leaderboard!");
        foreach (var entry in playerlist.sessions)
        {
		Logger.GetInstance().Debug($"{entry.cftools_id} with name: {entry.gamedata.player_name}");
        }
}
```

### Get whitelist

|       Param        |                                             Description                                              |
| -------------------- | ---------------------------------------------------------------------------------------------------- |
| `string server_api_id`           | the server_api_id can be found in app.cftools.cloud . |
| `string cftools_id`       	| cftools_id of the reciver. (optional when empty or null full will be requested.)|
| `string comment`       	| comment. (optional when empty or null full will be requested.)|
```csharp
 WhiteListResponse whitelist = await webManager.GetWhitelist(string server_api_id, string cftools_id = "", string comment = "");
```
## Post Data
### Kick player

|       Param        |                                             Description                                              |
| -------------------- | ---------------------------------------------------------------------------------------------------- |
| `string server_api_id`           | the server_api_id can be found in app.cftools.cloud . |
| `string player_session_id`       | the session id of the player what gets kicked. |
| `string reason`       	| any reason. |
```csharp
bool wasKickSuccessfully = await webManager.KickPlayer(string server_api_id, string player_session_id, string reason);
```

### Send private message
|       Param        |                                             Description                                              |
| -------------------- | ---------------------------------------------------------------------------------------------------- |
| `string server_api_id`           | the server_api_id can be found in app.cftools.cloud . |
| `string player_session_id`       | the session id of the player what recives the message. |
| `string content`       	| any message. |
```csharp
bool sendet = await webManager.SendPrivateMessage(string server_api_id, string player_session_id, string content);
```

### Send server message
|       Param        |                                             Description                                              |
| -------------------- | ---------------------------------------------------------------------------------------------------- |
| `string server_api_id`           | the server_api_id can be found in app.cftools.cloud . |
| `string content`       	| any message. |
```csharp
bool sendet = await webManager.ServerMessage(string server_api_id, string content);
```

### Send raw rcon command
|       Param        |                                             Description                                              |
| -------------------- | ---------------------------------------------------------------------------------------------------- |
| `string server_api_id`           | the server_api_id can be found in app.cftools.cloud . |
| `string command`       	| any valid rcon command.|
```csharp
bool sendet = await webManager.ServerMessage(string server_api_id, string content);
```


### Add queue priority
|       Param        |                                             Description                                              |
| -------------------- | ---------------------------------------------------------------------------------------------------- |
| `string server_api_id`           | the server_api_id can be found in app.cftools.cloud . |
| `string player_cfid`       	| cftools_id of the reciver.|
| `string comment`       	| any comment.|
| `string expires_at`       	| Isco8601 timestamp, you can use the convertor in CFHelper to convert any DateTime object to ISO8601.|
```csharp
DateTime expires = DateTime.UtcNow;
expires = expires.AddDays(2);//In our case we give him 2 Days.
bool addedWithsuccess = await webManager.AddQueuePriority(string server_api_id, string player_cfid, string comment, CFHelper.ConvertDateTimeToIso8601Time(expires));
```

### Add whitelist
|       Param        |                                             Description                                              |
| -------------------- | ---------------------------------------------------------------------------------------------------- |
| `string server_api_id`           | the server_api_id can be found in app.cftools.cloud . |
| `string player_cfid`       	| cftools_id of the reciver.|
| `string comment`       	| any comment.|
| `string expires_at`       	| Isco8601 timestamp, you can use the convertor in CFHelper to convert any DateTime object to ISO8601.|
```csharp
DateTime expires = DateTime.UtcNow;
expires = expires.AddDays(2);//In our case we give him 2 Days.
bool addedWithsuccess = await webManager.AddWhiteListEntry(string server_api_id, string player_cfid, string comment, CFHelper.ConvertDateTimeToIso8601Time(expires));
```

## Delete Data

### Delete queue priority
|       Param        |                                             Description                                              |
| -------------------- | ---------------------------------------------------------------------------------------------------- |
| `string server_api_id`           | the server_api_id can be found in app.cftools.cloud . |
| `string player_cfid`       	| cftools_id of the player.|
```csharp
bool deleted = await webManager.DeleteQueuePriority(string server_api_id, string player_cfid);
````

## Delete Data

|       Param        |                                             Description                                              |
| -------------------- | ---------------------------------------------------------------------------------------------------- |
| `string server_api_id`           | the server_api_id can be found in app.cftools.cloud . |
| `string cftools_id`       	| cftools_id of the player.|
```csharp
bool deleted = await webManager.DeleteWhiteListEntry(string server_api_id, string player_cfid);
````
Comming soon
````
