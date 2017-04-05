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

    public abstract partial class ImageAndTitleListPane : Panel{

        public Panel title_pane;
        public Panel contents;
        public IList<Entity> list;
        public int picBox_width = 40;
        public int picBox_height = 40;
        public int picBox_left = 8;
        public int rowHeight = 56;
        public int rowlabel_left_margin = 5;
        public int rowBottomMargin = 0;
        public int rowTopMargin = 0;
        public Font HoverFont = new Font( "Microsoft Sans Serif", 11.5f, FontStyle.Bold, GraphicsUnit.Point );
        public Font RegularFont = new Font( "Microsoft Sans Serif", 12.0f, FontStyle.Regular, GraphicsUnit.Point );


        /// <summary>
        /// Constructor for the ImageAndTitleListPane class.
        /// </summary>
        /// <param name="titlepane">the panel that will serve as a header. must not be null</param>
        /// <param name="list">the <see cref="SphereClient.Entities.Entity"/> list to represent. must not be null</param>
        public ImageAndTitleListPane(Panel titlepane, IList<Entity> list): base() {
            InitializeComponent();
            this.list = list;
            this.title_pane = titlepane;
            this.contents = new Panel();
            this.Controls.Add( this.title_pane );
            this.Controls.Add( this.contents );

        }

        /// <summary>
        /// Creates the required components (all the sub-panels, labels, etc.) 
        /// and places them where they belong.
        /// </summary>
        /// <returns>whether the creation and placement succeeded</returns>
        public bool TryCreateComponents() {
            bool status = false;
            if (InvokeRequired) {
                Invoke( new Action( delegate (){ status = TryCreateComponents(); } ) );
                return status;
            }
            
            try {
                this.list = Form1.Instance.fetchedChannels.ToList<Entity>() ;
                this.filter();
                this.BackColor = Color.Red;
                if(null == this.contents || this.contents.IsDisposed) {
                    this.contents = new Panel();
                    this.Controls.Add( this.contents );
                }
                this.contents.Controls.Clear();
                this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                this.MinimumSize = new Size( this.Width, title_pane.Size.Height );
                this.title_pane.Left = 0;
                this.title_pane.Top = 0;
                this.title_pane.Width = this.Width;
                this.contents.Left = 0;
                this.contents.Top =  this.title_pane.Top + this.title_pane.Size.Height;
                this.contents.Width = this.Width;
                this.contents.Height = this.Height - this.title_pane.Height - this.title_pane.Top;
                this.contents.BackColor = Color.FromArgb( 93, 6, 61 );
                this.contents.AutoScroll = true;

                int entityIndex = 0;
                //TODO turn rows into objects. (and move events to objects)
                foreach (Entity entity in this.list) {
                    Panel pane = new Panel();
                    PictureBox picbox = new PictureBox();
                    Label text = new Label();
                    pane.Cursor = Cursors.Hand;
                    pane.Top = this.rowTopMargin + (this.rowHeight + this.rowTopMargin) * entityIndex + (this.rowBottomMargin * entityIndex);
                    pane.Left = 0;
                    pane.Size = new Size(this.Width, this.rowHeight);
                    text.MouseEnter += this.OnRowLabelEnter;
                    text.MouseLeave += this.OnRowLabelLeave;
                    text.Text = entity.ToText();
                    text.AutoEllipsis = true;
                    text.Left = this.picBox_left + this.picBox_width + this.rowlabel_left_margin;
                    text.Top = (pane.Height - (text.Font.Height-1) *2) / 2 ;
                    text.Width = pane.Width - text.Left;
                    text.Font = this.RegularFont;
                    text.ForeColor = Color.FromArgb( 165, 97, 149 );
                    picbox.Size = new Size(this.picBox_width, this.picBox_height);
                    picbox.Left = this.picBox_left;
                    picbox.BackColor = Color.Aquamarine;
                    picbox.Top = (pane.Height - picBox_height) / 2;
                    pane.Controls.Add( picbox );
                    pane.Controls.Add( text );
                    this.contents.Controls.Add(pane);

                    entityIndex++;
                }
                
                status = true;

            } catch (Exception exception) {
                MessageBoxLogger.Instance.Log(this, "from TryCreateComponents:\n" + exception.Message + "\n" + exception.StackTrace);
            }
            return status;
        }

        /// <summary>
        /// Triggered when the mouse enters a row.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public void OnRowLabelEnter(object s, EventArgs e) {
            if (InvokeRequired) {
                Invoke( new Action( delegate() { OnRowLabelEnter( s, e ); } ) );
                return;
            }
            ((Label)s).Font = this.HoverFont;

        }
        /// <summary>
        /// Triggered when the mouse leaves a row.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public void OnRowLabelLeave( object s, EventArgs e ) {
            if (InvokeRequired) {
                Invoke( new Action( delegate () { OnRowLabelLeave( s, e ); } ) );
                return;
            }
            ((Label)s).Font = this.RegularFont;
        }

        /// <summary>
        /// This method should filter off all the unwanted entities in 
        /// this.list.
        /// </summary>
        public abstract void filter();


    }

    /// <summary>
    /// "Direct messages" Panel class.
    /// </summary>
    public class DiscussionListPanel: ImageAndTitleListPane {
        protected static Panel header;
        private Panel _head;
        private static int leftTitleOffset = 8;
        private static int topMargin = 5;
        private static int leftIconOffset = 210;

        /// <summary>
        /// Static initializer.
        /// </summary>
        static DiscussionListPanel() {
            DiscussionListPanel.header = new Panel();
            Label title = new Label();
            Label plus = new Label();
            title.Text = "Direct messages";
            title.Left = DiscussionListPanel.leftTitleOffset;
            title.Top = DiscussionListPanel.topMargin;
            title.AutoSize = true;
            plus.Text = "+";
            plus.Left = DiscussionListPanel.leftIconOffset;
            plus.Top = DiscussionListPanel.topMargin;
            DiscussionListPanel.header.Height = 37;
            DiscussionListPanel.header.Controls.Add( title );
            DiscussionListPanel.header.Controls.Add( plus );
            DiscussionListPanel.header.BackColor = Color.FromArgb( 165, 97, 149 );
            DiscussionListPanel.header.ForeColor = Color.FromArgb( 62, 1, 56 );
            DiscussionListPanel.header.Font = new Font( "Microsoft Sans Serif", 14.25f, FontStyle.Bold,GraphicsUnit.Point );
        }

        /// <summary>
        /// Constructor for the DiscussionListPanel class.
        /// </summary>
        /// <param name="list">the Sphereclient.Entities.Entity list to represent</param>
        public DiscussionListPanel(IList<Entity> list): base( DiscussionListPanel.header, list){
            this.BackColor = Color.FromArgb( 93, 6, 61 );
            this._head = DiscussionListPanel.header;
        }
        /// <summary>
        /// Constructor for the DiscussionListPanel class.
        /// will fetch the Channel list from <see cref="Form1.Instance.session"/>'s GetChannels
        /// method and filter them to only take those being Thread.Types.DISCUSSION 
        /// </summary>
        public DiscussionListPanel() : base(DiscussionListPanel.header, new List<Entity>()) {//
            this.BackColor = Color.FromArgb( 93, 6, 61 );
            this._head = DiscussionListPanel.header;   
        }

        /// <summary>
        /// Filters off all entities that are not of type Thread.Types.DISCUSSION
        /// </summary>
        public override void filter() {
            this.list = this.list.Where( c => Thread.Types.DISCUSSION == ((Channel)c).Type ).ToList<Entity>();
        }

    }

    /// <summary>
    /// "Group message" Panel class.
    /// </summary>
    public class GroupListPanel : ImageAndTitleListPane {
        protected static Panel header;
        private Panel _head;
        private static int leftTitleOffset = 8;
        private static int topMargin = 5;
        private static int leftIconOffset = 210;

        /// <summary>
        /// Static initializer.
        /// </summary>
        static GroupListPanel() {
            GroupListPanel.header = new Panel();
            Label title = new Label();
            Label plus = new Label();
            title.Text = "Group messages";
            title.Left = GroupListPanel.leftTitleOffset;
            title.Top = GroupListPanel.topMargin;
            title.AutoSize = true;
            plus.Text = "+";
            plus.Left = GroupListPanel.leftIconOffset;
            plus.Top = GroupListPanel.topMargin;
            GroupListPanel.header.Height = 37;
            GroupListPanel.header.Controls.Add( title );
            GroupListPanel.header.Controls.Add( plus );
            GroupListPanel.header.BackColor = Color.FromArgb( 165, 97, 149 );
            GroupListPanel.header.ForeColor = Color.FromArgb( 62, 1, 56 );
            GroupListPanel.header.Font = new Font( "Microsoft Sans Serif", 14.25f, FontStyle.Bold, GraphicsUnit.Point );
        }

        /// <summary>
        /// Constructor for the DiscussionListPanel class.
        /// </summary>
        /// <param name="list">the Sphereclient.Entities.Entity list to represent</param>
        public GroupListPanel( IList<Entity> list ) : base( GroupListPanel.header, list ) {
            this.BackColor = Color.FromArgb( 93, 6, 61 );
            this._head = GroupListPanel.header;
        }
        /// <summary>
        /// Constructor for the DiscussionListPanel class.
        /// will fetch the Channel list from <see cref="Form1.Instance.session"/>'s GetChannels
        /// method and filter them to only take those being Thread.Types.DISCUSSION 
        /// </summary>
        public GroupListPanel() : base( GroupListPanel.header, new List<Entity>() ) {//
            this.BackColor = Color.FromArgb( 93, 6, 61 );
            this._head = GroupListPanel.header;
        }

        /// <summary>
        /// Filters off all entities that are of type Thread.Types.DISCUSSION
        /// </summary>
        public override void filter() {
            this.list = this.list.Where( c => Thread.Types.DISCUSSION != ((Channel)c).Type ).ToList<Entity>();
        }

    }





}
