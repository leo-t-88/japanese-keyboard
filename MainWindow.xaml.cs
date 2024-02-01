using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Clavier_Jap;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        PreviewKeyDown += KeyPressedKeyboard;
        PreviewKeyUp += OnPreviewKeyUp;
    }
    private bool maj = false;

    private void OnButtonClicked(object sender, RoutedEventArgs e)
    {
        if (sender is Button clickedButton)
        {
            UpdateButtonAndResult(clickedButton);
        }
    }
    private void OnPreviewKeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
        {
            maj = true;
            UpdateButtonAndResult(caps);
        }
    }
    private void KeyPressedKeyboard(object sender, KeyEventArgs e)
    {
        if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl))
        {
            switch (e.Key)
            {
            case Key.D1:
                UpdateButtonAndResult(nu);
                break;
            case Key.D2:
                UpdateButtonAndResult(fu);
                break;
            case Key.D3:
                UpdateButtonAndResult(a);
                break;
            case Key.D4:
                UpdateButtonAndResult(u);
                break;
            case Key.D5:
                UpdateButtonAndResult(ee);
                break;
            case Key.D6:
                UpdateButtonAndResult(o);
                break;
            case Key.D7:
                UpdateButtonAndResult(ya);
                break;
            case Key.D8:
                UpdateButtonAndResult(yu);
                break;
            case Key.D9:
                UpdateButtonAndResult(yo);
                break;
            case Key.D0:
                UpdateButtonAndResult(wa);
                break;
            case Key.OemOpenBrackets:
                UpdateButtonAndResult(ho);
                break;
            case Key.Back:
                UpdateButtonAndResult(bckspc);
                break;
            case Key.OemPlus:
                UpdateButtonAndResult(he);
                break;
            case Key.A:
                UpdateButtonAndResult(ta);
                break;
            case Key.Z:
                UpdateButtonAndResult(te);
                break;
            case Key.E:
                UpdateButtonAndResult(i);
                break;
            case Key.R:
                UpdateButtonAndResult(su);
                break;
            case Key.T:
                UpdateButtonAndResult(ka);
                break;
            case Key.Y:
                UpdateButtonAndResult(n);
                break;
            case Key.U:
                UpdateButtonAndResult(na);
                break;
            case Key.I:
                UpdateButtonAndResult(ni);
                break;
            case Key.O:
                UpdateButtonAndResult(ra);
                break;
            case Key.P:
                UpdateButtonAndResult(se);
                break;
            case Key.Oem6:
                UpdateButtonAndResult(virgu);
                break;
            case Key.Oem1:
                UpdateButtonAndResult(dakuon);
                break;
            case Key.Oem5:
                UpdateButtonAndResult(dakuon2);
                break;
            case Key.Q:
                UpdateButtonAndResult(chi);
                break;
            case Key.S:
                UpdateButtonAndResult(to);
                break;
            case Key.D:
                UpdateButtonAndResult(shi);
                break;
            case Key.F:
                UpdateButtonAndResult(ha);
                break;
            case Key.G:
                UpdateButtonAndResult(ki);
                break;
            case Key.H:
                UpdateButtonAndResult(ku);
                break;
            case Key.J:
                UpdateButtonAndResult(ma);
                break;
            case Key.K:
                UpdateButtonAndResult(no);
                break;
            case Key.L:
                UpdateButtonAndResult(re);
                break;
            case Key.M:
                UpdateButtonAndResult(ri);
                break;
            case Key.Oem3:
                UpdateButtonAndResult(ke);
                break;
            case Key.Enter:
                UpdateButtonAndResult(entr);
                break;
            case Key.OemBackslash:
                UpdateButtonAndResult(tsu);
                break;
            case Key.W:
                UpdateButtonAndResult(sa);
                break;
            case Key.X:
                UpdateButtonAndResult(so);
                break;
            case Key.C:
                UpdateButtonAndResult(hi);
                break;
            case Key.V:
                UpdateButtonAndResult(ko);
                break;
            case Key.B:
                UpdateButtonAndResult(mi);
                break;
            case Key.N:
                UpdateButtonAndResult(mo);
                break;
            case Key.OemComma:
                UpdateButtonAndResult(ne);
                break;
            case Key.OemPeriod:
                UpdateButtonAndResult(ru);
                break;
            case Key.OemQuestion:
                UpdateButtonAndResult(me);
                break;
            case Key.Oem8:
                UpdateButtonAndResult(ro);
                break;
            case Key.Space:
                UpdateButtonAndResult(space);
                break;
            case Key.CapsLock:
            case Key.LeftShift:
            case Key.RightShift:
                UpdateButtonAndResult(caps);
                break;
            default:
                return;
            }
            e.Handled = true;
        }
    }

    private void UpdateButtonAndResult(Button clickedButton)
    {
        SolidColorBrush newBackground = new SolidColorBrush(Color.FromArgb(190, 255, 255, 255));
        clickedButton.Background = newBackground;

        switch (clickedButton.Name)
        {
        case "caps":
            if (maj == false){
                nu.Content = "ヌ";fu.Content = "フ";a.Content = "ア";u.Content = "ウ";ee.Content = "エ";o.Content = "オ";ya.Content = "ヤ";yu.Content = "ユ";yo.Content = "ヨ";wa.Content = "ワ";ho.Content = "ホ";
                ta.Content = "タ";te.Content = "テ";i.Content = "イ";su.Content = "ス";ka.Content = "カ";n.Content = "ン";na.Content = "ナ";ni.Content = "ニ";ra.Content = "ラ";se.Content = "セ";
                chi.Content = "チ";to.Content = "ト";shi.Content = "シ";ha.Content = "ハ";ki.Content = "キ";ku.Content = "ク";ma.Content = "マ";no.Content = "ノ";re.Content = "レ";ri.Content = "リ";ke.Content = "ケ";mu.Content = "ム";
                tsu.Content = "ツ";sa.Content = "サ";so.Content = "ソ";hi.Content = "ヒ";ko.Content = "コ";mi.Content = "ミ";mo.Content = "モ";ne.Content = "ネ";ru.Content = "ル";me.Content = "メ";ro.Content = "ロ";
                maj = true;
            } else if((maj == true) && (!Console.CapsLock)){
                nu.Content = "ぬ";fu.Content = "ふ";a.Content = "あ";u.Content = "う";ee.Content = "え";o.Content = "お";ya.Content = "や";yu.Content = "ゆ";yo.Content = "よ";wa.Content = "わ";ho.Content = "ほ";
                ta.Content = "た";te.Content = "て";i.Content = "い";su.Content = "す";ka.Content = "か";n.Content = "ん";na.Content = "な";ni.Content = "に";ra.Content = "ら";se.Content = "せ";
                chi.Content = "ち";to.Content = "と";shi.Content = "し";ha.Content = "は";ki.Content = "き";ku.Content = "く";ma.Content = "ま";no.Content = "の";re.Content = "れ";ri.Content = "り";ke.Content = "け";mu.Content = "む";
                tsu.Content = "つ";sa.Content = "さ";so.Content = "そ";hi.Content = "ひ";ko.Content = "こ";mi.Content = "み";mo.Content = "も";ne.Content = "ね";ru.Content = "る";me.Content = "め";ro.Content = "ろ";
                maj = false;
            }
            break;
        case "vague":
        case "nu":
        case "fu":
        case "a":
        case "u":
        case "ee":
        case "o":
        case "ya":
        case "yu":
        case "yo":
        case "wa":
        case "ho":
        case "he":
        case "ta":
        case "te":
        case "i":
        case "su":
        case "ka":
        case "n":
        case "na":
        case "ni":
        case "ra":
        case "se":
        case "virgu":
        case "dakuon":
        case "dakuon2":
        case "yen":
        case "chi":
        case "to":
        case "shi":
        case "ha":
        case "ki":
        case "ku":
        case "ma":
        case "no":
        case "re":
        case "ri":
        case "ke":
        case "mu":
        case "tsu":
        case "sa":
        case "so":
        case "hi":
        case "ko":
        case "mi":
        case "mo":
        case "ne":
        case "ru":
        case "me":
        case "ro":
        case "space":
            Resulttext.Text += clickedButton.Content.ToString();
            break;
        case "entr":
            int currentPosition = Resulttext.SelectionStart;
            Resulttext.Text = Resulttext.Text.Insert(currentPosition, "\n");
            break;
        case "bckspc":
            if (Resulttext.Text.Length > 0)
            {
                Resulttext.Text = Resulttext.Text.Substring(0, Resulttext.Text.Length - 1);
            }
            break;
        case "arleft":
            MessageBox.Show("Settings are not available at the moment.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            break;
        default:
            break;
        }
        Resulttext.SelectionStart = Resulttext.Text.Length;

        Task.Delay(150).ContinueWith(t => 
        {
            Dispatcher.Invoke(() =>
            {
                clickedButton.Background = new SolidColorBrush(Color.FromArgb(128, 255, 255, 255));

                if (maj == true) {
                caps.Background = new SolidColorBrush(Color.FromArgb(190, 255, 255, 255));
                }
            });
        });
    }
}