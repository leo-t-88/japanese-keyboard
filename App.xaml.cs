using System.Configuration;
using System.Data;
using System.Windows;
using System.IO;

namespace Clavier_Jap;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        if (!ExistImg("assets/azerty.png") || !ExistImg("assets/qwerty.png") || !ExistImg("assets/smartinput.png") || !ExistImg("assets/default.jpg") || !ExistImg("assets/custom.jpg") || !ExistImg("assets/changebg.jpg"))
        {
            MessageBoxResult result = MessageBox.Show("Some images needed for the application to work are missing.\n\nPlease reinstall the application or the missing images.", "Error - Missing images", MessageBoxButton.OK, MessageBoxImage.Error);
            if (result == MessageBoxResult.OK)
            {
                Shutdown();
            }
        }
    }
    private bool ExistImg(string cheminImage)
    {
        return File.Exists(cheminImage);
    }
}