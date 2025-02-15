using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Clavier_Jap;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        /* Not needed since the app is compiled into a single file.
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string appDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string assetsPath = Path.Combine(appDirectory, "assets");

        if (!File.Exists(Path.Combine(assetsPath, "azerty.png")) ||
            !File.Exists(Path.Combine(assetsPath, "qwerty.png")) ||
            !File.Exists(Path.Combine(assetsPath, "smartinput.png")) ||
            !File.Exists(Path.Combine(assetsPath, "default.jpg")) ||
            !File.Exists(Path.Combine(assetsPath, "custom.jpg")) ||
            !File.Exists(Path.Combine(assetsPath, "changebg.jpg")))
        {
            MessageBoxResult result = MessageBox.Show("Some images needed for the application to work are missing.\n\nPlease reinstall the application or the missing images.", "Error - Missing images", MessageBoxButton.OK, MessageBoxImage.Error);
            if (result == MessageBoxResult.OK)
            {
                Shutdown();
            }
        }*/
    }
    private bool ExistImg(string cheminImage)
    {
        return File.Exists(cheminImage);
    }
}