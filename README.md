<img align="left" width="116" height="116" src="https://raw.githubusercontent.com/marlonajgayle/Net5WebTemplate/develop/src/Content/.template.config/icon.png" />

# DayZ CF-Tools Cloud C# SDK


This SDK gives you the ability to post or query data from the CFTools Cloud API.
You can run anything available in the webapi through this SDK. Currently supported is:
- [x] #Fetch Leaderboard
- [ ] #Fetch Playerdata
- [ ] #Fetch Server data


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

### Documentation

# Fetch Leaderboard

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
#POST-Requests
````
Comming soon
````
