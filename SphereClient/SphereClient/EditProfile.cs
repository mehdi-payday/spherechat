using System;
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
            this.pictureBox19.SizeMode = PictureBoxSizeMode.Zoom;
            this.textBox1.Text = ((bool)this.user?.ProfilePicture.StartsWith("http://") ? "" : "http://") + this.user?.ProfilePicture?.ToString() ?? "";
            this.textBox2.Text = this.user?.FirstName;
            this.textBox3.Text = this.user?.LastName;
        }

        /// <summary>
        /// Saves the properties (images, names, etc.) of the
        /// profile to the server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e) {
            Entities.User u = (Entities.User)Form1.Instance.user;
            u.LastName = (string.IsNullOrWhiteSpace(this.textBox3.Text) ? this.textBox3.Text : u.LastName);
            u.FirstName = (string.IsNullOrWhiteSpace(this.textBox2.Text) ? this.textBox2.Text : u.FirstName); ;
            u.ProfilePicture = (string.IsNullOrWhiteSpace(this.textBox1.Text) ? this.textBox1.Text : u.LastName); ;
            Form1.Instance.session.REST.PostProfile(u);

        }

        /// <summary>
        /// Cancels and closes the form, discarding the changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            this.Close();
        }

        /// <summary>
        /// Singleton accessor.
        /// </summary>
        public static EditProfile Instance {
            get {
                if (null == EditProfile.instance) {
                    EditProfile.instance = new EditProfile(Form1.Instance.user);
                }
                return EditProfile.instance;
            }
            set {
                EditProfile.instance = value;
            }
        }

        /// <summary>
        /// Triggers when the text in the image url field changes,
        /// it updates the preview image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(this.textBox1.Text)) {
                this.pictureBox19.Image = Properties.Resources.default_user_image;
            }
            else {
                this.pictureBox19.LoadAsync(this.textBox1.Text);
            }

        }
    }
}
