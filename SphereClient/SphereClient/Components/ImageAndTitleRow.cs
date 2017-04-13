using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SphereClient.Components {
    class ImageAndTitleRow : Panel{
        
        public PictureBox image;
        public Label title;
        public object entity;
        
        /// <summary>
        /// Constructor for the ImageAndTitleRow class.
        /// </summary>
        /// <param name="title">the text to display right of the image</param>
        /// <param name="imageURL">the URL of the image to display</param>
        /// <param name="parent">the parent container of the object</param>
        /// <param name="titleFont">(optional)the font to use for the title</param>
        public ImageAndTitleRow(string title, string imageURL, Panel parent, Font titleFont = null):base() {
            this.Parent = parent;
            this.title = new Label();
            this.image = new PictureBox();
            this.image.Size = Constants.MEDIUM_IMAGE_SIZE;
            this.image.BackColor = Color.Crimson;
            this.image.Margin = Constants.MARGIN_SMALL;
            this.image.Left = Constants.MARGIN_SMALL.Left;
            this.image.Top = Constants.MARGIN_SMALL.Top;
            this.image.Load( imageURL );
            this.image.SizeMode = PictureBoxSizeMode.Zoom;
            this.image.Click += ( object s, EventArgs e ) => { this.OnClick( e ); };
            this.image.MouseEnter += ( object s, EventArgs e ) => { this.OnMouseEnter( e ); };
            this.image.MouseLeave += ( object s, EventArgs e ) => { this.OnMouseLeave( e ); };
            this.Height = this.image.Height + Constants.MARGIN_SMALL.Bottom + Constants.MARGIN_SMALL.Top;
            this.title.Text = title;
            this.title.AutoSize = true;
            //this.title.Font = titleFont ?? Constants.BIG_BOLD_FONT;
            this.title.Click += (object s, EventArgs e) =>{ this.OnClick(e); };
            this.title.MouseEnter += (object s, EventArgs e) =>{ this.OnMouseEnter(e); };
            this.title.MouseLeave += ( object s, EventArgs e ) => { this.OnMouseLeave( e ); };
            this.title.AutoEllipsis = true;
            this.title.Top = this.Height / 2 - this.title.Height / 2;
            this.title.Left = this.image.Left + this.image.Width + Constants.MARGIN_SMALL.Right;
            this.Controls.Add( this.title );
            this.Controls.Add( this.image );
            this.BackColor = Color.Transparent;
            this.Cursor = Cursors.Hand;
            if (null != this.Parent) {
                this.Width = this.Parent.Width;
            }
            this.title.MaximumSize= new Size(this.Width - this.title.Left - Constants.MARGIN_SMALL.Right, 30);
            
        }

        

        /// <summary>
        /// Gets or set the title of the row
        /// </summary>
        public string TitleText {
            get {
                return this.title?.Text;
            }
            set {
                if (null != this.title ) {
                    this.title.Text = value;
                }
            }
        }


    }
}
