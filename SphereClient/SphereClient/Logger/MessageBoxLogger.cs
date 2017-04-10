using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SphereClient.Logger {
    public class MessageBoxLogger: Logger {
        private static MessageBoxLogger _instance;

        public MessageBoxLogger() {
            this.OnLog += this.Log;
            this.OnLogLine += this.Log;
        }

        private void Log(object sender, LogEventArgs args) {
            MessageBox.Show( args.message );
        }

        public static MessageBoxLogger Instance{
            get {
                if (null == MessageBoxLogger._instance) {
                    MessageBoxLogger._instance = new MessageBoxLogger();
                }
                return MessageBoxLogger._instance;
            }
            set {
                MessageBoxLogger._instance = value;
            }
        }
    }
}
