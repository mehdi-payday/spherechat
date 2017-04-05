using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereClient.Logger {
    public class LogEventArgs: EventArgs {
        public string message;
        public LogEventArgs(string msg): base() {
            this.message = msg;
        }
    }
    public abstract class Logger {
        public delegate void LogDelegate( object sender, LogEventArgs args);
        public event LogDelegate OnLog;
        public event LogDelegate OnLogLine;

        public void Log(object s, string message ) {
            this.OnLog(s, new LogEventArgs(message));
        }
        public bool TryLog(object s, string message ) {
            bool status = false;
            try {
                this.OnLog( s, new LogEventArgs( message ) );
                status = true;
            } catch (Exception) {
                //void
            }

            return status;
        }
        public void LogLine(object s, string message ) {
            this.OnLogLine( s, new LogEventArgs( message ) );
        }
        public bool TryLogLine( object s, string message ) {
            bool status = false;
            try {
                this.OnLogLine( s, new LogEventArgs( message ) );
                status = true;
            } catch (Exception) {
                //void
            }

            return status;
        }
        
    }
}
