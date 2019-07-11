using log4net;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Log4NetPractise
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string myTextBoxLog;
        public string MyTextBoxLog
        {
            get => myTextBoxLog;
            set
            {
                myTextBoxLog = value;
                OnPropertyChanged(nameof(MyTextBoxLog));
            }
        }
        public DelegateCommand MyClickCmd { get; }
        public string Name { get; set; }
        private ConsoleWriter consoleWriter;

        public ViewModel()
        {
            MyClickCmd = new DelegateCommand(MyExecution);
            consoleWriter = new ConsoleWriter();
            consoleWriter.WriteEvent += consoleWriter_WriteEvent;
            consoleWriter.WriteLineEvent += consoleWriter_WriteLineEvent;
            Console.SetOut(consoleWriter);
        }

        private void consoleWriter_WriteLineEvent(object sender, ConsoleWriterEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void consoleWriter_WriteEvent(object sender, ConsoleWriterEventArgs e)
        {
            MyTextBoxLog += e.Value;
        }

        private async void MyExecution(object obj)
        {
            for (int i=1; i<=200; i++)
            {
                //MyTextBoxLog += $"{i} : Hello World!" + Environment.NewLine;
                log.Debug($"{i} : Hello World!");
                await Task.Delay(100);
            }
        }

    }
}
