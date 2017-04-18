using SphereClient.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SphereClient {
    struct listItem {
        public object o;
        public string s;

        /// <summary>
        /// struc constructor
        /// </summary>
        /// <param name="o"></param>
        /// <param name="s"></param>
        public listItem(object o, string s) {
            this.o = o;
            this.s = s;
        }

        /// <summary>
        /// string representation of the object,
        /// given by the object's "s" property.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return s;
        }
    }

    public partial class CreateDiscussion : Form {
        private static CreateDiscussion _instance;

        /// <summary>
        /// Constructor for the CreateDiscussion form.
        /// </summary>
        protected CreateDiscussion() {
            InitializeComponent();
            User[] users = Form1.Instance.session.REST.GetAllUsers().Where(u => u.UserId != Form1.Instance.user?.UserId).ToArray();
            foreach (User u in users) {
                listBox1.Items.Add(new listItem(u, u.Username));
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
        /// Shows Dialog and sets the default discussion type.
        /// </summary>
        /// <param name="type"></param>
        public void Show(Entities.Channel.Types type) {
            this.listBox1.SelectedItems.Clear();
            switch (type) {
                case Entities.Channel.Types.discussion:
                    this.comboBox1.SelectedIndex = 0;
                    break;
                case Entities.Channel.Types.private_channel:
                    this.comboBox1.SelectedIndex = 2;
                    break;
                case Entities.Channel.Types.public_channel:
                    this.comboBox1.SelectedIndex = 1;
                    break;
            }
            this.ShowDialog();

        }

        /// <summary>
        /// Triggers when the user clicks on the "Cancel" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            this.Close();
        }

        /// <summary>
        /// Triggers when the user clicks on the "Create !" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateChannel_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(this.textBox1.Text) || string.IsNullOrEmpty(this.textBox1.Text)) {
                this.textBox1.BackColor = Constants.RED;
                System.Threading.Thread t = new System.Threading.Thread(() => {
                    System.Threading.Thread.Sleep(3000);
                    this.textBox1.Invoke(new Action(() => {
                        this.BackColor = Color.White;
                    }));
                });
                t.Start();
                return;
            }
            List<int> checkedNodes = new List<int>();
            checkedNodes.Add((int)Form1.Instance.user?.UserId);
            foreach (object t in this.listBox1.SelectedItems) {
                checkedNodes.Add((int)((User)((listItem)t).o).UserId);
            }
            Channel c = new Channel();
            c.Title = this.textBox1.Text;
            c.Members = checkedNodes.ToArray();
            switch (this.comboBox1.SelectedIndex) {
                case 0:
                    c.Type = Channel.Types.discussion;
                    break;
                case 1:
                    c.Type = Channel.Types.public_channel;
                    break;
                case 2:
                    c.Type = Channel.Types.private_channel;
                    break;
            }
            c.Description = this.textBox2.Text;
            Form1.Instance.session.REST.PostChannel(c);
            MessageBox.Show("Discussion created successfully.");
            this.Close();


        }


    }
}
