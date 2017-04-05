using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SphereClient.REST;
using SphereClient.Logger;
using SphereClient.Components;
using SphereClient.Entities;

namespace SphereClient {
    public partial class Form1 : Form {
        private static Form1 _instance = null;


        public Session session;
        public IList<Channel> fetchedChannels;

        /// <summary>
        /// Constructor for the Form1 class. Initializes the object
        /// and creates a session.
        /// </summary>
        /// <param name="username">username to use to create a session</param>
        /// <param name="password">password for the username's account</param>
        protected Form1(string username, string password) {
            InitializeComponent();
            Connect( username, password );
        }
        /// <summary>
        /// Default contructor for the Form1 class.
        /// </summary>
        protected Form1( ) {
            InitializeComponent();
        }

        /// <summary>
        /// Attempts to connect as a user and starts the main application loop.
        /// </summary>
        /// <param name="username">the username to use</param>
        /// <param name="password">the password to use</param>
        public void ConnectAndStart(string username, string password) {
            Connect( username, password );
            Start();
        }

        /// <summary>
        /// Attempts to connect to the server. This method throws
        /// upon failure.
        /// </summary>
        /// <param name="username">the username to use</param>
        /// <param name="password">the password to use</param>
        public void Connect( string username, string password ) {
            this.session = new REST.Session( username, password );
        }

        /// <summary>
        /// Starts the main application logic loop.
        /// </summary>
        public void Start() {            
            System.Threading.Thread t = new System.Threading.Thread( delegate () {
                FetchChannels();
            } );
            t.Start();
            
        }

        /// <summary>
        /// Gets the Channels from the server and updates both
        /// the Direct and Group message panels.
        /// </summary>
        private void FetchChannels() {
            if (InvokeRequired) {
                Invoke( new Action( FetchChannels) );
                return;
            }
            this.fetchedChannels = this.session.GetChannels().ToList();

            if (!this.panel7.TryCreateComponents() || !this.panel8.TryCreateComponents()) {
                Application.Exit();
            }
        }


        /// <summary>
        /// Returns the main form window instance singleton.
        /// </summary>
        public static Form1 Instance {
            get {
                if(null == Form1._instance) {
                    Form1.Create();
                }
                return Form1._instance;
            }
            set {
                Form1._instance = value;
            }
        }

        /// <summary>
        /// Creates the singleton instance of Form1 and connects using a username and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void Create(string username, string password) {
            Form1.Instance = new Form1(username, password);
        }
        /// <summary>
        /// Creates the singleton instance of Form1.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void Create() {
            Form1.Instance = new Form1();
        }

        private void Form1_FormClosed( object sender, FormClosedEventArgs e ) {
            Application.Exit();
        }
    }
}
