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
    public partial class EditProfile : Form {

        private static EditProfile instance;
        private Entities.User? user;

        /// <summary>
        /// Constructor for the EditProfile class.
        /// </summary>
        /// <param name="user">the user whom's profile has to be edited</param>
        protected EditProfile(Entities.User? user) {
            this.user = user;
            InitializeComponent();
            this.textBox1.Text = this.user.Value.ProfilePicture.ToString();
            this.textBox2.Text = this.user.Value.FirstName;
            this.textBox3.Text = this.user.Value.LastName;
        }

        /// <summary>
        /// Updates the image when typing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyPress( object sender, KeyPressEventArgs e ) {
            this.pictureBox19.LoadAsync( textBox1.Text );
        }

        /// <summary>
        /// Saves the description and image  of the
        /// profile url to the server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click( object sender, EventArgs e ) {
            Entities.User u = new Entities.User();
            u.LastName = this.textBox3.Text;
            u.FirstName = this.textBox2.Text;
            u.ProfilePicture = this.textBox1.Text;
            Form1.Instance.user = u;
            
        }

        /// <summary>
        /// Cancels and closes the form, discarding the changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel3_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e ) {
            this.Close();
        }

        /// <summary>
        /// Singleton accessor.
        /// </summary>
        public static EditProfile Instance {
            get {
                if(null == EditProfile.instance) {
                    EditProfile.instance = new EditProfile(Form1.Instance.user);
                }
                return EditProfile.instance;
            }
            set {
                EditProfile.instance = value;
            }
        }

        private void label1_Click( object sender, EventArgs e ) {

        }
    }
}
