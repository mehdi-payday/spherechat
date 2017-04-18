using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SphereClient {
    public partial class LoginForm : Form {
        private static LoginForm instance;

        /// <summary>
        /// The constructor for the LoginForm class
        /// </summary>
        protected LoginForm() {
            InitializeComponent();
        }

        /// <summary>
        /// Triggers when the user clicks on the Login button.
        /// Attempts to login.s
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click( object sender, EventArgs e ) {
            try {
                label2.Text = "";
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text)) {
                    label2.Text += "You must enter a valid username\n";
                    return;
                }
                if (string.IsNullOrEmpty( textBox2.Text )) {
                    label2.Text += "The password cannot be empty\n";
                    return;
                }

                Form1.Instance.Connect( textBox1.Text, textBox2.Text );
                this.Hide();
                Form1.Instance.Start();
                Form1.Instance.Show();
                
                
            } catch (Exception ex) {
                label2.Text += "The username or password is invalid\n"+ ex.Message;
            }

            
        }

        /// <summary>
        /// Triggered when the user clicks on the register link.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e ) {
            System.Diagnostics.Process.Start( "http://spherechat.tk/#!/signup" );
        }

        /// <summary>
        /// The singleton accessor for the LoginForm class.
        /// </summary>
        public static LoginForm Instance {
            get {
                if(instance == null) {
                    instance = new LoginForm();
                }
                return instance;
            }
            set {
                instance = value;
            }
        }
    }
}
