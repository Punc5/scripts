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
            new TextWidget("                        "),
            new TitleWidget()
            {
                IsShortTitle = true,
            }
        },

        // Right Widgets
        RightWidgets = () => new IBarWidget[]
        {
            new TextWidget("                   "),
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
    context.WorkspaceContainer.CreateWorkspaces("Main", "Browsers", "Terminal+Code", "Work+School", "Sound", "Chat", "Gaming", "~Other");
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
        ("Code", defaultLayouts()),
        ("Work", defaultLayouts()),
        ("Sound", defaultLayouts()),
        ("Chat", defaultLayouts()),
        ("Gaming", defaultLayouts()),
        ("Other", defaultLayouts()),
    };

    // Routes
    context.WindowRouter.RouteProcessName("chrome", "Browsers");
    context.WindowRouter.RouteProcessName("vivaldi", "Browsers");
    context.WindowRouter.RouteProcessName("brave", "Browsers");
    context.WindowRouter.RouteProcessName("Tor", "Browsers");

    context.WindowRouter.RouteProcessName("WindowsTerminal", "Terminal+Code");
    context.WindowRouter.RouteProcessName("VSCodium", "Terminal+Code");

    context.WindowRouter.RouteProcessName("SteelSeries", "Sound");
    context.WindowRouter.RouteProcessName("SteelSeriesGGClient", "Sound");
    context.WindowRouter.RouteProcessName("Spotify", "Sound");

    context.WindowRouter.RouteProcessName("Discord", "Chat");
    context.WindowRouter.RouteProcessName("Messenger", "Chat");
    context.WindowRouter.RouteProcessName("ts3client_win64", "Chat");
    context.WindowRouter.RouteProcessName("Slack", "Chat");

    context.WindowRouter.RouteProcessName("steamwebhelper", "Gaming");
    context.WindowRouter.RouteProcessName("steam", "Gaming");

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