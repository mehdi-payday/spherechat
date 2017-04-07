using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SphereClient.Components {
    public class MessageRow :Panel{
        private PictureBox image;
        private Panel textPane;
        private Label text;
        private Label timestampLabel;
        private Label usernameLabel;
        private Entities.Message message;
        private Panel parent;




        public MessageRow(Entities.Message message, Panel parent) {
            this.message = message;
            this.parent = parent;

            Color message_bg_color = Constants.LIGHT_GRAY;
            Color message_text_color = Constants.DARK_GRAY;
            if(Form1.Instance.session.GetProfile().UserId == this.message.UserId) {
                message_bg_color = Constants.PURPLE;
                message_text_color = Constants.LIGHT_GRAY;
            }

            this.image = new PictureBox();
            this.textPane = new Panel();
            this.text = new Label();
            this.timestampLabel = new Label();

            this.Width = this.parent.Width;

            this.image.Size = Constants.MEDIUM_IMAGE_SIZE;
            this.image.Left = Constants.MARGIN_SMALL.Left;
            this.image.Top = Constants.MARGIN_SMALL.Top;
            this.image.BackColor = Color.Crimson;
            //this.image.LoadAsync( Form1.Instance.fetchedChannels[0].Memberships.Where( u => u.UserDetails.UserId != Form1.Instance.session.Me.UserId ).First().UserDetails.ProfilePicture);

            this.textPane.Left = this.image.Left + this.image.Width + Constants.MARGIN_SMALL.Right;
            this.textPane.Top = Constants.MARGIN_SMALL.Top;
            this.textPane.MaximumSize = new Size((int)(0.8f * this.Width), int.MaxValue);
            this.textPane.BackColor = message_bg_color;
            this.textPane.BackgroundImage = Constants.TRANSPARENT_GRADIENT_VERTICAL;
            this.textPane.BackgroundImageLayout = ImageLayout.Stretch;

            this.text.Left = Constants.MARGIN_SMALL.Left;
            this.text.Top = Constants.MARGIN_SMALL.Top;
            this.text.MaximumSize = new Size( Constants.MAX_MESSAGE_PANE_WIDTH - Constants.MARGIN_SMALL.Left - Constants.MARGIN_SMALL.Right, int.MaxValue );
            this.text.Text = this.message.UserId +" " + Form1.Instance.session.GetProfile().UserId +"\n"+ this.message.Contents;
            this.text.AutoSize = true;
            this.text.ForeColor = message_text_color;
            
            this.textPane.Height = Constants.MARGIN_SMALL.Top + this.text.Height + Constants.MARGIN_SMALL.Bottom;
            this.textPane.Width = Constants.MARGIN_SMALL.Left + this.text.Width + Constants.MARGIN_SMALL.Right;
            this.textPane.Controls.Add( this.text );
            this.textPane.AutoSize = true;

            if(this.textPane.Height < this.image.Height) {
                this.Height = this.image.Height + Constants.MARGIN_SMALL.Top + Constants.MARGIN_SMALL.Bottom;
                this.textPane.Top = this.image.Top + this.image.Height - this.textPane.Height;
            } else {
                this.Height = this.textPane.Height + Constants.MARGIN_SMALL.Top + Constants.MARGIN_SMALL.Bottom;
                this.image.Top = this.textPane.Top + this.textPane.Height - this.image.Height;
            }
            

            this.timestampLabel.Text = this.message.SentDate.ToString() + "   " + this.message.UserId;
            this.timestampLabel.Left = this.Width - this.timestampLabel.Width - Constants.MARGIN_SMALL.Right;
            this.timestampLabel.Top = this.Height - this.timestampLabel.Height;
            this.timestampLabel.ForeColor = Constants.DARK_GRAY;


            this.Controls.Add(this.image);
            this.Controls.Add( this.textPane );
            this.Controls.Add( this.timestampLabel );
            this.BackColor = Constants.BASE_WHITE;
            
        }

    }
}
