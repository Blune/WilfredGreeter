using System;
using System.Windows;
using System.Windows.Threading;

namespace WilfredGreeter
{
    public class WindowHider
    {
        private readonly Window _window;
        private readonly double _displayTime;

        public WindowHider(Window window, double displayTime)
        {
            _window = window;
            _displayTime = displayTime;
            _window.ShowInTaskbar = false;
        }

        public void HideWindow()
        {
            _window.Hide();
        }

        public void ShowWindow()
        {
            _window.Visibility = Visibility.Visible;
            _window.WindowState = WindowState.Maximized;
            _window.Show();
        }

        public void ShowWindowForDisplayTime(Action afterDisplayTime)
        {
            ShowWindow();
            StartCloseTimer();
            

            void StartCloseTimer()
            {
                DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(_displayTime)};
                timer.Tick += TimerTick;
                timer.Start();
            }
            void TimerTick(object sender, EventArgs e)
            {
                DispatcherTimer timer = (DispatcherTimer)sender;
                timer.Stop();
                timer.Tick -= TimerTick;
                _window.WindowState = WindowState.Minimized;
                afterDisplayTime();
            }
        }
    }
}
