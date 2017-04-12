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
using System.Net;

namespace SphereClient {
    /// <summary>
    /// Main form class
    /// </summary>
    public partial class Form1 : Form {
        private static Form1 _instance = null;

        public Session session;
        public User? user;
        public IList<Entity> fetchedChannels;
        public Channel currentChannel;
        public Panel preloader;
        /// <summary>
        /// Static initializer
        /// </summary>
        static Form1(){

        }
        
        /// <summary>
        /// Constructor for the Form1 class. Initializes the object
        /// and creates a session.
        /// </summary>
        /// <param name="username">username to use to create a session</param>
        /// <param name="password">password for the username's account</param>
        protected Form1(string username, string password) {
            InitializeComponent();
            MakePreloader();
            Connect( username, password );
        }
        /// <summary>
        /// Default contructor for the Form1 class.
        /// </summary>
        protected Form1( ) {
            InitializeComponent();
            MakePreloader();
        }

        /// <summary>
        /// Generates the preloader object.
        /// </summary>
        private void MakePreloader() {
            preloader = new Panel();
            preloader.Size = this.Size;
            preloader.Left = 0;
            preloader.Top = 0;
            preloader.BackColor = Color.White;
            PictureBox pb = new PictureBox();
            pb.Image = Properties.Resources._25;
            pb.SizeMode = PictureBoxSizeMode.CenterImage;
            pb.Size = new Size( 200, 200 );
            pb.Parent = preloader;
            pb.Left = (int)(preloader.Width - pb.Width) / 2;
            pb.Top = (int)(preloader.Height - pb.Height) / 2;
            preloader.Controls.Add( pb );
            this.Controls.Add( preloader );
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
            this.user = this.session.GetProfile();
        }

        /// <summary>
        /// Starts the main application logic loop.
        /// </summary>
        public void Start() {
            ShowPreloader();         
            System.Threading.Thread t = new System.Threading.Thread( delegate () {
                FetchChannels();
            } );
            t.Start();
            
        }

        /// <summary>
        /// Gets the Channels from the server and updates both
        /// the Direct and Group message panels.
        /// </summary>
        public void FetchChannels() {
            if (InvokeRequired) {
                Invoke( new Action( FetchChannels) );
                return;
            }
            this.fetchedChannels = new List<Entity>();
            foreach (var c in this.session.GetChannels()) {
                this.fetchedChannels.Add( c );
            }
            this.currentChannel = (Channel)fetchedChannels[0];
            panel4.FetchMessages( this.currentChannel);

            if (!this.panel7.TryCreateComponents() || !this.panel8.TryCreateComponents()) {
                Application.Exit();
            }

            System.Threading.Thread t = new System.Threading.Thread(()=> {
                System.Threading.Thread.Sleep(2000);
                Form1.Instance.HidePreloader();
            } );
            t.Start();
            
        }

        /// <summary>
        /// Sets the currently viewed channel and updates the message box pane.
        /// </summary>
        /// <param name="index">the id of the channel</param>
        public void SetCurrentViewedChannel(int id) {
            if (InvokeRequired) {
                Invoke(new Action(()=> { SetCurrentViewedChannel( id); } ));
                return;
            }
            if(id == this.currentChannel.ChannelId) {
                return;
            }
            IEnumerable<Entity> list = this.fetchedChannels.Where( c => ((Channel)c).ChannelId == id );
            this.currentChannel = (Channel)(list.Any() ? list.First() : null);
            this.panel4.FetchMessages( this.currentChannel );
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

        /// <summary>
        /// Triggered once the form just closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed( object sender, FormClosedEventArgs e ) {
            Application.Exit();
        }

        /// <summary>
        /// Triggered when the user clicks on the "Send" button (the paper plane arrow icon) 
        /// from the message input section.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click( object sender, EventArgs e ) {
            Entities.Message msg = new Entities.Message();
            msg.Contents = this.richTextBox1.Text;
            this.session.PostMessageToChannel( msg, this.currentChannel );
            
        }

        /// <summary>
        /// Shows the preloader.
        /// </summary>
        public void ShowPreloader() {
            if (InvokeRequired) {
                Invoke( new Action( ShowPreloader ) );
                return;
            }
            this.preloader.BringToFront();
        }

        /// <summary>
        /// Hides the preloader.
        /// </summary>
        public void HidePreloader() {
            if (InvokeRequired) {
                Invoke( new Action( HidePreloader ) );
                return;
            }
            this.preloader.SendToBack();
        }
    }
}
