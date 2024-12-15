// Development
// #r "C:\Users\dalyisaac\Repos\workspacer\src\workspacer.Shared\bin\Debug\net5.0-windows\win10-x64\workspacer.Shared.dll"
// #r "C:\Users\dalyisaac\Repos\workspacer\src\workspacer.Bar\bin\Debug\net5.0-windows\win10-x64\workspacer.Bar.dll"
// #r "C:\Users\dalyisaac\Repos\workspacer\src\workspacer.Gap\bin\Debug\net5.0-windows\win10-x64\workspacer.Gap.dll"
// #r "C:\Users\dalyisaac\Repos\workspacer\src\workspacer.ActionMenu\bin\Debug\net5.0-windows\win10-x64\workspacer.ActionMenu.dll"
// #r "C:\Users\dalyisaac\Repos\workspacer\src\workspacer.FocusIndicator\bin\Debug\net5.0-windows\win10-x64\workspacer.FocusIndicator.dll"

// Production
#r "C:\Program Files\workspacer\workspacer.Shared.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.Bar\workspacer.Bar.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.Gap\workspacer.Gap.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.ActionMenu\workspacer.ActionMenu.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.FocusIndicator\workspacer.FocusIndicator.dll"

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using workspacer;
using workspacer.Bar;
using workspacer.Bar.Widgets;
using workspacer.Gap;
using workspacer.ActionMenu;
using workspacer.FocusIndicator;

Action<IConfigContext> doConfig = (context) =>
{   
    // Appearance
    var fontSize = 12;
    var barHeight = 21;
    var fontName = "JetBrainsMono NF";
    var background = new Color(0x43, 0x4B, 0x5D);
    
    // Gap
    var gap = barHeight - 13;
    var gapPlugin = context.AddGap(new GapPluginConfig() { InnerGap = gap, OuterGap = gap / 2, Delta = gap / 2 });

    // Bar
     context.AddBar(new BarPluginConfig()
    {
        FontSize = fontSize,
        BarHeight = barHeight,
        FontName = fontName,
        DefaultWidgetBackground = background,

        // Left Widgets
        LeftWidgets = () => new IBarWidget[]
        {
            new WorkspaceWidget(),
            new TextWidget("¦"),
            new TitleWidget()
            {
                IsShortTitle = true,
            }
        },

        // Right Widgets
        RightWidgets = () => new IBarWidget[]
        {
            new TextWidget("workspacer"),
            new TimeWidget(1000, "| HH:mm:ss ¦ dd-MM-yyyy |"),
            new ActiveLayoutWidget(),
        }
    });
    
    // Bar focus indicator
    // context.AddFocusIndicator();

    // Action menu
    var actionMenu = context.AddActionMenu();
    var actionMenuBuilder = actionMenu.DefaultMenu;

    // Action menu - Recycle Bin
    /*
    actionMenuBuilder.AddFreeForm("Recycle Bin", (o) =>z
    {
        System.Diagnostics.Process.Start("explorer.exe", "shell:recyclebinfolder");
    });`
    */

    // Workspaces
    context.WorkspaceContainer.CreateWorkspaces("Main", "Browsers", "School+Google", "Code+3D", "Terminal+Write", "VM", "Security+Network", "Sound", "Gaming+Chat");
    context.CanMinimizeWindows = true;
    
    // Default layouts
    Func<ILayoutEngine[]> defaultLayouts = () => new ILayoutEngine[]
    {
        new TallLayoutEngine(),
        new VertLayoutEngine(),
        new HorzLayoutEngine(),
        new FullLayoutEngine(),
    };
    context.DefaultLayouts = defaultLayouts;

    // Array of workspace names and their layouts
    (string, ILayoutEngine[])[] workspaces =
    {
        ("Main", defaultLayouts()),
        ("Browsers", defaultLayouts()),
        ("School+Google", defaultLayouts()),
        ("Code+3D", defaultLayouts()),
        ("Terminal+Write", defaultLayouts()),
        ("VM", defaultLayouts()),
        ("Security+Network", defaultLayouts()),
        ("Sound", defaultLayouts()),
        ("Gaming+Chat", defaultLayouts()),
    };

    // Routes
    context.WindowRouter.RouteProcessName("chrome", "School+Google");

    context.WindowRouter.RouteProcessName("vivaldi", "Browsers");
    context.WindowRouter.RouteProcessName("brave", "Browsers");
    context.WindowRouter.RouteProcessName("Tor", "Browsers");

    context.WindowRouter.RouteProcessName("Malwarebytes", "Security+Network");
    context.WindowRouter.RouteProcessName("Windows Security", "Security+Network");
    context.WindowRouter.RouteProcessName("Wireshark", "Security+Network");
    context.WindowRouter.RouteProcessName("NextDNS", "Security+Network");

    context.WindowRouter.RouteProcessName("VSCodium", "Code+3D");
    context.WindowRouter.RouteProcessName("devenv", "Code+3D");
    // context.WindowRouter.RouteProcessName("git-bash", "Code+3D");
    // context.WindowRouter.RouteProcessName("MINGW64:/c/Users/kacpe", "Code+3D");
    context.WindowRouter.RouteProcessName("GitHubDesktop", "Code+3D");
    context.WindowRouter.RouteProcessName("Unity Hub", "Code+3D");
    context.WindowRouter.RouteProcessName("Unity", "Code+3D");
    context.WindowRouter.RouteProcessName("blender", "Code+3D");

    context.WindowRouter.RouteProcessName("WindowsTerminal", "Terminal+Write");
    context.WindowRouter.RouteProcessName("soffice.bin", "Terminal+Write");
    context.WindowRouter.RouteProcessName("soffice.exe", "Terminal+Write");

    context.WindowRouter.RouteProcessName("vmware", "VM");
    context.WindowRouter.RouteProcessName("vmplayer", "VM");

    context.WindowRouter.RouteProcessName("SteelSeries", "Sound");
    context.WindowRouter.RouteProcessName("SteelSeriesGGClient", "Sound");
    context.WindowRouter.RouteProcessName("Spotify", "Sound");

    context.WindowRouter.RouteProcessName("steamwebhelper", "Gaming+Chat");
    context.WindowRouter.RouteProcessName("steam", "Gaming+Chat");
    context.WindowRouter.RouteProcessName("Discord", "Gaming+Chat");
    context.WindowRouter.RouteProcessName("Messenger", "Gaming+Chat");
    context.WindowRouter.RouteProcessName("ts3client_win64", "Gaming+Chat");
    context.WindowRouter.RouteProcessName("Slack", "Gaming+Chat");

    // Filters
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("cs2"));
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("msiexec"));
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("Yubico Authenticator"));

    // Keybindings
    context.Keybinds.Subscribe(KeyModifiers.Win | KeyModifiers.Control, Keys.M, () =>
    {
        actionMenu.ShowMenu(actionMenuBuilder);
    }, "show action menu");
};
return doConfig;
