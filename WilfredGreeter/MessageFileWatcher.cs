using System;
using System.IO;

namespace WilfredGreeter
{
    public class MessageFileWatcher
    {
        private readonly Action _fileChangedAction;
        private readonly string _messageFile;
        private readonly FileSystemWatcher _watcher;

        private readonly bool _shouldWatchNetworkFile;

        public MessageFileWatcher(string networkPath, Action fileChangedAction, string messageFile)
        {
            _shouldWatchNetworkFile = !string.IsNullOrEmpty(networkPath) && Directory.Exists(networkPath);
            _fileChangedAction = fileChangedAction;
            _messageFile = messageFile;

            _watcher = _shouldWatchNetworkFile
                ? new FileSystemWatcher(networkPath) 
                : new FileSystemWatcher();

            _watcher.Changed += _watcher_Changed;
        }

        public void Stop() => _watcher.EnableRaisingEvents = false;
        public void Start() => _watcher.EnableRaisingEvents = _shouldWatchNetworkFile;
        private void _watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if(e.FullPath == _messageFile)_fileChangedAction();
        }
    }
}
