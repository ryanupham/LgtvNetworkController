namespace LgtvNetworkController.Enums;

public class Key
{
    public readonly string Value;

    public static readonly Key App = new("app");  //
    public static readonly Key ArrowDown = new("arrowdown");
    public static readonly Key ArrowLeft = new("arrowleft");
    public static readonly Key ArrowRight = new("arrowright");
    public static readonly Key ArrowUp = new("arrowup");
    public static readonly Key AspectRatio = new("aspectratio");
    public static readonly Key AudioDescription = new("audiodescription");
    public static readonly Key AudioMode = new("audiomode");
    public static readonly Key AutoConfig = new("autoconfig");
    public static readonly Key AVMode = new("avmode");
    public static readonly Key Back = new("returnback");
    public static readonly Key BlueButton = new("bluebutton");
    public static readonly Key CaptionSubtitle = new("captionsubtitle");
    public static readonly Key ChannelDown = new("channeldown");
    public static readonly Key ChannelList = new("channellist");
    public static readonly Key ChannelUp = new("channelup");
    public static readonly Key DeviceInput = new("deviceinput");
    public static readonly Key EnergySaving = new("screenbright"); //
    public static readonly Key Exit = new("exit");
    public static readonly Key FastForward = new("fastforward");
    public static readonly Key FavoriteChannel = new("favoritechannel");
    public static readonly Key GreenButton = new("greenbutton");
    public static readonly Key Home = new("myapp");
    public static readonly Key LiveTV = new("livetv");
    public static readonly Key Menu = new("settingmenu");  ///////
    public static readonly Key Number0 = new("number0");
    public static readonly Key Number1 = new("number1");
    public static readonly Key Number2 = new("number2");
    public static readonly Key Number3 = new("number3");
    public static readonly Key Number4 = new("number4");
    public static readonly Key Number5 = new("number5");
    public static readonly Key Number6 = new("number6");
    public static readonly Key Number7 = new("number7");
    public static readonly Key Number8 = new("number8");
    public static readonly Key Number9 = new("number9");
    public static readonly Key OK = new("ok");
    public static readonly Key Play = new("play");
    public static readonly Key PowerButton = new("powerbutton");
    public static readonly Key PreviousChannel = new("previouschannel");
    public static readonly Key ProgramGuide = new("programguide");
    public static readonly Key ProgramInfo = new("programminfo");
    public static readonly Key ProgramOrder = new("programmorder");
    public static readonly Key QuickMenu = new("quickmenu");
    public static readonly Key Record = new("record");
    public static readonly Key RedButton = new("redbutton");
    public static readonly Key Rewind = new("rewind");
    public static readonly Key Settings = new("settingmenu");
    public static readonly Key Simplink = new("simplelink");
    public static readonly Key SleepTimer = new("sleepreserve");
    public static readonly Key SlowPlay = new("slowplay");
    public static readonly Key SmartHome = new("smarthome");
    public static readonly Key SoccerScreen = new("soccerscreen");
    public static readonly Key TeleText = new("teletext");
    public static readonly Key TeleTextOption = new("teletextoption");
    public static readonly Key ThreeD = new("3d");
    public static readonly Key UserGuide = new("userguide");
    public static readonly Key VideoMode = new("videomode");
    public static readonly Key VolumeDown = new("volumedown");
    public static readonly Key VolumeMute = new("volumemute");
    public static readonly Key VolumeUp = new("volumeup");
    public static readonly Key YellowButton = new("yellowbutton");

    // input, pause

    private Key(string value) => Value = value;

    public override string ToString() => Value;
}
