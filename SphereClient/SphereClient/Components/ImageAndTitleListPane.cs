using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SphereClient.Entities;
using SphereClient.Logger;

namespace SphereClient.Components {

    public class leftSection {
        IList<Channel> discussions;
        IList<Channel> group_discussions;

        Panel discussion_section_panel;
        Panel group_section_panel;

        public leftSection() {
            Channel[] allchannels = Form1.Instance.session?.GetChannels();
            this.discussions = allchannels.Where( c => c.Type == Thread.Types.DISCUSSION ).ToList();
            this.group_discussions = allchannels.Except( this.discussions ).ToList();
        }



    };

    public partial class ImageAndTitleListPane : Panel{

        public Panel title_pane;
        public Panel contents;
        public IList<Entity> list;
        public int picBox_width = 40;
        public int picBox_height = 40;
        public int picBox_left = 8;
        public int rowHeight = 56;
        public int rowlabel_left_margin = 10;
        public int rowBottomMargin = 0;
        public int rowTopMargin = 0;

        /// <summary>
        /// Constructor for the ImageAndTitleListPane class.
        /// </summary>
        /// <param name="titlepane">the panel that will serve as a header. must not be null</param>
        /// <param name="list">the <see cref="SphereClient.Entities.Entity"/> list to represent. must not be null</param>
        public ImageAndTitleListPane(Panel titlepane, IList<Entity> list): base() {
            InitializeComponent();
            this.list = list;
            this.title_pane = titlepane;
            

            /*if (!TryCreateComponents()) {
                //MessageBoxLogger.Instance.Log(this, "ImageAndTitleListPane component creation failed");
                Application.Exit();
            }*/


        }

        /// <summary>
        /// Creates the required components (all the sub-panels, labels, etc.) 
        /// and places them where they belong.
        /// </summary>
        /// <returns>whether the creation and placement succeeded</returns>
        public bool TryCreateComponents() {
            
            bool status = false;
            try {
                this.list = Form1.Instance.session.GetChannels().Where( c => Thread.Types.DISCUSSION == c.Type ).ToList<Entity>() ;
                
                if (null == this.contents || this.contents.IsDisposed) {
                    this.contents = new Panel();
                }
                this.contents.Controls.Clear();
                this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                this.MinimumSize = new Size( this.Width, title_pane.Size.Height );
                this.title_pane.Left = 0;
                this.title_pane.Top = 0;
                this.contents.Left = 0;
                this.contents.Top = this.title_pane.Top + this.title_pane.Size.Height;
                this.contents.Width = this.Width;
                this.contents.Height = this.Height - this.title_pane.Height - this.title_pane.Top;
                this.contents.BackColor = Color.AliceBlue;
                this.contents.AutoScroll = true;

                int entityIndex = 0;
                foreach (Entity entity in this.list) {
                    Panel pane = new Panel();
                    PictureBox picbox = new PictureBox();
                    Label text = new Label();
                    MessageBoxLogger.Instance.Log( this, "creating " + entity.ToText());
                    pane.Top = this.rowTopMargin + (this.rowHeight + this.rowTopMargin) * entityIndex + (this.rowBottomMargin * entityIndex);
                    pane.Left = 0;
                    pane.Size = new Size(this.Width, this.rowHeight);
                    text.Text = entity.ToText();
                    text.Left = this.picBox_left + this.picBox_width + this.rowlabel_left_margin;
                    text.Top = (pane.Height - text.Height) / 2;
                    picbox.Size = new Size(this.picBox_width, this.picBox_height);
                    picbox.Left = this.picBox_left;
                    picbox.BackColor = Color.Aquamarine;
                    picbox.Top = (pane.Height - picBox_height) / 2;
                    pane.Controls.Add( picbox );
                    pane.Controls.Add( text );
                    this.contents.Controls.Add( pane);

                    entityIndex++;
                }



                status = true;
            } catch (Exception exception) {
                MessageBoxLogger.Instance.Log(this, "from TryCreateComponents:\n" + exception.Message + "\n" + exception.StackTrace);
            }
            return status;
        }



    }


    public class DiscussionListPanel: ImageAndTitleListPane {
        protected static Panel header;
        private static readonly int leftTitleOffset = 8;
        private static readonly int leftIconOffset = 0;

        /// <summary>
        /// Static initializer.
        /// </summary>
        static DiscussionListPanel() {
            DiscussionListPanel.header = new Panel();
            Label title = new Label();
            title.Text = "Direct messages";
            DiscussionListPanel.header.Controls.Add( title );
            title.Left = DiscussionListPanel.leftTitleOffset;
            title.Top = (DiscussionListPanel.header.Height - title.Height) / 2;
            Label plus = new Label();
            plus.Text = "+";
            DiscussionListPanel.header.Controls.Add(plus);
            plus.Left = DiscussionListPanel.leftIconOffset;
            plus.Top = (DiscussionListPanel.header.Height - plus.Height) / 2;
        }

        /// <summary>
        /// Constructor for the DiscussionListPanel class.
        /// </summary>
        /// <param name="list">the Sphereclient.Entities.Entity list to represent</param>
        public DiscussionListPanel(IList<Entity> list): base( DiscussionListPanel.header, list){

        }
        /// <summary>
        /// Constructor for the DiscussionListPanel class.
        /// will fetch the Channel list from <see cref="Form1.Instance.session"/>'s GetChannels
        /// method and filter them to only take those being Thread.Types.DISCUSSION 
        /// </summary>
        public DiscussionListPanel() : base(DiscussionListPanel.header, new List<Entity>()) {//

        }


    }




}
