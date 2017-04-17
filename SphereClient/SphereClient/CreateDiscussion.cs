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
    public partial class CreateDiscussion : Form {
        private static CreateDiscussion _instance;

        /// <summary>
        /// Constructor for the CreateDiscussion form.
        /// </summary>
        protected CreateDiscussion() {
            InitializeComponent();
            User[] users = Form1.Instance.session.REST.GetAllUsers().Where( u => u.UserId != Form1.Instance.user?.UserId).ToArray();
            foreach(User u in users) {
                TreeNode t = new TreeNode();
                t.Text = u.Username;
                t.Tag = u;
                this.treeView2.Nodes.Add(t);
            }
        }

        /// <summary>
        /// Singleton instance getter and setter.
        /// </summary>
        public static CreateDiscussion Instance {
            get {
                if (null == CreateDiscussion._instance) {
                    CreateDiscussion._instance = new CreateDiscussion();
                }
                return CreateDiscussion._instance;
            }
            set {
                CreateDiscussion._instance = value;
            }
        }



        /// <summary>
        /// Triggers when the user clicks on the "Cancel" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel3_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e ) {
            this.Close();
        }

        /// <summary>
        /// Triggers when the user clicks on the "Create !" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click( object sender, EventArgs e ) {
            if (string.IsNullOrWhiteSpace(this.textBox1.Text) || string.IsNullOrEmpty(this.textBox1.Text)) {
                this.textBox1.BackColor = Constants.RED;
                System.Threading.Thread t = new System.Threading.Thread( () => {
                    System.Threading.Thread.Sleep( 3000 );
                    this.textBox1.Invoke( new Action( () => {
                        this.BackColor = Color.White;
                    } ) );
                } );
                t.Start();
                return;
            }
            List<int> checkedNodes = new List<int>();
            checkedNodes.Add((int)Form1.Instance.user?.UserId);
            foreach(TreeNode t in this.treeView2.Nodes) {
                if (t.Checked) {
                    checkedNodes.Add( (int)( (User)t.Tag ).UserId );
                }
            }
            Channel c = new Channel();
            c.Title = this.textBox1.Text;
            c.ManagerUser = (int)Form1.Instance.user?.UserId;

            try {
                Form1.Instance.session.REST.PostChannel( c );
                MessageBox.Show( "Discussion created successfully." );
                this.Close();
            } catch (Exception ex) {
                MessageBox.Show( "create channel exception \n" + ex.Message );
            }

        }


    }
}
