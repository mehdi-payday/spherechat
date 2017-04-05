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
            try {
                this.session = new REST.Session( username, password );
                
            } catch (Exception ex) {
                MessageBox.Show( string.Format("An exception occured while creating a session: {0}", ex.Message ));
                Application.Exit();
            }
            System.Threading.Thread t = new System.Threading.Thread(delegate() {
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
                    Form1._instance = new Form1("sphereman","spherique");
                }
                return Form1._instance;
            }
            set {
                Form1._instance = value;
            }
        }

        
    }
}
