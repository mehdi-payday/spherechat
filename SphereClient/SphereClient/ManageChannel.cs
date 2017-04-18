using SphereClient.Entities;
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
    
    public partial class ManageChannel : Form {
        private Channel channel;
        private User[] users;

        /// <summary>
        /// Constructor for the ManageChannel form
        /// </summary>
        /// <param name="c"></param>
        public ManageChannel(Channel c) {
            this.channel = c;
            InitializeComponent();
            this.textBox1.Text = this.channel.Title;
            this.textBox2.Text = this.channel.Description;
            this.comboBox1.SelectedIndex = this.channel.Type == Channel.Types.private_channel ? 0 : 1;
            this.users = Form1.Instance.session.REST.GetAllUsers().Where( u => u.UserId != Form1.Instance.user?.UserId ).ToArray();
            int i = 0;
            foreach (User u in this.users) {
                listBox1.Items.Add( new listItem( u, u.Username ) );
                if(this.channel.Memberships.Any(m => m.UserId == u.UserId )) {
                    this.listBox1.SelectedIndices.Add( i );
                }
                i++;
            }
            this.comboBox2.Items.AddRange( this.channel.Memberships.Select( m => m.UserDetails.Username ).ToArray());
            this.comboBox2.SelectedIndex = this.comboBox2.Items.IndexOf( Form1.Instance.user?.Username );

        }

        /// <summary>
        /// Triggered when the user changes the memberships,
        /// updates the manager combobox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged( object sender, EventArgs e ) {
            string last = this.comboBox2.SelectedText;
            this.comboBox2.Items.Clear();
            this.comboBox2.Items.AddRange( this.channel.Memberships.Select( m => m.UserDetails.Username ).ToArray() );
            this.comboBox2.SelectedIndex = this.comboBox2.Items.IndexOf( last ) > -1 ? this.comboBox2.Items.IndexOf( last ) : 0;
        }

        /// <summary>
        /// Triggered when the user clicks on the "Save" button.
        /// posts the changes to the server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click( object sender, EventArgs e ) {
            this.channel.Members = this.listBox1.SelectedItems.Cast<listItem>().Select( c => ((User)c.o).UserId ).ToArray() ;
            this.channel.ManagerUser = this.users.Where( u => u.Username == this.comboBox2.SelectedText ).FirstOrDefault().UserId;
            this.channel.Title = this.textBox1.Text;
            this.channel.Description = this.textBox2.Text;
            this.channel.Type = this.comboBox1.SelectedIndex == 0 ? Channel.Types.private_channel : Channel.Types.public_channel;
            if(string.IsNullOrWhiteSpace(this.channel.Title.Trim()) || string.IsNullOrWhiteSpace( this.channel.Description.Trim() )) {
                MessageBox.Show( "Les champs Nom et Description sont necessaires" );
                return;
            }
            try {
                //Le serveur ne peut pas supporter ce type de requete
                //Form1.Instance.session.REST.PostUpdateChannel( this.channel );

            } catch (Exception ex) {
                MessageBox.Show("could not save: \n"+ex.Message);
            }

        }
    }
}
