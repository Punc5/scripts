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
    context.WorkspaceContainer.CreateWorkspaces("Main", "Sound", "Productivity", "Projects", "VMs", "Sec+Net", "Play+Talk");
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
        ("Sound", defaultLayouts()),
        ("Productivity", defaultLayouts()),
        ("Projects", defaultLayouts()),
        ("VMs", defaultLayouts()),
        ("Sec+Net", defaultLayouts()),
        ("Play+Talk", defaultLayouts()),
    };

    // Routes
    context.WindowRouter.RouteProcessName("SteelSeries", "Sound");
    context.WindowRouter.RouteProcessName("SteelSeriesGGClient", "Sound");
    context.WindowRouter.RouteProcessName("Spotify", "Sound");

    context.WindowRouter.RouteProcessName("chrome", "Productivity");
    context.WindowRouter.RouteProcessName("vivaldi", "Productivity");
    context.WindowRouter.RouteProcessName("brave", "Productivity");
    context.WindowRouter.RouteProcessName("Tor Browser", "Productivity");
    context.WindowRouter.RouteProcessName("thunderbird", "Productivity");

    context.WindowRouter.RouteProcessName("VSCodium", "Projects");
    context.WindowRouter.RouteProcessName("devenv", "Projects");
    // context.WindowRouter.RouteProcessName("git-bash", "Projects");
    // context.WindowRouter.RouteProcessName("MINGW64:/c/Users/kacpe", "Projects");
    context.WindowRouter.RouteProcessName("GitHubDesktop", "Projects");
    context.WindowRouter.RouteProcessName("Unity Hub", "Projects");
    context.WindowRouter.RouteProcessName("Unity", "Projects"); 
    context.WindowRouter.RouteProcessName("blender", "Projects");
    // context.WindowRouter.RouteProcessName("ChatGPT", "Projects");
    context.WindowRouter.RouteProcessName("WindowsTerminal", "Projects");
    context.WindowRouter.RouteProcessName("soffice.bin", "Projects");
    context.WindowRouter.RouteProcessName("soffice.exe", "Projects");

    context.WindowRouter.RouteProcessName("vmware", "VMs");
    context.WindowRouter.RouteProcessName("vmplayer", "VMs");

    context.WindowRouter.RouteProcessName("KeePassXC", "Sec+Net");
    context.WindowRouter.RouteProcessName("Bitwarden", "Sec+Net");
    context.WindowRouter.RouteProcessName("Malwarebytes", "Sec+Net");
    context.WindowRouter.RouteProcessName("Windows Security", "Sec+Net");
    context.WindowRouter.RouteProcessName("Wireshark", "Sec+Net");
    context.WindowRouter.RouteProcessName("NextDNS", "Sec+Net");

    context.WindowRouter.RouteProcessName("steamwebhelper", "Play+Talk");
    context.WindowRouter.RouteProcessName("steam", "Play+Talk");
    context.WindowRouter.RouteProcessName("Discord", "Play+Talk");
    context.WindowRouter.RouteProcessName("Messenger", "Play+Talk");
    context.WindowRouter.RouteProcessName("ts3client_win64", "Play+Talk");
    context.WindowRouter.RouteProcessName("Slack", "Play+Talk");

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
