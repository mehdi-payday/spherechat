using SphereClient.Entities;
using SphereClient.Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SphereClient.Components {
    /// <summary>
    /// Base class for the "direct messages" and 
    /// "group messages" panels.
    /// </summary>
    public abstract partial class ImageAndTitleListPane : Panel {
        public delegate void PlusClick(object s, EventArgs e);
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
        public event PlusClick OnPlusClick;
        private int selected = 0;

        /// <summary>
        /// Constructor for the ImageAndTitleListPane class.
        /// </summary>
        /// <param name="titlepane">the panel that will serve as a header. must not be null</param>
        /// <param name="list">the <see cref="SphereClient.Entities.Entity"/> list to represent. must not be null</param>
        public ImageAndTitleListPane(IList<Entities.Entity> list) : base() {
            InitializeComponent();
            this.list = list;
            this.contents = new Panel();
            this.Controls.Add(this.contents);
            this.OnPlusClick += (object s, EventArgs e) => { };
        }

        /// <summary>
        /// Creates the required components (all the sub-panels, labels, etc.) 
        /// and places them where they belong.
        /// </summary>
        /// <returns>whether the creation and placement succeeded</returns>
        public bool TryCreateComponents<T>(IList<T> list) {
            bool status = false;
            if (InvokeRequired) {
                Invoke(new Action(delegate () { status = TryCreateComponents(list); }));
                return status;
            }
            try {
                this.list = list.Select(u=>(Entity)u).ToList();
                this.filter();

                if (null == this.contents || this.contents.IsDisposed) {
                    this.contents = new Panel();
                    this.Controls.Add(this.contents);
                }
                this.contents.Controls.Clear();
                this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                this.MinimumSize = new Size(this.Width, title_pane.Size.Height);
                this.title_pane.Left = 0;
                this.title_pane.Top = 0;
                this.title_pane.Width = this.Width;
                this.contents.Left = 0;
                this.contents.Top = this.title_pane.Top + this.title_pane.Size.Height;
                this.contents.Width = this.Width;
                this.contents.Height = this.Height - this.title_pane.Height - this.title_pane.Top;
                this.contents.BackColor = Color.Transparent;//Constants.PURPLE;
                this.contents.AutoScroll = true;

                int entityIndex = 0;
                //TODO and move events to objects
                foreach (Entity entity in this.list) {
                    ImageAndTitleRow row = new ImageAndTitleRow(entity.ToText(), "http://vignette3.wikia.nocookie.net/reddeadredemption/images/8/88/Reddeadredemption_agentedgarross_256x256.jpg/revision/latest?cb=20110906163856", this);
                    row.entity = entity;
                    row.Click += OnRowLabelClick;
                    row.Top = (this.contents.Controls.Count) * row.PreferredSize.Height;
                    this.contents.Controls.Add(row);
                    entityIndex++;
                }

                status = true;

            }
            catch (Exception exception) {
                MessageBoxLogger.Instance.Log(this, "from TryCreateComponents:\n" + exception.Message + "\n" + exception.StackTrace);
            }
            return status;
        }

        /// <summary>
        /// Triggered when the user clicks on a row.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public void OnRowLabelClick(object s, EventArgs e) {
            System.Threading.Thread t = new System.Threading.Thread(delegate () {
                if (null != ((ImageAndTitleRow)s).entity) {
                    Form1.Instance.SetCurrentViewedChannel(((Channel)((ImageAndTitleRow)s).entity).ThreadId);
                }
                else {
                    MessageBox.Show("an entity was null in the group imageandtitlelistpane");
                }

            });
            t.Start();
        }

        /// <summary>
        /// Exposes the invokation of the OnPlusClick event to
        /// all subclasses.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        protected void InvokePlusClick(object s, EventArgs e) {
            if (InvokeRequired) {
                Invoke(new Action(() => { this.InvokePlusClick(s, e); }));
                return;
            }
            this.OnPlusClick(s, e);
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
    public class DiscussionListPanel : ImageAndTitleListPane {
        private Panel _head;
        private static int leftIconOffset = 210;

        /// <summary>
        /// Constructor for the DiscussionListPanel class.
        /// </summary>
        /// <param name="list">the Sphereclient.Entities.Entity list to represent</param>
        public DiscussionListPanel(IList<Entity> list) : base(list) {
            base.title_pane = new Panel();
            Label title = new Label();
            Label plus = new Label();
            title.Text = "Direct messages";
            title.Left = Constants.MARGIN_SMALL.Left;
            title.Top = Constants.MARGIN_SMALL.Top;
            title.AutoSize = true;
            plus.Text = "+";
            plus.Left = DiscussionListPanel.leftIconOffset;
            plus.Top = Constants.MARGIN_SMALL.Top;
            base.title_pane.Height = 37;
            base.title_pane.Controls.Add(title);
            base.title_pane.Controls.Add(plus);
            base.title_pane.BackColor = Constants.LIGHT_PURPLE;
            base.title_pane.ForeColor = Constants.DARK_PURPLE;
            base.title_pane.Font = Constants.SECTION_TITLE_FONT;
            this.BackColor = Constants.PURPLE;
            this._head = base.title_pane;
            this.ForeColor = Constants.DARK_PURPLE;
            plus.Cursor = Cursors.Hand;
            plus.Click += (object s, EventArgs e) => {
                base.InvokePlusClick(this, e);
            };
            base.Controls.Add(base.title_pane);

        }

        /// <summary>
        /// Constructor for the DiscussionListPanel class.
        /// will fetch the Channel list from <see cref="Form1.Instance.session"/>'s GetChannels
        /// method and filter them to only take those being Thread.Types.DISCUSSION 
        /// </summary>
        public DiscussionListPanel() : this(new List<Entity>()) {
            //void
        }

        /// <summary>
        /// Filters off all entities that are not of type Thread.Types.DISCUSSION
        /// </summary>
        public override void filter() {
            this.list = this.list.Where(c =>c.ToTypeString() == "private_channel").ToList<Entity>();
        }

    }

    /// <summary>
    /// "Group message" Panel class.
    /// </summary>
    public class GroupListPanel : ImageAndTitleListPane {

        private Panel _head;
        private static int leftIconOffset = 210;

        /// <summary>
        /// Constructor for the DiscussionListPanel class.
        /// </summary>
        /// <param name="list">the Sphereclient.Entities.Entity list to represent</param>
        public GroupListPanel(IList<Entity> list) : base(list) {
            base.title_pane = new Panel();
            Label title = new Label();
            Label plus = new Label();
            title.Text = "Group messages";
            title.Left = Constants.MARGIN_SMALL.Left;
            title.Top = Constants.MARGIN_SMALL.Top;
            title.AutoSize = true;
            plus.Text = "+";
            plus.Left = GroupListPanel.leftIconOffset;
            plus.Top = Constants.MARGIN_SMALL.Top;
            base.title_pane.Height = 37;
            base.title_pane.Controls.Add(title);
            base.title_pane.Controls.Add(plus);
            base.title_pane.BackColor = Constants.LIGHT_PURPLE;
            base.title_pane.ForeColor = Constants.DARK_PURPLE;
            base.title_pane.Font = Constants.SECTION_TITLE_FONT;
            this.BackColor = Constants.PURPLE;
            this._head = base.title_pane;
            this.ForeColor = Constants.DARK_PURPLE;
            plus.Cursor = Cursors.Hand;
            plus.Click += (object s, EventArgs e) => {
                base.InvokePlusClick(this, e);
            };
            base.Controls.Add(base.title_pane);
        }

        /// <summary>
        /// Constructor for the DiscussionListPanel class.
        /// will fetch the Channel list from <see cref="Form1.Instance.session"/>'s GetChannels
        /// method and filter them to only take those being Thread.Types.DISCUSSION 
        /// </summary>
        public GroupListPanel() : this(new List<Entity>()) {
            //void
        }

        /// <summary>
        /// Filters off all entities that are of type Thread.Types.DISCUSSION
        /// </summary>
        public override void filter() {
            this.list = this.list.Where(c => c.ToTypeString() == "public_channel").ToList<Entity>();
        }

    }





}
