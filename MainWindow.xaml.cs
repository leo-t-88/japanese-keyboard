using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Text.Json;
using Microsoft.Win32;
using System.Windows.Data;
using System.Windows.Media.Animation;

namespace Clavier_Jap;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public string BackgroundImagePath { get; set; }
    public string CustomImagePath { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        SettignsApp(new string[] {"Start"});
        PreviewKeyDown += KeyPressedKeyboard;
        PreviewKeyUp += OnPreviewKeyUp;
    }
    private bool maj = false;
    private bool setts = false;

    public class KeyboardConfig
    {
        public string KeyboardApp { get; set; }
        public string BackgroundApp { get; set; }
        public string CustomImg { get; set; }
    }
    private void SettignsApp(string[] args)
    {
        string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        if (!File.Exists(configFilePath))
        {
            var defaultConfig = new KeyboardConfig
            {
                KeyboardApp = "qwerty",
                BackgroundApp = "default",
                CustomImg = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "custom.jpg")
            };
            string jsonString = JsonSerializer.Serialize(defaultConfig);
            File.WriteAllText(configFilePath, jsonString);
        }
        string jsonContent = File.ReadAllText(configFilePath);
        var config = JsonSerializer.Deserialize<KeyboardConfig>(jsonContent);
        if (args.Contains("Qwerty"))
        {
            config.KeyboardApp = "qwerty";
        }
        else if (args.Contains("Azerty"))
        {
            config.KeyboardApp = "azerty";
        }
        else if (args.Contains("SmartInput"))
        {
            config.KeyboardApp = "smartinput";
        }
        else if (args.Contains("Default"))
        {
            config.BackgroundApp = "default";
        }
        else if (args.Contains("Custom"))
        {
            config.BackgroundApp = "custom";
        }
        else if (!args.Contains("Start"))
        {
            config.CustomImg = args[0];
        }
        if (!File.Exists(config.CustomImg)){
            config.CustomImg = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "custom.jpg");
        }
        string updatedJson = JsonSerializer.Serialize(config);
        File.WriteAllText(configFilePath, updatedJson);
        CustomImagePath = config.CustomImg;
        if (config.BackgroundApp == "custom")
        {
            BtnCuActive.Visibility = Visibility.Visible;
            BtnCu.Visibility = Visibility.Collapsed;
            BtnDeActive.Visibility = Visibility.Collapsed;
            BtnDe.Visibility = Visibility.Visible;
            BackgroundImagePath = config.CustomImg;
        } else {
            BtnDeActive.Visibility = Visibility.Visible;
            BtnDe.Visibility = Visibility.Collapsed;
            BtnCuActive.Visibility = Visibility.Collapsed;
            BtnCu.Visibility = Visibility.Visible;
            BackgroundImagePath = "./assets/default.jpg";
        }
        if (config.KeyboardApp == "azerty")
        {
            BtnAzActive.Visibility = Visibility.Visible;
            BtnAz.Visibility = Visibility.Collapsed;
            BtnQwActive.Visibility = Visibility.Collapsed;
            BtnQw.Visibility = Visibility.Visible;
            BtnSIActive.Visibility = Visibility.Collapsed;
            BtnSI.Visibility = Visibility.Visible;
        } else if (config.KeyboardApp == "smartinput"){
            BtnAzActive.Visibility = Visibility.Collapsed;
            BtnAz.Visibility = Visibility.Visible;
            BtnQwActive.Visibility = Visibility.Collapsed;
            BtnQw.Visibility = Visibility.Visible;
            BtnSIActive.Visibility = Visibility.Visible;
            BtnSI.Visibility = Visibility.Collapsed;
        } else {
            BtnQwActive.Visibility = Visibility.Visible;
            BtnQw.Visibility = Visibility.Collapsed;
            BtnAzActive.Visibility = Visibility.Collapsed;
            BtnAz.Visibility = Visibility.Visible;
            BtnSIActive.Visibility = Visibility.Collapsed;
            BtnSI.Visibility = Visibility.Visible;
        }
    }
    private void WindowStateChanged(object sender, EventArgs e)
    {
        if (WindowState == WindowState.Maximized)
        {
            Topmost = false;
            madeby.HorizontalAlignment = HorizontalAlignment.Left;
        }
        else
        {
            Topmost = true;
            madeby.HorizontalAlignment = HorizontalAlignment.Center;
        }
    }
    private readonly Dictionary<string, string> substitutionMap = new Dictionary<string, string>()
        {
            {"chi", "ち"},{"shi", "し"},{"tsu", "つ"},
            {"kya", "きゃ"},{"kyu", "きゅ"},{"kyo", "きょ"},{"gya", "ぎゃ"},{"gyu", "ぎゅ"},{"gyo", "ぎょ"},{"sha", "しゃ"},{"shu", "しゅ"},{"sho", "しょ"},{"ja", "じゃ"},{"ju", "じゅ"},{"jo", "じょ"},{"cha", "ちゃ"},{"chu", "ちゅ"},{"cho", "ちょ"},{"nya", "にゃ"},{"nyu", "にゅ"},{"nyo", "にょ"},{"hya", "ひゃ"},{"hyu", "ひゅ"},{"hyo", "ひょ"},{"bya", "びゃ"},{"byu", "びゅ"},{"byo", "びょ"},{"pya", "ぴゃ"},{"pyu", "ぴゅ"},{"pyo", "ぴょ"},{"mya", "みゃ"},{"myu", "みゅ"},{"myo", "みょ"},{"rya", "りゃ"},{"ryu", "りゅ"},{"ryo", "りょ"},
            {"ya", "や"},{"wa", "わ"},{"ta", "た"},{"ka", "か"},{"んa", "な"},{"ra", "ら"},{"ha", "は"},{"ma", "ま"},{"sa", "さ"},{"ke", "け"},{"he", "へ"},{"me", "め"},{"te", "て"},{"んe", "ね"},{"se", "せ"},{"re", "れ"},{"yu", "ゆ"},{"fu", "ふ"},{"mu", "む"},{"ku", "く"},{"んu", "ぬ"},{"su", "す"},{"ru", "る"},{"yo", "よ"},{"ho", "ほ"},{"mo", "も"},{"ko", "こ"},{"んo", "の"},{"so", "そ"},{"ro", "ろ"},{"to", "と"},{"hi", "ひ"},{"mi", "み"},{"ki", "き"},{"んi", "に"},{"ri", "り"},
            {"ga", "が"},{"gi", "ぎ"},{"gu", "ぐ"},{"ge", "げ"},{"go", "ご"},{"za", "ざ"},{"ji", "じ"},{"zu", "ず"},{"ze", "ぜ"},{"zo", "ぞ"},{"da", "だ"},{"じi", "ぢ"},{"ずu", "づ"},{"de", "で"},{"do", "ど"},{"ba", "ば"},{"bi", "び"},{"bu", "ぶ"},{"be", "べ"},{"bo", "ぼ"},{"pa", "ぱ"},{"pi", "ぴ"},{"pu", "ぷ"},{"pe", "ぺ"},{"po", "ぽ"},
            {"a", "あ"},{"u", "う"},{"o", "お"},{"e", "え"},{"i", "い"},{"n", "ん"},{",", "、"},
            {"CHI", "チ"},{"SHI", "シ"},{"TSU", "ツ"},
            {"KYA", "キャ"},{"KYU", "キュ"},{"KYO", "キョ"},{"GYA", "ギャ"},{"GYU", "ギュ"},{"GYO", "ギョ"},{"SHA", "シャ"},{"SHU", "シュ"},{"SHO", "ショ"},{"JA", "ジャ"},{"JU", "ジュ"},{"JO", "ジョ"},{"CHA", "チャ"},{"CHU", "チュ"},{"CHO", "チョ"},{"NYA", "ニャ"},{"NYU", "ニュ"},{"NYO", "ニョ"},{"HYA", "ヒャ"},{"HYU", "ヒュ"},{"HYO", "ヒョ"},{"BYA", "ビャ"},{"BYU", "ビュ"},{"BYO", "ビョ"},{"PYA", "ピャ"},{"PYU", "ピュ"},{"PYO", "ピュ"},{"MYA", "ミャ"},{"MYU", "ミュ"},{"MYO", "ミョ"},{"RYA", "リャ"},{"RYU", "リュ"},{"RYO", "リョ"},
            {"YA", "ヤ"},{"WA", "ワ"},{"TA", "タ"},{"KA", "カ"},{"ンA", "ナ"},{"RA", "ラ"},{"HA", "ハ"},{"MA", "マ"},{"SA", "サ"},{"KE", "ケ"},{"HE", "ヘ"},{"ME", "メ"},{"TE", "テ"},{"ンE", "ネ"},{"SE", "セ"},{"RE", "レ"},{"YU", "ユ"},{"FU", "フ"},{"MU", "ム"},{"KU", "ク"},{"ンU", "ヌ"},{"SU", "ス"},{"RU", "ル"},{"YO", "ヨ"},{"HO", "ホ"},{"MO", "モ"},{"KO", "コ"},{"ンO", "ノ"},{"SO", "ソ"},{"RO", "ロ"},{"TO", "ト"},{"HI", "ヒ"},{"MI", "ミ"},{"KI", "き"},{"リI", "キ"},{"RI", "リ"},
            {"GA", "ガ"},{"GI", "ギ"},{"GU", "グ"},{"GE", "ゲ"},{"GO", "ゴ"},{"ZA", "ザ"},{"JI", "ジ"},{"ZU", "ズ"},{"ZE", "ゼ"},{"ZO", "ゾ"},{"DA", "ダ"},{"ジi", "ヂ"},{"ズu", "ヅ"},{"DE", "デ"},{"DO", "ド"},{"BA", "バ"},{"BI", "ビ"},{"BU", "ブ"},{"BE", "ベ"},{"BO", "ボ"},{"PA", "パ"},{"PI", "ピ"},{"PU", "プ"},{"PE", "ペ"},{"PO", "ポ"},
            {"A", "ア"},{"I", "イ"},{"U", "ウ"},{"E", "エ"},{"O", "オ"},{"N", "ン"},
        };
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
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            string jsonContent = File.ReadAllText(configFilePath);
            var config = JsonSerializer.Deserialize<KeyboardConfig>(jsonContent);
            if (config.KeyboardApp != "smartinput"){
                if (config.KeyboardApp == "azerty")
                {
                    switch (e.Key)
                    {
                    case Key.OemOpenBrackets:
                        UpdateButtonAndResult(ho);
                        break;
                    case Key.OemQuotes:
                        UpdateButtonAndResult(vague);
                        break;
                    case Key.A:
                        UpdateButtonAndResult(ta);
                        break;
                    case Key.Z:
                        UpdateButtonAndResult(te);
                        break;
                    case Key.Oem6:
                        UpdateButtonAndResult(virgu);
                        break;
                    case Key.Oem1:
                        UpdateButtonAndResult(dakuon);
                        break;
                    case Key.Q:
                        UpdateButtonAndResult(chi);
                        break;
                    case Key.M:
                        UpdateButtonAndResult(ri);
                        break;
                    case Key.Oem3:
                        UpdateButtonAndResult(ke);
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
                    case Key.W:
                        UpdateButtonAndResult(sa);
                        break;
                    default:
                        break;
                    }
                } else {
                    switch (e.Key)
                    {
                    case Key.OemMinus:
                        UpdateButtonAndResult(ho);
                        break;
                    case Key.Oem3:
                        UpdateButtonAndResult(vague);
                        break;
                    case Key.Q:
                        UpdateButtonAndResult(ta);
                        break;
                    case Key.W:
                        UpdateButtonAndResult(te);
                        break;
                    case Key.OemOpenBrackets:
                        UpdateButtonAndResult(virgu);
                        break;
                    case Key.Oem6:
                        UpdateButtonAndResult(dakuon);
                        break;
                    case Key.Z:
                        UpdateButtonAndResult(sa);
                        break;
                    case Key.A:
                        UpdateButtonAndResult(chi);
                        break;
                    case Key.Oem1:
                        UpdateButtonAndResult(ri);
                        break;
                    case Key.OemQuotes:
                        UpdateButtonAndResult(ke);
                        break;
                    case Key.M:
                        UpdateButtonAndResult(ne);
                        break;
                    case Key.OemComma:
                        UpdateButtonAndResult(ru);
                        break;
                    case Key.OemPeriod:
                        UpdateButtonAndResult(me);
                        break;
                    case Key.OemQuestion:
                        UpdateButtonAndResult(ro);
                        break;
                    default:
                        break;
                    }
                }
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
                case Key.OemPlus:
                    UpdateButtonAndResult(he);
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
                case Key.OemBackslash:
                    UpdateButtonAndResult(tsu);
                    break;
                case Key.Oem5:
                    UpdateButtonAndResult(dakuon2);
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
                default:
                    break;
                }
                e.Handled = true;
            }
            if (e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.RightShift){
                UpdateButtonAndResult(caps);
                e.Handled = true;
            } else if (e.Key == Key.Space){
                UpdateButtonAndResult(space);
                e.Handled = true;
            } else if (e.Key == Key.Enter){
                UpdateButtonAndResult(entr);
                e.Handled = true;
            } else if (e.Key == Key.Back){
                UpdateButtonAndResult(bckspc);
                e.Handled = true;
            } else if (e.Key == Key.Left){
                UpdateButtonAndResult(leftarr);
                e.Handled = true;
            } else if (e.Key == Key.Right){
                UpdateButtonAndResult(rightarr);
                e.Handled = true;
            }

            if (config.KeyboardApp == "smartinput"){
                Task.Delay(50).ContinueWith(t => 
                {
                    Dispatcher.Invoke(() =>
                    {
                        int positionDuCurseur = Resulttext.CaretIndex;
                        string texteSaisi = Resulttext.Text;
                        foreach (var entry in substitutionMap)
                        {
                            texteSaisi = texteSaisi.Replace(entry.Key, entry.Value);
                        }
                        Resulttext.Text = texteSaisi;
                        if (e.Key != Key.Left && e.Key != Key.Right){
                            Resulttext.CaretIndex = positionDuCurseur;
                        }
                    });
                });
            }
        }
    }
    private void SettingsBtnDown(object sender, MouseButtonEventArgs e)
    {
        SettingsPage();
    }
    private void SettingsPage()
    {
        if (setts == false)
        {
            Stngspage.Visibility = Visibility.Visible;
            setts = true;
        }
        else
        {
            Stngspage.Visibility = Visibility.Collapsed;
            setts = false;
        }
    }

    private void ChangeCustomImg()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Image file|*.jpg;*.jpeg;*.png;*.jfif;*.webp";
        bool? result = openFileDialog.ShowDialog();
        if (result == true)
        {
            SettignsApp(new string[] {openFileDialog.FileName});
            SettignsApp(new string[] {"Custom"});
            Restart();
        }
    }

    private void Restart()
    {
        string Textouput = Resulttext.Text;
        bool isMaximized = WindowState == WindowState.Maximized;
        int x = (int)Left; int y = (int)Top; int height = (int)Height; int width = (int)Width;
        var NewWindow = new MainWindow();
        NewWindow.SettingsPage();
        NewWindow.Show();
        if (isMaximized)
        {
            NewWindow.WindowState = WindowState.Maximized;
        } else {
            NewWindow.Left = x; NewWindow.Top = y; NewWindow.Height = height; NewWindow.Width = width;
        }
        Application.Current.MainWindow.Close();
        Application.Current.MainWindow = NewWindow;
        NewWindow.Resulttext.Text = Textouput;
        if(Console.CapsLock){
            NewWindow.UpdateButtonAndResult(caps);
        }
    }

    private void UpdateButtonAndResult(Button clickedButton)
    {
        SolidColorBrush newBackground = new SolidColorBrush(Color.FromArgb(190, 255, 255, 255));
        clickedButton.Background = newBackground;
        int positionDuCurseur = Resulttext.CaretIndex;
        Resulttext.Focus();

        switch (clickedButton.Name)
        {
        case "BtnQw":
            SettignsApp(new string[] {"Qwerty"});
            return;
        case "BtnAz":
            SettignsApp(new string[] {"Azerty"});
            return;
        case "BtnSI":
            SettignsApp(new string[] {"SmartInput"});
            return;
        case "BtnDe":
            SettignsApp(new string[] {"Default"});
            Restart();
            return;
        case "BtnCu":
            SettignsApp(new string[] {"Custom"});
            Restart();
            return;
        case "BtnRe":
            ChangeCustomImg();
            return;
        case "caps":
                //I know there's a visual glitch where, if you hold down the shift key, the letters change each time between katagana and hiragana (on a high-performance/powerful computer).
                //If anyone knows how to solve this problem, please let me know by opening a pull request.
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
            Resulttext.Text = Resulttext.Text.Insert(Resulttext.CaretIndex, clickedButton.Content.ToString());
            Resulttext.CaretIndex = positionDuCurseur + 1;
            break;
        case "entr":
            Resulttext.Text = Resulttext.Text.Insert(Resulttext.CaretIndex, "\n");
            Resulttext.CaretIndex = positionDuCurseur + 1;
            break;
        case "leftarr":
            if (positionDuCurseur > 0)
            {
                Resulttext.CaretIndex = positionDuCurseur - 1;
            }
            break;
        case "rightarr":
            Resulttext.CaretIndex = positionDuCurseur + 1;
            break;
        case "bckspc":
            if (Resulttext.SelectionLength > 0)
            {
                Resulttext.Text = Resulttext.Text.Remove(Resulttext.CaretIndex, Resulttext.SelectionLength);
                Resulttext.CaretIndex = positionDuCurseur;
            }
            else if (Resulttext.SelectionStart > 0)
            {
                Resulttext.Text = Resulttext.Text.Remove(Resulttext.SelectionStart - 1, 1);
                Resulttext.CaretIndex = positionDuCurseur - 1;
            }
            break;
        case "stgs":
            SettingsPage();
            break;
        default:
            break;
        }
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

    private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
        e.Handled = true;
    }
}