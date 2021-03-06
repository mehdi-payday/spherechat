
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SphereClient.Components {
    class MessageListPanel : Panel {
        private IList<MessageRow> messages;
        public Entities.Channel channel;

        /// <summary>
        /// Default constructor for the MessageListPanel class.
        /// </summary>
        public MessageListPanel() : base() {
            this.messages = new List<MessageRow>();
            this.BackColor = Color.Transparent;
        }

        /// <summary>
        /// Constructor for the MessageListPanel class. also loads messages
        /// from the given channel.
        /// </summary>
        /// <param name="channel">The channel to fetch the messages from</param>
        public MessageListPanel(Entities.Channel channel) : base() {
            this.channel = channel;
            this.messages = new List<MessageRow>();
            FetchMessages(channel);
        }

        /// <summary>
        /// Removes all contained messages, fetches those of the specified channel and
        /// displays them.
        /// </summary>
        /// <param name="channel">The channel to fetch the messages from</param>
        public void FetchMessages(Entities.Channel channel) {
            Form1.Instance.ShowPreloader();
            this.messages.Clear();
            this.Controls.Clear();
            this.channel = channel;
            Entities.Message[] fetchedMessages = Form1.Instance.session.REST.GetMessages(channel).ToArray();
            int hoffset = 0;
            foreach (Entities.Message msg in fetchedMessages.OrderBy(m => m.SentDate)) {
                MessageRow mr = new MessageRow(msg, this.channel, this);
                mr.Top = hoffset;
                this.messages.Add(mr);
                this.Controls.Add(mr);
                hoffset += mr.Height;
            }
            System.Threading.Thread t = new System.Threading.Thread(() => {
                System.Threading.Thread.Sleep(1000);
                Form1.Instance.HidePreloader();
            });
            t.Start();
        }

        /// <summary>
        /// Retrieves the messages since last time they were fetched and adds them to
        /// the list. Unlike FetchMessages, this method is non-destructive
        /// </summary>
        /// <param name="channel"> the channel to fetch the messages from</param>
        public void FetchMessagesSinceLastTime(Entities.Channel channel) {
            Entities.Message[] fetchedMessages = Form1.Instance.session.REST.GetMessages(channel).ToArray();
            foreach (Entities.Message msg in fetchedMessages.OrderBy(m => m.SentDate)) {
                OnNewMessage(msg);
            }
        }

        /// <summary>
        /// Adds and displays the given message.
        /// When a new message is detected, this method should be called.
        /// </summary>
        /// <param name="message">the new message to add</param>
        public void OnNewMessage(Entities.Message message) {
            if (InvokeRequired) {
                Invoke(new Action(()=> { OnNewMessage( message ); } ));
                return;
            }
            MessageRow mr = new MessageRow(message, this.channel, this);
            if (this.messages.Any()) {
                mr.Top = this.messages.Last().Top + this.messages.Last().Height;
            }
            else {
                mr.Top = 0;
            }
            this.messages.Add(mr);
            this.Controls.Add(mr);
        }

    }
}
