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
        private int selected = 0;

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
                this.contents.BackColor = Constants.PURPLE;
                this.contents.AutoScroll = true;

                int entityIndex = 0;
                //TODO and move events to objects
                foreach (Entity entity in this.list) {
                    ImageAndTitleRow row = new ImageAndTitleRow( entity.ToText(), "http://vignette3.wikia.nocookie.net/reddeadredemption/images/8/88/Reddeadredemption_agentedgarross_256x256.jpg/revision/latest?cb=20110906163856", this );
                   
                    this.contents.Controls.Add(row);

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

        public void OnRowLabelClick(object s, EventArgs e ) {
            this.contents.Controls.IndexOf( (Label)s );
            
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
        private static int leftIconOffset = 210;

        /// <summary>
        /// Static initializer.
        /// </summary>
        static DiscussionListPanel() {
            DiscussionListPanel.header = new Panel();
            Label title = new Label();
            Label plus = new Label();
            title.Text = "Direct messages";
            title.Left = Constants.MARGIN_SMALL.Left;
            title.Top = Constants.MARGIN_SMALL.Top;
            title.AutoSize = true;
            plus.Text = "+";
            plus.Left = DiscussionListPanel.leftIconOffset;
            plus.Top = Constants.MARGIN_SMALL.Top;
            DiscussionListPanel.header.Height = 37;
            DiscussionListPanel.header.Controls.Add( title );
            DiscussionListPanel.header.Controls.Add( plus );
            DiscussionListPanel.header.BackColor = Constants.LIGHT_PURPLE;
            DiscussionListPanel.header.ForeColor = Constants.DARK_PURPLE;
            DiscussionListPanel.header.Font = Constants.SECTION_TITLE_FONT ;
        }

        /// <summary>
        /// Constructor for the DiscussionListPanel class.
        /// </summary>
        /// <param name="list">the Sphereclient.Entities.Entity list to represent</param>
        public DiscussionListPanel(IList<Entity> list): base( DiscussionListPanel.header, list){
            this.BackColor = Constants.PURPLE;
            this._head = DiscussionListPanel.header;
        }
        /// <summary>
        /// Constructor for the DiscussionListPanel class.
        /// will fetch the Channel list from <see cref="Form1.Instance.session"/>'s GetChannels
        /// method and filter them to only take those being Thread.Types.DISCUSSION 
        /// </summary>
        public DiscussionListPanel() : base(DiscussionListPanel.header, new List<Entity>()) {//
            this.BackColor = Constants.PURPLE;
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
        private static int leftIconOffset = 210;
        

        /// <summary>
        /// Static initializer.
        /// </summary>
        static GroupListPanel() {
            GroupListPanel.header = new Panel();
            Label title = new Label();
            Label plus = new Label();
            title.Text = "Group messages";
            title.Left = Constants.MARGIN_SMALL.Left;
            title.Top = Constants.MARGIN_SMALL.Top;
            title.AutoSize = true;
            plus.Text = "+";
            plus.Left = GroupListPanel.leftIconOffset;
            plus.Top = Constants.MARGIN_SMALL.Top;
            GroupListPanel.header.Height = 37;
            GroupListPanel.header.Controls.Add( title );
            GroupListPanel.header.Controls.Add( plus );
            GroupListPanel.header.BackColor = Constants.LIGHT_PURPLE;
            GroupListPanel.header.ForeColor = Constants.DARK_PURPLE;
            GroupListPanel.header.Font = Constants.SECTION_TITLE_FONT;
        }

        /// <summary>
        /// Constructor for the DiscussionListPanel class.
        /// </summary>
        /// <param name="list">the Sphereclient.Entities.Entity list to represent</param>
        public GroupListPanel( IList<Entity> list ) : base( GroupListPanel.header, list ) {
            this.BackColor = Constants.PURPLE;
            this._head = GroupListPanel.header;
        }
        /// <summary>
        /// Constructor for the DiscussionListPanel class.
        /// will fetch the Channel list from <see cref="Form1.Instance.session"/>'s GetChannels
        /// method and filter them to only take those being Thread.Types.DISCUSSION 
        /// </summary>
        public GroupListPanel() : base( GroupListPanel.header, new List<Entity>() ) {//
            this.BackColor = Constants.PURPLE;
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
