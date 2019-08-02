using System;
using System.Configuration;
using System.IO;
using System.Windows;

namespace WilfredGreeter
{
    public partial class MainWindow
    {
        private readonly WindowHider _windowHider;
        private readonly ImageLoader _imageLoader;
        private readonly MessageFileWatcher _messageWatcher;
        private readonly SoundPlayer _soundPlayer;
        private readonly MessageLoader _messageLoader;
        private readonly MessageLoader _headerMessageLoader;

        public MainWindow()
        {
            InitializeComponent();

            var noDisplayTimeConfigured = !double.TryParse(ConfigurationManager.AppSettings["DisplayTime"], out var displayTime);
            if (noDisplayTimeConfigured) displayTime = 6.0;

            _soundPlayer = new SoundPlayer(ConfigurationManager.AppSettings["SoundFileName"]);
            _windowHider = new WindowHider(this, displayTime);

            var headerFilePath = ConfigurationManager.AppSettings["HeaderMessageFile"];
            var filePath = ConfigurationManager.AppSettings["MessageFile"];
            var networkPath = Path.GetDirectoryName(filePath);
            _headerMessageLoader = new MessageLoader(headerFilePath);
            _messageLoader = new MessageLoader(filePath);

            _messageWatcher = new MessageFileWatcher(networkPath, FileChange, filePath);
            _imageLoader = new ImageLoader(ConfigurationManager.AppSettings["ImagePath"]);

            _windowHider.HideWindow();
            _messageWatcher.Start();

            StateChanged += MainWindow_StateChanged;

            Wilfred.Source = _imageLoader.Image;
            Message.Content = _messageLoader.LoadMessage();
        }

        public MainWindow(string simpleMessage)
        {
            InitializeComponent();

            var noDisplayTimeConfigured = !double.TryParse(ConfigurationManager.AppSettings["DisplayTime"], out var displayTime);
            if (noDisplayTimeConfigured) displayTime = 6.0;

            _soundPlayer = new SoundPlayer(ConfigurationManager.AppSettings["SoundFile"]);
            _windowHider = new WindowHider(this, displayTime);
            
            Message.Content = simpleMessage;

            _soundPlayer.PlaySound();
            _windowHider.ShowWindowForDisplayTime(Close);
        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                _windowHider.HideWindow();
                _messageWatcher.Start();
            }
            else if (WindowState == WindowState.Maximized)
            {
                _messageWatcher.Stop();
            }
        }

        private void FileChange()
        {
            Dispatcher.Invoke(() =>
            {
                Wilfred.Source = _imageLoader.Image;
                HeaderText.Content = _headerMessageLoader.LoadMessage();
                Message.Content = _messageLoader.LoadMessage();

                _windowHider.ShowWindowForDisplayTime(() => { });
                _soundPlayer.PlaySound();
            });
        }
    }
}
